// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.DER.Applications.Requests;

namespace NETCore.Ldap.Authentication
{
    public interface IAuthenticationHandlerFactory
    {
        IAuthenticationHandler Build(BindRequestAuthenticationChoices authChoice);
    }
}
