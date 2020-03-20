// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.DER.Applications.Filters;
using NETCore.Ldap.DER.Universals;
using System;
using System.Collections.Generic;

namespace NETCore.Ldap.Builders
{
    public class FilterBuilder
    {
        public SearchRequestFilter BuildAndFilter(IEnumerable<Func<FilterBuilder, SearchRequestFilter>> callbacks)
        {
            var filters = new List<SearchRequestFilter>();
            foreach(var cb in callbacks)
            {
                filters.Add(cb(this));
            }

            var filter = new SearchRequestFilter
            {
                Type = SearchRequestFilterTypes.And,
                Filters = filters
            };
            return filter;
        }
        
        public SearchRequestFilter BuildOrFilter(IEnumerable<Func<FilterBuilder, SearchRequestFilter>> callbacks)
        {
            var filters = new List<SearchRequestFilter>();
            foreach (var cb in callbacks)
            {
                filters.Add(cb(this));
            }

            var filter = new SearchRequestFilter
            {
                Type = SearchRequestFilterTypes.Or,
                Filters = filters
            };
            return filter;
        }

        public SearchRequestFilter BuildPresentFilter(string name)
        {
            var filter = new SearchRequestFilter
            {
                Type = SearchRequestFilterTypes.Present,
                Value = name
            };
            return filter;
        }

        public SearchRequestFilter BuildEqualFilter(string name, string value)
        {
            var filter = new SearchRequestFilter
            {
                Type = SearchRequestFilterTypes.EqualityMatch,
                Attribute = new AttributeValueAssertion
                {
                    AttributeDescription = new DEROctetString(name),
                    AssertionValue = new DEROctetString(value)
                }
            };
            return filter;
        }

        public SearchRequestFilter BuildGreaterOrEqualFilter(string name, string value)
        {
            var filter = new SearchRequestFilter
            {
                Type = SearchRequestFilterTypes.GreaterOrEqual,
                Attribute = new AttributeValueAssertion
                {
                    AttributeDescription = new DEROctetString(name),
                    AssertionValue = new DEROctetString(value)
                }
            };
            return filter;
        }

        public SearchRequestFilter BuildLessOrEqualFilter(string name, string value)
        {
            var filter = new SearchRequestFilter
            {
                Type = SearchRequestFilterTypes.LessOrEqual,
                Attribute = new AttributeValueAssertion
                {
                    AttributeDescription = new DEROctetString(name),
                    AssertionValue = new DEROctetString(value)
                }
            };
            return filter;
        }

        public SearchRequestFilter BuildApproxMatchFilter(string name, string value)
        {
            var filter = new SearchRequestFilter
            {
                Type = SearchRequestFilterTypes.ApproxMatch,
                Attribute = new AttributeValueAssertion
                {
                    AttributeDescription = new DEROctetString(name),
                    AssertionValue = new DEROctetString(value)
                }
            };
            return filter;
        }
    }
}
