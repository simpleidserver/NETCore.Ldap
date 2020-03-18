// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace NETCore.Ldap.Domain
{
    public class LDAPEntryAttribute : ICloneable
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public object Clone()
        {
            return new LDAPEntryAttribute
            {
                Id = Id,
                Name = Name,
                Value = Value
            };
        }
    }
}
