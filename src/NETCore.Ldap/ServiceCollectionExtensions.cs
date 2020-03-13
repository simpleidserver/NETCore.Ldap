// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap;
using NETCore.Ldap.Commands;
using NETCore.Ldap.Commands.Handlers;
using NETCore.Ldap.Domain;
using NETCore.Ldap.Persistence;
using NETCore.Ldap.Persistence.InMemory;
using System;
using System.Collections.Generic;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static LdapServerBuilder AddLdapServer(this IServiceCollection services, Action<LdapServerOptions> callback = null)
        {
            if (callback != null)
            {
                services.Configure(callback);
            }

            var builder = new LdapServerBuilder(services);
            services.AddLdapCommands()
                .AddLdapStores();
            return builder;
        }

        private static IServiceCollection AddLdapCommands(this IServiceCollection services)
        {
            services.AddTransient<ILdapService, LdapService>();
            services.AddTransient<IAddRequestCommandHandler, AddRequestCommandHandler>();
            return services;
        }

        private static IServiceCollection AddLdapStores(this IServiceCollection services)
        {
            var ldapSchemas = new List<LDAPSchema>();
            services.AddSingleton<ILDAPSchemaQueryStore>(new InMemoryLDAPSchemaQueryStore(ldapSchemas));
            return services;
        }
    }
}