// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;

namespace NETCore.Ldap.Persistence.Parameters
{
    public enum SearchScopeTypes
    {
        BaseObject = 0,
        SingleLevel = 1,
        WholeSubtree = 2
    }

    public enum SearchFilterTypes
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

    public class LDAPAttributeFilter
    {
        public LDAPAttributeFilter()
        {
            Filters = new List<LDAPAttributeFilter>();
        }

        public string AttributeName { get; set; }
        public string AttributeValue { get; set; }
        public SearchFilterTypes Type { get; set; }
        public ICollection<LDAPAttributeFilter> Filters { get; set; }
    }

    public class SearchLDAPEntriesParameter
    {
        public string BaseDistinguishedName { get; set; }
        public int SizeLimit { get; set; }
        public SearchScopeTypes Scope { get; set; }
        public LDAPAttributeFilter Filter { get; set; }
    }
}