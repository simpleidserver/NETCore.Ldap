// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Microsoft.Extensions.DependencyInjection;
using NETCore.Ldap.Domain;
using NETCore.Ldap.Extensions;
using NETCore.Ldap.Parser;
using NETCore.Ldap.Persistence;
using NETCore.Ldap.Persistence.InMemory;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace NETCore.Ldap
{
    public class LdapServerBuilder
    {
        private readonly IServiceCollection _services;

        internal LdapServerBuilder(IServiceCollection services)
        {
            _services = services;
        }

        public LdapServerBuilder ImportLDIF(string filePath)
        {
            var parser = new LDIFParser();
            var operations = parser.Parse(filePath);
            var entries = new ConcurrentBag<LDAPEntry>();
            foreach(var operation in operations)
            {
                if (operation.Type == ChangeRecordTypes.ADD)
                {
                    var changeAdd = operation as ChangeAdd;
                    entries.Add(new LDAPEntry
                    {
                        DistinguishedName = changeAdd.DistinguishedName,
                        Level = changeAdd.DistinguishedName.ComputeLevel(),
                        Attributes = changeAdd.Attributes.Select(a => new LDAPEntryAttribute
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = a.Type,
                            Values = new List<string> { a.Value }
                        }).ToList()
                    });
                }
            }

            _services.AddSingleton<ILDAPEntryQueryStore>(new InMemoryLDAPEntryQueryStore(entries));
            _services.AddSingleton<ILDAPEntryCommandStore>(new InMemoryLDAPEntryCommandStore(entries));
            return this;
        }
    }
}
