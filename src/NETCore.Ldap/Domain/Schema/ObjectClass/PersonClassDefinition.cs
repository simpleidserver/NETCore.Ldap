// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;

namespace NETCore.Ldap.Domain
{
    /// <summary>
    /// The 'person' object class is the basis of an entry that represents a human being.
    /// </summary>
    public class PersonClassDefinition : LDAPObjectClassDefinition
    {
        public PersonClassDefinition()
        {
            OID = LdapConstants.StandardObjectClassOIDS.PersonOID;
            Name = LdapConstants.StandardObjectClassNames.PersonName;
            Kind = LDAPObjectClassKinds.Structural;
            Supertype = LdapConstants.StandardObjectClassNames.Top;
            Must = new List<string>
            {
                LdapConstants.StandardAttributeNames.Surname,
                LdapConstants.StandardAttributeNames.CommonName
            };
            May = new List<string>
            {
                LdapConstants.StandardAttributeNames.UserPassword,
                LdapConstants.StandardAttributeNames.TelephoneNumber,
                LdapConstants.StandardAttributeNames.SeeAlso,
                LdapConstants.StandardAttributeNames.Description
            };
        }
    }
}
