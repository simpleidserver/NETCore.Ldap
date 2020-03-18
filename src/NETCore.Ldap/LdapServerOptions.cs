// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Net;

namespace NETCore.Ldap
{
    public class LdapServerOptions
    {
        public LdapServerOptions()
        {
            IpAdr = IPAddress.Parse("127.0.0.1");
            Port = 389;
            UserPasswordAttributeName = LdapConstants.StandardAttributeTypeNames.UserPassword;
            ObjectClassAttributeName = LdapConstants.StandardAttributeTypeNames.ObjectClass;
            NameAttributeName = LdapConstants.StandardAttributeTypeNames.Name;
            EqualityAttributeName = LdapConstants.StandardAttributeTypeNames.Equality;
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
        /// Name attribute name.
        /// </summary>
        public string NameAttributeName { get; set; }
        /// <summary>
        /// Equality attribute name.
        /// </summary>
        public string EqualityAttributeName { get; set; }
    }
}
