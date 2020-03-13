// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;

namespace NETCore.Ldap.Domain
{
    public class LDAPEntry
    {
        public LDAPEntry()
        {
            Attributes = new List<LDAPEntryAttribute>();
        }

        public string DistinguishedName { get; set; }
        public ICollection<LDAPEntryAttribute> Attributes { get; set; }
    }
}