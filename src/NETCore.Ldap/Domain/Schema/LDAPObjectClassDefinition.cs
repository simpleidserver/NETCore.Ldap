// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;

namespace NETCore.Ldap.Domain
{
    /// <summary>
    /// An object class is "an identified family of objects (or conceivable objects) that share certain characteristics.
    /// </summary>
    public class LDAPObjectClassDefinition
    {
        public LDAPObjectClassDefinition()
        {
            Must = new List<string>();
            May = new List<string>();
        }

        /// <summary>
        /// Identifier used to name an object.
        /// </summary>
        public string OID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// Indicate that the object class is obsolete.
        /// </summary>
        public bool Obsolete { get; set; }
        /// <summary>
        /// Specifies the direct superclasses of this object class;
        /// </summary>
        public string Supertype { get; set; }
        /// <summary>
        /// Kind of class.
        /// </summary>
        public LDAPObjectClassKinds Kind { get; set; }
        /// <summary>
        /// Required attributes.
        /// </summary>
        public ICollection<string> Must { get; set; }
        /// <summary>
        /// Optional attributes.
        /// </summary>
        public ICollection<string> May { get; set; }
    }
}
