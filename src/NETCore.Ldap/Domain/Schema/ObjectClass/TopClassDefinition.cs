// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;

namespace NETCore.Ldap.Domain
{
    public class TopClassDefinition : LDAPObjectClassDefinition
    {
        public TopClassDefinition()
        {
            OID = LdapConstants.StandardObjectClassOIDS.TopOID;
            Name = LdapConstants.StandardObjectClassNames.Top;
            Kind = LDAPObjectClassKinds.Abstract;
            Must = new List<string>
            {
                LdapConstants.StandardAttributeNames.ObjectClass
            };
        }
    }
}
