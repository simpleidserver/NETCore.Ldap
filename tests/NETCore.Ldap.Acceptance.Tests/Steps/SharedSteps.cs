// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Microsoft.Extensions.DependencyInjection;
using NETCore.Ldap.Builders;
using NETCore.Ldap.DER;
using NETCore.Ldap.DER.Applications.Requests;
using NETCore.Ldap.DER.Applications.Responses;
using NETCore.Ldap.Domain;
using NETCore.Ldap.Persistence;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace NETCore.Ldap.Acceptance.Tests.Steps
{
    [Binding]
    public class SharedSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private Stream _stream;
        private static object _obj = new object();
        private static ILdapServer _ldapServer;
        private static IServiceProvider _serviceProvider;
        private static ManualResetEvent _manualResetEvent = new ManualResetEvent(true);

        public SharedSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            lock(_obj)
            {
                if (_ldapServer == null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "LDIF.txt");
                    var serviceCollection = new ServiceCollection();
                    serviceCollection.AddLdapServer().ImportLDIF(path);
                    _serviceProvider = serviceCollection.BuildServiceProvider();
                    _ldapServer = _serviceProvider.GetService<ILdapServer>();
                    _ldapServer.Start();
                }

                var client = new TcpClient("127.0.0.1", 389);
                _stream = client.GetStream();
                _manualResetEvent.WaitOne();
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _manualResetEvent.Set();
        }

        [Given("Add LDAP entry '(.*)'")]
        public void GivenAddEntry(string dn, Table table)
        {
            var ldapEntryCommandStore = _serviceProvider.GetService<ILDAPEntryCommandStore>();
            var ldapEntry = new LDAPEntry
            {
                DistinguishedName = dn,
                Attributes = new List<LDAPEntryAttribute>()
            };
            foreach (var record in table.Rows)
            {
                var key = record["Key"];
                var value = record["Value"];
                ldapEntry.Attributes.Add(new LDAPEntryAttribute
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = key,
                    Values = new List<string>
                    {
                        value
                    }
                });
            }

            ldapEntryCommandStore.Add(ldapEntry);
        }

        [When("Authenticate user with login '(.*)', password '(.*)' and MessageId '(.*)'")]
        public async Task WhenExecuteBindRequest(string userName, string password, int messageId)
        {
            var payload = LdapPacketBuilder.NewBindRequest(messageId, 3, userName, (opt) =>
            {
                opt.SetSimpleAuthentication(password);
            }).Serialize().ToArray();
            await Send(payload);
        }

        [When("Add LDAP entry '(.*)' and MessageId '(.*)'")]
        public async Task WhenExecuteAddRequest(string dn, int messageId, Table table)
        {
            var payload = LdapPacketBuilder.NewAddRequest(messageId, dn, (opt) =>
            {
                foreach(var record in table.Rows)
                {
                    var key = record["Key"];
                    var value = record["Value"];
                    opt.AddAttribute(key, new List<string>
                    {
                        value
                    });
                }
            }).Serialize().ToArray();
            await Send(payload);
        }

        [When("Search LDAP entries, base object is '(.*)', message identifier is '(.*)'")]
        public async Task WhenExecuteSearchRequest(string dn, int messageId)
        {
            var payload = LdapPacketBuilder.NewSearchRequest(messageId, dn, SearchRequestScopes.WholeSubtree, SearchRequestDeferAliases.NeverDerefAliases, 10, 10, false, new List<string> { }, (opt) =>
            {
                opt.SetPresentFilter("objectClass");
            }).Serialize().ToArray();
            await _stream.WriteAsync(payload);
            bool isSearchResultDone = false;
            int i = 0;
            while(!isSearchResultDone)
            {
                var data = new byte[5000];
                await _stream.ReadAsync(data, 0, data.Length);
                var ldapPacket = LdapPacket.Extract(data.ToList());
                var ldapPacketJSON = JObject.FromObject(ldapPacket);
                if (ldapPacket.ProtocolOperation.Operation is SearchResultDone)
                {
                    isSearchResultDone = true;
                    _scenarioContext.Set(ldapPacketJSON, "searchResultDone");
                }
                else
                {
                    _scenarioContext.Set(ldapPacketJSON, $"searchResultEntry-{i}");
                }

                i++;
            }
        }

        [Then("LDAP Packet '(.*)'='(.*)'")]
        public void ThenEqualsTo(string key, string value)
        {
            var jsonHttpBody = _scenarioContext["ldapPacket"] as JObject;
            var currentValue = jsonHttpBody.SelectToken(key).ToString().ToLowerInvariant();
            Assert.Equal(value.ToLowerInvariant(), currentValue);
        }

        [Then("extract JSON '(.*)', JSON '(.*)'='(.*)'")]
        public void ThenExtractJSONEqualsTo(string jsonKey, string key, string value)
        {
            var jsonHttpBody = _scenarioContext[jsonKey] as JObject;
            var currentValue = jsonHttpBody.SelectToken(key).ToString().ToLowerInvariant();
            Assert.Equal(value.ToLowerInvariant(), currentValue);
        }

        private async Task Send(byte[] payload)
        {
            await _stream.WriteAsync(payload);
            var data = new byte[5000];
            await _stream.ReadAsync(data, 0, data.Length);
            var ldapPacket = LdapPacket.Extract(data.ToList());
            var ldapPacketJSON = JObject.FromObject(ldapPacket);
            _scenarioContext.Set(ldapPacketJSON, "ldapPacket");
        }
    }
}