// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.DER.Applications;
using System;

namespace NETCore.Ldap.Exceptions
{
    public class LdapException : Exception
    {
        public LdapException(string message, LDAPResultCodes code, string target) : base(message) 
        {
            Code = code;
            Target = target;
        }

        public LDAPResultCodes Code { get; set; }
        public string Target { get; set; }
    }
}
