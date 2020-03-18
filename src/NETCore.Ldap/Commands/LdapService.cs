// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.Commands.Handlers;
using NETCore.Ldap.DER;
using NETCore.Ldap.DER.Applications;
using NETCore.Ldap.Exceptions;
using NETCore.Ldap.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NETCore.Ldap.Commands
{
    public class LdapService : ILdapService
    {
        private readonly IAddRequestCommandHandler _addRequestCommandHandler;
        private readonly IBindRequestCommandHandler _bindRequestCommandHandler;
        private readonly ISearchRequestCommandHandler _searchRequestCommandHandler;

        public LdapService(IAddRequestCommandHandler addRequestCommandHandler, IBindRequestCommandHandler bindRequestCommandHandler, ISearchRequestCommandHandler searchRequestCommandHandler)
        {
            _addRequestCommandHandler = addRequestCommandHandler;
            _bindRequestCommandHandler = bindRequestCommandHandler;
            _searchRequestCommandHandler = searchRequestCommandHandler;
        }

        public async Task<ICollection<LdapPacket>> Handle(LdapPacket ldapPacket)
        {
            switch (ldapPacket.ProtocolOperation.Tag.LdapCommand)
            {
                case LdapCommands.AddRequest:
                    return await _addRequestCommandHandler.Execute(new AddRequestCommand
                    {
                        MessageId = ldapPacket.MessageId,
                        Controls = ldapPacket.Controls,
                        ProtocolOperation = ldapPacket.ProtocolOperation
                    });
                case LdapCommands.BindRequest:
                    return await _bindRequestCommandHandler.Execute(new BindRequestCommand
                    {
                        MessageId = ldapPacket.MessageId,
                        Controls = ldapPacket.Controls,
                        ProtocolOperation = ldapPacket.ProtocolOperation
                    });
                case LdapCommands.SearchRequest:
                    break;
            }

            throw new LdapException(Global.OperationDoesntExist, LDAPResultCodes.ProtocolError, string.Empty);
        }
    }
}
