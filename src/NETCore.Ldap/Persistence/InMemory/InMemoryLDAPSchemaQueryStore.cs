// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Ldap.Persistence.InMemory
{
    public class InMemoryLDAPSchemaQueryStore : ILDAPSchemaQueryStore
    {
        private readonly ICollection<LDAPSchema> _schemas;

        public InMemoryLDAPSchemaQueryStore(ICollection<LDAPSchema> schemas)
        {
            _schemas = schemas;
        }

        public Task<LDAPSchema> Get(string distinguishedName)
        {
            return Task.FromResult(_schemas.FirstOrDefault(s => s.DistinguishedName == distinguishedName));
        }
    }
}
