// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.Builders;
using NETCore.Ldap.DER;
using NETCore.Ldap.DER.Applications;
using NETCore.Ldap.DER.Applications.AuthChoices;
using NETCore.Ldap.DER.Applications.Requests;
using NETCore.Ldap.DER.Applications.Responses;
using NETCore.Ldap.DER.Universals;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NETCore.Ldap.Tests
{
    public class LdapPacketBuilderFixture
    {
        [Fact]
        public void When_Serialize_BindRequest()
        {
            var payload = LdapPacketBuilder.NewBindRequest(1, 3, "administrator", (opt) =>
            {
                opt.SetSimpleAuthentication("password");
            }).Serialize().ToList();
            var ldapPacket = LdapPacket.Extract(payload);
            var bindRequest = ldapPacket.ProtocolOperation.Operation as BindRequest;
            var simpleAuthChoice = bindRequest.Authentication as SimpleAuthChoice;
            Assert.NotNull(ldapPacket);
            Assert.Equal(1, ldapPacket.MessageId.Value);
            Assert.Equal(3, bindRequest.Version.Value);
            Assert.Equal("administrator", bindRequest.Name.Value);
            Assert.Equal("password", simpleAuthChoice.Value.Value);
        }

        [Fact]
        public void When_Serialize_BindResponse_With_Error()
        {
            var operation = new BindResponse();
            operation.Result = new LDAPResult
            {
                MatchedDN = new DEROctetString("administrator"),
                DiagnosticMessage = new DEROctetString("error"),
                ResultCode = new DEREnumerated<LDAPResultCodes>
                {
                    Value = LDAPResultCodes.AuthMethodNotSupported
                }
            };
            var ldapPacket = new LdapPacket
            {
                MessageId = new DERInteger(1),
                ProtocolOperation = new DERProtocolOperation
                {
                    Operation = operation
                }
            };
            var payload = ldapPacket.Serialize();
            var deserializedLdapPacket = LdapPacket.Extract(payload);
            var ldapResult = operation.Result as LDAPResult;
            Assert.NotNull(deserializedLdapPacket);
            Assert.Equal("administrator", ldapResult.MatchedDN.Value);
            Assert.Equal("error", ldapResult.DiagnosticMessage.Value);
            Assert.Equal(LDAPResultCodes.AuthMethodNotSupported, ldapResult.ResultCode.Value);
        }

        [Fact]
        public void When_Serialize_AddRequest()
        {
            var payload = LdapPacketBuilder.NewAddRequest(1, "administrator", (opt) =>
            {
                opt.AddAttribute("objectClass", new List<string>
                {
                    "inetOrgPerson"
                });
            }).Serialize().ToList();
            var ldapPacket = LdapPacket.Extract(payload);
            var addRequest = ldapPacket.ProtocolOperation.Operation as AddRequest;
            Assert.NotNull(ldapPacket);
            Assert.Equal(1, ldapPacket.MessageId.Value);
            Assert.True(addRequest.Attributes.Values.Count() == 1);
            Assert.Equal("objectClass", addRequest.Attributes.Values.First().Type.Value);
        }

        [Fact]
        public void When_Serialize_SearchRequest()
        {
            var payload = LdapPacketBuilder.NewSearchRequest(1, "cn=system", SearchRequestScopes.BaseObject, SearchRequestDeferAliases.NeverDerefAliases, 10, 10, false, new List<string> { }, (opt) =>
            {
                opt.SetEqualFilter("name", "value");
            }).Serialize().ToList();
            var ldapPacket = LdapPacket.Extract(payload);
            var searchRequest = ldapPacket.ProtocolOperation.Operation as SearchRequest;
            Assert.NotNull(searchRequest);
        }
    }
}