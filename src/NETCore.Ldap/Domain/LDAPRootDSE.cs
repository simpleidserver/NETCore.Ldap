// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;

namespace NETCore.Ldap.Domain
{
    public class LDAPRootDSE
    {
        /// <summary>
        /// These are the base DNs for the trees containing data that clients are generally intended to interact with.
        /// </summary>
        public ICollection<string> NamingContexts { get; set; }
        /// <summary>
        /// This specifies the versions of the LDAP protocol that the server supports.
        /// </summary>
        public int SupportedLDAPVersion { get; set; }
        /// <summary>
        /// This specifies the OIDs of all of the request controls that the server is willing to accept.
        /// </summary>
        public ICollection<string> SupportedControls { get; set; }
        /// <summary>
        /// This specifies the names of all the SASL mechanisms that the server can support for use in bind operations.
        /// </summary>
        public ICollection<string> SupportedSASLMechanisms { get; set; }
        /// <summary>
        ///  This specifies the name of the vendor (or organization) that created the directory server software.
        /// </summary>
        public string VendorName { get; set; }
        /// <summary>
        /// This specifies the version for the directory server software.
        /// </summary>
        public string VendorVersion { get; set; }
    }
}
