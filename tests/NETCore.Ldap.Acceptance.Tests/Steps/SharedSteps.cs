// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Microsoft.Extensions.DependencyInjection;
using NETCore.Ldap.Builders;
using NETCore.Ldap.DER;
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
                    Value = value
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
            var client = new TcpClient("127.0.0.1", 389);
            var stream = client.GetStream();
            await stream.WriteAsync(payload);
            var data = new byte[5000];
            await stream.ReadAsync(data, 0, data.Length);
            var ldapPacket = LdapPacket.Extract(data.ToList());
            var ldapPacketJSON = JObject.FromObject(ldapPacket);
            _scenarioContext.Set(ldapPacketJSON, "ldapPacket");
        }

        [Then("LDAP Packet '(.*)'='(.*)'")]
        public void ThenEqualsTo(string key, string value)
        {
            var jsonHttpBody = _scenarioContext["ldapPacket"] as JObject;
            var currentValue = jsonHttpBody.SelectToken(key).ToString().ToLowerInvariant();
            Assert.Equal(value.ToLowerInvariant(), currentValue);
        }
    }
}