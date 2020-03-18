// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.Authentication;
using NETCore.Ldap.DER;
using NETCore.Ldap.DER.Applications;
using NETCore.Ldap.DER.Applications.Requests;
using NETCore.Ldap.DER.Applications.Responses;
using NETCore.Ldap.DER.Universals;
using NETCore.Ldap.Exceptions;
using NETCore.Ldap.Persistence;
using NETCore.Ldap.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NETCore.Ldap.Commands.Handlers
{
    public class BindRequestCommandHandler : IBindRequestCommandHandler
    {
        private readonly ILDAPEntryQueryStore _ldapEntryQueryStore;
        private readonly IAuthenticationHandlerFactory _authenticationHandlerFactory;

        public BindRequestCommandHandler(ILDAPEntryQueryStore ldapEntryQueryStore, IAuthenticationHandlerFactory authenticationHandlerFactory)
        {
            _ldapEntryQueryStore = ldapEntryQueryStore;
            _authenticationHandlerFactory = authenticationHandlerFactory;
        }

        public async Task<ICollection<LdapPacket>> Execute(BindRequestCommand bindRequestCommand)
        {
            var bindRequest = bindRequestCommand.ProtocolOperation.Operation as BindRequest;
            var dn = bindRequest.Name.Value;
            var authenticationHandler = _authenticationHandlerFactory.Build(bindRequest.Authentication.Type);
            if (authenticationHandler == null)
            {
                throw new LdapException(Global.AuthenticationMethodNotSupported, LDAPResultCodes.AuthMethodNotSupported, dn);
            }

            var entry = await _ldapEntryQueryStore.Get(dn);
            if (entry == null)
            {
                throw new LdapException(string.Format(Global.EntryDoesntExist, dn), LDAPResultCodes.NoSuchObject, dn);
            }

            await authenticationHandler.Authenticate(entry, bindRequest);
            var donePacket = new LdapPacket
            {
                MessageId = bindRequestCommand.MessageId,
                ProtocolOperation = new DERProtocolOperation
                {
                    Operation = new BindResponse
                    {
                        Result = new LDAPResult
                        {
                            MatchedDN = new DEROctetString(bindRequest.Name.Value),
                            ResultCode = new DEREnumerated<LDAPResultCodes>
                            {
                                Value = LDAPResultCodes.Success
                            },
                            DiagnosticMessage = new DEROctetString(string.Empty)
                        }
                    }
                }
            };
            return new List<LdapPacket>
            {
                donePacket
            };
        }
    }
}
