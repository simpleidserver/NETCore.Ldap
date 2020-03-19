// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.Domain;
using System.Collections.Generic;

namespace NETCore.Ldap.MatchingRule
{
    public interface IMatchingRuleHandler
    {
        string Name { get; }
        bool Handle(LDAPEntryAttribute ldapEntryAttribute, ICollection<string> values);
    }
}
