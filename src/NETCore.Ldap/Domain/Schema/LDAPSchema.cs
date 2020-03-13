// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;

namespace NETCore.Ldap.Domain
{
    /// <summary>
    /// Schema is the collection of attribute type definitions, object class definitions and other information which a server uses to determine
    /// how to match a filter or attribute value assertion(in a compare operation) against the attributes of an entry, and whether to permit add and modify operations.
    /// </summary>
    public class LDAPSchema
    {
        public LDAPSchema()
        {
            ObjectClasses = new List<LDAPObjectClassDefinition>();
        }

        public string DistinguishedName { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public ICollection<LDAPObjectClassDefinition> ObjectClasses { get; set; }
    }
}
