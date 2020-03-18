// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;

namespace NETCore.Ldap.Parser
{
    public class ChangeAdd : IChangeRecord
    {
        public ChangeAdd(string dn)
        {
            DistinguishedName = dn;
            Attributes = new List<LDIFAttribute>();
        }
                
        public string DistinguishedName { get; private set; }
        public ICollection<LDIFAttribute> Attributes { get; private set; }
        public ChangeRecordTypes Type => ChangeRecordTypes.ADD;
    }
}
