// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace NETCore.Ldap.Domain
{
    /// <summary>
    /// The 'extensibleObject' auxiliary object class allows entries that belong to it to hold any user attribute.
    /// </summary>
    public class ExtensibleObjectClassDefinition : LDAPObjectClassDefinition
    {
        public ExtensibleObjectClassDefinition()
        {
            OID = LdapConstants.StandardObjectClassOIDS.ExtensibleOID;
            Name = LdapConstants.StandardObjectClassNames.ExtensibleName;
            Supertype = LdapConstants.StandardObjectClassNames.Top;
            Kind = LDAPObjectClassKinds.Auxiliary;
        }
    }
}
