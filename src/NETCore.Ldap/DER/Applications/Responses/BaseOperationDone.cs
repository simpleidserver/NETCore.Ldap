// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace NETCore.Ldap.DER.Applications.Responses
{
    public abstract class BaseOperationDone : DERApplicationType
    {
        public LDAPResult Result { get; set; }
    }
}