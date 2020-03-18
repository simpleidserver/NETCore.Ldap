// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace NETCore.Ldap
{
    public static class LdapConstants
    {
        public static class StandardObjectClassOIDS
        {
            public const string ExtensibleOID = "1.3.6.1.4.1.1466.101.120.111";
            public const string TopOID = "2.5.6.0";
            public const string PersonOID = "2.5.6.6";
        }

        public static class StandardMatchingRuleOIDS
        {
            public const string ObjectIdentifierMatchOID = "2.5.13.0";
            public const string OctetStringMatchOID = "2.5.13.17";
            public const string CaseIgnoreMatchOID = "2.5.13.2";
        }

        public static class StandardAttributeTypeOIDS
        {
            public const string ObjectClassOID = "2.5.4.0";
            public const string NameOID = "2.5.4.41";
            public const string SurnameOID = "2.5.4.4";
            public const string UserPasswordOID = "2.5.4.35";
        }

        public static class StandardAttributeSyntaxOIDS
        {
            public const string OID = "1.3.6.1.4.1.1466.115.121.1.38";
            public const string OctetStringOID = "1.3.6.1.4.1.1466.115.121.1.40";
        }

        public static class StandardAttributeTypeNames
        {
            public const string UserPassword = "userPassword";
            public const string ObjectClass = "objectClass";
            public const string Name = "m-name";
            public const string Equality = "m-equality";
        }

        public static class StandardMatchingRuleName
        {
            public const string OctetStringMatch = "octetStringMatch";
        }
    }
}