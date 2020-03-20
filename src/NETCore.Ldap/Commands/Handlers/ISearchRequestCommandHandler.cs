// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.DER;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NETCore.Ldap.Commands.Handlers
{
    public interface ISearchRequestCommandHandler
    {
        Task<ICollection<LdapPacket>> Execute(SearchRequestCommand searchRequestCommand);
    }
}
