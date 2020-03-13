// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Microsoft.Extensions.DependencyInjection;
using NETCore.Ldap.Domain;
using NETCore.Ldap.Persistence;
using NETCore.Ldap.Persistence.InMemory;
using System.Collections.Generic;

namespace NETCore.Ldap
{
    public class LdapServerBuilder
    {
        private readonly IServiceCollection _services;

        internal LdapServerBuilder(IServiceCollection services)
        {
            _services = services;
        }

        public LdapServerBuilder AddSchemas(ICollection<LDAPSchema> schemas)
        {
            _services.AddSingleton<ILDAPSchemaQueryStore>(new InMemoryLDAPSchemaQueryStore(schemas));
            return this;
        }
    }
}
