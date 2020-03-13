// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;

namespace NETCore.Ldap.Persistence.Parameters
{
    public enum SearchScopes
    {
        BaseObject = 0,
        SingleLevel = 1,
        WholeSubtree = 2
    }

    public enum SearchFilters
    {
        And = 0,
        Or = 1,
        Not = 2,
        EqualityMatch = 3,
        SubStrings = 4,
        GreaterOrEqual = 5,
        LessOrEqual = 6,
        Present = 7,
        ApproxMatch = 8
    }

    public class LdapAttributeFilter
    {
        public LdapAttributeFilter()
        {
            Filters = new List<LdapAttributeFilter>();
        }

        public string AttributeName { get; set; }
        public string AttributeValue { get; set; }
        public SearchFilters Filter { get; set; }
        public ICollection<LdapAttributeFilter> Filters { get; set; }
    }

    public class SearchLdapEntriesParameter
    {
        public string DistinguishedName { get; set; }
        public int Level { get; set; }
        public SearchScopes Scope { get; set; }
        public LdapAttributeFilter Filter { get; set; }
    }
}