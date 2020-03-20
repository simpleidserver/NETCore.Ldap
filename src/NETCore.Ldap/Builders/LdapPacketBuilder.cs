// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.DER;
using NETCore.Ldap.DER.Applications.Requests;
using NETCore.Ldap.DER.Universals;
using System;
using System.Collections.Generic;

namespace NETCore.Ldap.Builders
{
    public class LdapPacketBuilder
    {
        public static LdapPacket NewBindRequest(int messageId, int version, string name, Action<BindRequestBuilder> callback)
        {
            var bindRequestBuilder = new BindRequestBuilder(version, name);
            callback(bindRequestBuilder);
            var bindRequest = bindRequestBuilder.Build();
            var result = new LdapPacket
            {
                MessageId = new DERInteger(messageId),
                ProtocolOperation = new DERProtocolOperation
                {
                    Operation = bindRequest
                }
            };
            return result;
        }

        public static LdapPacket NewAddRequest(int messageId, string distinguishedName, Action<AddRequestBuilder> callback)
        {
            var addRequestBuilder = new AddRequestBuilder(distinguishedName);
            callback(addRequestBuilder);
            var addRequest = addRequestBuilder.Build();
            var result = new LdapPacket
            {
                MessageId = new DERInteger(messageId),
                ProtocolOperation = new DERProtocolOperation
                {
                    Operation = addRequest
                }
            };
            return result;
        }

        public static LdapPacket NewSearchRequest(int messageId, string baseObject, SearchRequestScopes scope, SearchRequestDeferAliases searchRequestDeferAliases, int sizeLimit, int timeLimit, bool typesOnly, ICollection<string> attributes, Action<SearchRequestBuilder> callback)
        {
            var builder = new SearchRequestBuilder(baseObject, scope, searchRequestDeferAliases, sizeLimit, timeLimit, typesOnly, attributes);
            callback(builder);
            var result = new LdapPacket
            {
                MessageId = new DERInteger(messageId),
                ProtocolOperation = new DERProtocolOperation
                {
                    Operation = builder.Build()
                }
            };
            return result;
        }
    }
}
