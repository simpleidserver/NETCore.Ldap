// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace NETCore.Ldap
{
    public static class LdapConstants
    {
        public static class StandardObjectClassNames
        {
            public const string ExtensibleName = "extensibleObject";
            public const string PersonName = "person";
            public const string Top = "top";
        }

        public static class StandardObjectClassOIDS
        {
            public const string ExtensibleOID = "1.3.6.1.4.1.1466.101.120.111";
            public const string TopOID = "2.5.6.0";
            public const string PersonOID = "2.5.6.6";
        }

        public static class StandardAttributeNames
        {
            public const string ObjectClass = "objectClass";
            public const string Surname = "sn";
            public const string CommonName = "cn";
            public const string UserPassword = "userPassword";
            public const string TelephoneNumber = "telephoneNumber";
            public const string SeeAlso = "seeAlso";
            public const string Description = "description";
        }
    }
}