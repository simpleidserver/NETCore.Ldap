// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NETCore.Ldap.Domain;
using NETCore.Ldap.Extensions;

namespace NETCore.Ldap.Persistence.InMemory
{
    public class InMemoryLDAPEntryCommandStore : ILDAPEntryCommandStore
    {
        private readonly ConcurrentBag<LDAPEntry> _entries;

        public InMemoryLDAPEntryCommandStore(ConcurrentBag<LDAPEntry> entries)
        {
            _entries = entries;
        }

        public bool Update(LDAPEntry entry)
        {
            var record = _entries.First(e => e.DistinguishedName == entry.DistinguishedName);
            _entries.Remove(record);
            _entries.Add(entry);
            return true;
        }

        public bool Update(IEnumerable<LDAPEntry> entries)
        {
            foreach(var entry in entries)
            {
                Update(entry);
            }

            return true;
        }

        public bool Add(LDAPEntry representation)
        {
            _entries.Add((LDAPEntry)representation.Clone());
            return true;
        }

        public bool Delete(string dn)
        {
            var record = _entries.First(e => e.DistinguishedName == dn);
            _entries.Remove(record);
            return true;
        }

        public Task<int> SaveChanges()
        {
            return Task.FromResult(1);
        }
    }
}
