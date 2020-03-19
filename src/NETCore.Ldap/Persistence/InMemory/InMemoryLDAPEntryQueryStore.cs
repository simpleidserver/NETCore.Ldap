// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.Domain;
using NETCore.Ldap.Persistence.Parameters;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NETCore.Ldap.Persistence.InMemory
{
    public class InMemoryLDAPEntryQueryStore : ILDAPEntryQueryStore
    {
        private readonly ConcurrentBag<LDAPEntry> _entries;

        public InMemoryLDAPEntryQueryStore(ConcurrentBag<LDAPEntry> entries)
        {
            _entries = entries;
        }

        public Task<LDAPEntry> Get(string distinguishedName)
        {
            return Task.FromResult(_entries.FirstOrDefault(e => e.DistinguishedName == distinguishedName));
        }

        public Task<LDAPEntry> GetByAttribute(string key, string value)
        {
            return Task.FromResult(_entries.FirstOrDefault(e => e.Attributes.Any(attr => attr.Name == key && attr.Values.Contains(value))));
        }

        public Task<ICollection<LDAPEntry>> GetByAttributes(ICollection<KeyValuePair<string, string>> attributes)
        {
            ICollection<LDAPEntry> result = _entries.Where(e => e.Attributes.Any(attr => attributes.Any(a => a.Key == attr.Name && attr.Values.Contains(a.Value)))).ToList();
            return Task.FromResult(result);
        }

        public IEnumerable<LDAPEntry> Search(SearchLdapEntriesParameter parameter)
        {
            IEnumerable<LDAPEntry> result = null;
            /*
            switch (parameter.Scope)
            {
                case SearchScopes.BaseObject:
                    result = _entries.Where(r => r.DistinguishedName == parameter.DistinguishedName && r.Level == parameter.Level);
                    break;
                case SearchScopes.SingleLevel:
                    var nextLevel = parameter.Level + 1;
                    result = _entries.Where(r => r.DistinguishedName.EndsWith(parameter.DistinguishedName) && r.Level == nextLevel);
                    break;
                case SearchScopes.WholeSubtree:
                    result = _entries.Where(r => r.DistinguishedName.EndsWith(parameter.DistinguishedName) && r.Level >= parameter.Level);
                    break;
            }
            */
            if (parameter.Filter != null)
            {
                var exprTree = (Expression<Func<LDAPEntry, bool>>)BuildExpression(parameter.Filter);
                var compiledExprTree = exprTree.Compile();
                result = result.Where(compiledExprTree);
            }

            return result;
        }

        private Expression BuildExpression(LdapAttributeFilter ldapAttributeFilter, ParameterExpression parentExpression = null)
        {
            ParameterExpression rootExpression = Expression.Parameter(typeof(LDAPEntry), "r");
            if (parentExpression != null)
            {
                rootExpression = parentExpression;
            }

            Expression comparison = null;
            if (ldapAttributeFilter.Filter == SearchFilters.Or || ldapAttributeFilter.Filter == SearchFilters.And)
            {
                int i = 0;
                Expression leftExpr = null;
                foreach (var child in ldapAttributeFilter.Filters)
                {
                    i++;
                    var childExpression = BuildExpression(child, rootExpression);
                    if (i == 1)
                    {
                        leftExpr = childExpression;
                    }
                    else
                    {
                        switch (ldapAttributeFilter.Filter)
                        {
                            case SearchFilters.And:
                                leftExpr = Expression.And(leftExpr, childExpression);
                                break;
                            case SearchFilters.Or:
                                leftExpr = Expression.Or(leftExpr, childExpression);
                                break;
                        }
                    }
                }

                if (parentExpression != null)
                {
                    return leftExpr;
                }

                Expression<Func<LDAPEntry, bool>> result = Expression.Lambda<Func<LDAPEntry, bool>>(leftExpr, rootExpression);
                return result;
            }

            var memberExpression = Expression.PropertyOrField(rootExpression, "Attributes");
            var attribute = Expression.Parameter(typeof(LDAPEntryAttribute), "a");
            var attributeName = Expression.PropertyOrField(attribute, "Name");
            var attributeValue = Expression.PropertyOrField(attribute, "Value");
            var nameConstant = Expression.Constant(ldapAttributeFilter.AttributeName, typeof(string));
            var valueConstant = Expression.Constant(ldapAttributeFilter.AttributeValue, typeof(string));
            var equalityName = Expression.Call(typeof(string), "Equals", null, attributeName, nameConstant, Expression.Constant(StringComparison.CurrentCultureIgnoreCase));
            var equalityValue = Expression.Call(typeof(string), "Equals", null, attributeValue, valueConstant, Expression.Constant(StringComparison.CurrentCultureIgnoreCase));

            switch (ldapAttributeFilter.Filter)
            {
                case SearchFilters.Present:
                    comparison = equalityName;
                    break;
                case SearchFilters.EqualityMatch:
                    comparison = Expression.AndAlso(equalityName, equalityValue);
                    break;
                case SearchFilters.GreaterOrEqual:
                    comparison = Expression.GreaterThanOrEqual(equalityName, equalityValue);
                    break;
                case SearchFilters.LessOrEqual:
                    comparison = Expression.LessThanOrEqual(equalityName, equalityValue);
                    break;
            }

            var predicate = Expression.Lambda<Func<LDAPEntryAttribute, bool>>(comparison, attribute);
            var anyCall = Expression.Call(typeof(Enumerable), "Any", new[] { typeof(LDAPEntryAttribute) }, memberExpression, predicate);
            if (parentExpression != null)
            {
                return anyCall;
            }

            return Expression.Lambda<Func<LDAPEntry, bool>>(anyCall, rootExpression);
        }
    }
}
