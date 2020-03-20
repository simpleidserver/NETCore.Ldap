// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace NETCore.Ldap
{
    public static class LdapConstants
    {
        public static class StandardAttributeSyntaxOIDS
        {
            public const string OID = "1.3.6.1.4.1.1466.115.121.1.38";
            public const string OctetStringOID = "1.3.6.1.4.1.1466.115.121.1.40";
            public const string BooleanOID = "1.3.6.1.4.1.1466.115.121.1.7";
        }

        public static class StandardAttributeTypeNames
        {
            public const string UserPassword = "userPassword";
            public const string ObjectClass = "objectClass";
            public const string VendorName = "vendorName";
            public const string VendorVersion = "vendorVersion";
            public const string SupportedLDAPVersion = "supportedLDAPVersion";
            public const string SupportedSASLMechanisms = "supportedSASLMechanisms";
            public const string NamingContexts = "namingContexts";
            public const string SubschemaSubentry = "subschemaSubentry";
            public const string Name = "m-name";
            public const string Equality = "m-equality";
            public const string Must = "m-must";
            public const string May = "m-may";
            public const string SingleValue = "m-singlevalue";
            public const string Syntax = "m-syntax";
        }

        public static class StandardMatchingRuleName
        {
            public const string OctetStringMatch = "octetStringMatch";
        }
    }
}