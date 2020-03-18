// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap;
using NETCore.Ldap.Authentication;
using NETCore.Ldap.Commands;
using NETCore.Ldap.Commands.Handlers;
using NETCore.Ldap.Domain;
using NETCore.Ldap.MatchingRule;
using NETCore.Ldap.Persistence;
using NETCore.Ldap.Persistence.InMemory;
using NETCore.Ldap.Services;
using System;
using System.Collections.Concurrent;

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
            else
            {
                services.Configure<LdapServerOptions>(opt => { });
            }

            var builder = new LdapServerBuilder(services);
            services.AddAuthentication()
                .AddMatchingRule()
                .AddLdapServer()
                .AddLdapCommands()
                .AddLdapStores()
                .AddServices()
                .AddLogging();
            return builder;
        }

        private static IServiceCollection AddAuthentication(this IServiceCollection services)
        {
            services.AddTransient<IAuthenticationHandlerFactory, AuthenticationHandlerFactory>();
            services.AddTransient<IAuthenticationHandler, SimpleAuthenticationHandler>();
            return services;
        }

        private static IServiceCollection AddMatchingRule(this IServiceCollection services)
        {
            services.AddTransient<IMatchingRuleHandlerFactory, MatchingRuleHandlerFactory>();
            services.AddTransient<IMatchingRuleHandler, OctetStringMatchHandler>();
            return services;
        }

        private static IServiceCollection AddLdapServer(this IServiceCollection services)
        {
            services.AddTransient<ILdapServer, LdapServer>();
            return services;
        }

        private static IServiceCollection AddLdapCommands(this IServiceCollection services)
        {
            services.AddTransient<ILdapService, LdapService>();
            services.AddTransient<IAddRequestCommandHandler, AddRequestCommandHandler>();
            services.AddTransient<IBindRequestCommandHandler, BindRequestCommandHandler>();
            services.AddTransient<ISearchRequestCommandHandler, SearchRequestCommandHandler>();
            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IPasswordService, PasswordService>();
            return services;
        }

        private static IServiceCollection AddLdapStores(this IServiceCollection services)
        {
            var entries = new ConcurrentBag<LDAPEntry>();
            services.AddSingleton<ILDAPEntryQueryStore>(new InMemoryLDAPEntryQueryStore(entries));
            services.AddSingleton<ILDAPEntryCommandStore>(new InMemoryLDAPEntryCommandStore(entries));
            return services;
        }
    }
}