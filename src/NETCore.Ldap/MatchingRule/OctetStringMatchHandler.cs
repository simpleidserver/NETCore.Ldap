// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.Domain;
using System.Collections.Generic;
using System.Linq;

namespace NETCore.Ldap.MatchingRule
{
    public class OctetStringMatchHandler : IMatchingRuleHandler
    {
        public string Name => LdapConstants.StandardMatchingRuleName.OctetStringMatch;

        public bool Handle(LDAPEntryAttribute ldapEntryAttribute, ICollection<string> values)
        {
            return ldapEntryAttribute.Values.All(v => values.Contains(v));
        }
    }
}