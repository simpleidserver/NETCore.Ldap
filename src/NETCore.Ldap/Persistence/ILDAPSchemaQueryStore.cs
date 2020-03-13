// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.Domain;
using System.Threading.Tasks;

namespace NETCore.Ldap.Persistence
{
    public interface ILDAPSchemaQueryStore
    {
        Task<LDAPSchema> Get(string distinguishedName);
    }
}
