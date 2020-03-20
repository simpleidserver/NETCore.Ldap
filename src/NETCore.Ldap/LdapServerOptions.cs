// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Net;

namespace NETCore.Ldap
{
    public class LdapServerOptions
    {
        public LdapServerOptions()
        {
            IpAdr = IPAddress.Parse("127.0.0.1");
            Port = 389;
            VendorName = "SimpleIdServer";
            VendorVersion = "1.0.0";
            SupportedLDAPVersion = 3;
            NamingContexts = new List<string>
            {
                "dc=example,dc=com",
                "ou=config",
                "ou=schema",
                "ou=system",
                "cn=schema"
            };
            SubSchemaSubEntry = "cn=schema";
            UserPasswordAttributeName = LdapConstants.StandardAttributeTypeNames.UserPassword;
            ObjectClassAttributeName = LdapConstants.StandardAttributeTypeNames.ObjectClass;
            MustAttributeName = LdapConstants.StandardAttributeTypeNames.Must;
            MayAttributeName = LdapConstants.StandardAttributeTypeNames.May;
            NameAttributeName = LdapConstants.StandardAttributeTypeNames.Name;
            EqualityAttributeName = LdapConstants.StandardAttributeTypeNames.Equality;
            SingleValueAttributeName = LdapConstants.StandardAttributeTypeNames.SingleValue;
            SyntaxAttributeName = LdapConstants.StandardAttributeTypeNames.Syntax;
        }

        /// <summary>
        /// IP Address (default value is 127.0.0.1).
        /// </summary>
        public IPAddress IpAdr { get; set; }
        /// <summary>
        /// Port (default value is 389).
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// UserPassword attribute name used during the authentication.
        /// </summary>
        public string UserPasswordAttributeName { get; set; }
        /// <summary>
        /// ObjectClass attribute name.
        /// </summary>
        public string ObjectClassAttributeName { get; set; }
        /// <summary>
        /// Vendor name.
        /// </summary>
        public string VendorName { get; set; }
        /// <summary>
        /// Version version.
        /// </summary>
        public string VendorVersion { get; set; }
        /// <summary>
        /// Supported LDAP version.
        /// </summary>
        public int SupportedLDAPVersion { get; set; }
        /// <summary>
        /// Naming contexts.
        /// </summary>
        public ICollection<string> NamingContexts { get; set; }
        /// <summary>
        /// Sub schema sub entry.
        /// </summary>
        public string SubSchemaSubEntry { get; set; }
        /// <summary>
        /// May attribute name.
        /// </summary>
        public string MayAttributeName { get; set; }
        /// <summary>
        /// Must attribute name.
        /// </summary>
        public string MustAttributeName { get; set; }
        /// <summary>
        /// Name attribute name.
        /// </summary>
        public string NameAttributeName { get; set; }
        /// <summary>
        /// Equality attribute name.
        /// </summary>
        public string EqualityAttributeName { get; set; }
        /// <summary>
        /// SingleValue attribute name.
        /// </summary>
        public string SingleValueAttributeName { get; set; }
        /// <summary>
        /// Syntax attribute name.
        /// </summary>
        public string SyntaxAttributeName { get; set; }
    }
}
