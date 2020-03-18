// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.DER.Applications.Requests;
using NETCore.Ldap.Domain;
using System.Threading.Tasks;

namespace NETCore.Ldap.Authentication
{
    public interface IAuthenticationHandler
    {
        BindRequestAuthenticationChoices AuthChoice { get; }
        Task Authenticate(LDAPEntry entry, BindRequest bindRequest);
    }
}
