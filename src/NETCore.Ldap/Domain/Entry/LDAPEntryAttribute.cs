// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;

namespace NETCore.Ldap.Domain
{
    public class LDAPEntryAttribute : ICloneable
    {
        public LDAPEntryAttribute()
        {
            Values = new List<string>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Values { get; set; }

        public object Clone()
        {
            return new LDAPEntryAttribute
            {
                Id = Id,
                Name = Name,
                Values = Values.ToList()
            };
        }
    }
}
