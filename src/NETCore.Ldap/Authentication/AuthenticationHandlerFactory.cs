// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.DER.Applications.Requests;
using System.Collections.Generic;
using System.Linq;

namespace NETCore.Ldap.Authentication
{
    public class AuthenticationHandlerFactory : IAuthenticationHandlerFactory
    {
        private readonly IEnumerable<IAuthenticationHandler> _authenticationHandlers;

        public AuthenticationHandlerFactory(IEnumerable<IAuthenticationHandler> authenticationHandlers)
        {
            _authenticationHandlers = authenticationHandlers;
        }

        public IAuthenticationHandler Build(BindRequestAuthenticationChoices authChoice)
        {
            return _authenticationHandlers.FirstOrDefault(a => a.AuthChoice == authChoice);
        }
    }
}
