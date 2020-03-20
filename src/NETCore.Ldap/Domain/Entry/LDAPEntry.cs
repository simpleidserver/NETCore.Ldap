// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;

namespace NETCore.Ldap.Domain
{
    public class LDAPEntry : ICloneable
    {
        public LDAPEntry()
        {
            Attributes = new List<LDAPEntryAttribute>();
        }

        public string DistinguishedName { get; set; }
        public int Level { get; set; }
        public ICollection<LDAPEntryAttribute> Attributes { get; set; }

        public override bool Equals(object obj)
        {
            var source = obj as LDAPEntry;
            if (source == null)
            {
                return false;
            }

            return source.GetHashCode() == GetHashCode();
        }

        public override int GetHashCode()
        {
            return DistinguishedName.GetHashCode();
        }

        public object Clone()
        {
            return new LDAPEntry
            {
                DistinguishedName = DistinguishedName,
                Level = Level,
                Attributes = Attributes.Select(a => (LDAPEntryAttribute)a.Clone()).ToList()
            };
        }
    }
}