// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.Domain;
using NETCore.Ldap.Persistence.Parameters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NETCore.Ldap.Persistence
{
    public interface ILDAPEntryQueryStore
    {
        Task<LDAPEntry> Get(string distinguishedName);
        Task<LDAPEntry> GetByAttribute(string key, string value);
        IEnumerable<LDAPEntry> Search(SearchLdapEntriesParameter parameter);
    }
}
