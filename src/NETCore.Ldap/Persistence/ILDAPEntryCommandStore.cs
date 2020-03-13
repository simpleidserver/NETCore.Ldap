// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NETCore.Ldap.Persistence
{
    public interface ILDAPEntryCommandStore
    {
        bool Update(LDAPEntry entry);
        bool Update(IEnumerable<LDAPEntry> entries);
        bool Delete(string dn);
        bool Add(LDAPEntry representation);
        Task<int> SaveChanges();
    }
}