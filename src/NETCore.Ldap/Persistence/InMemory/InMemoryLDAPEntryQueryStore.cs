// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.Domain;
using NETCore.Ldap.Extensions;
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

        public Task<IEnumerable<LDAPEntry>> Search(SearchLDAPEntriesParameter parameter)
        {
            IEnumerable<LDAPEntry> result = null;
            var level = parameter.BaseDistinguishedName.ComputeLevel();
            switch (parameter.Scope)
            {
                case SearchScopeTypes.BaseObject:
                    result = _entries.Where(r => r.DistinguishedName == parameter.BaseDistinguishedName);
                    break;
                case SearchScopeTypes.SingleLevel:
                    result = _entries.Where(r => r.DistinguishedName.EndsWith(parameter.BaseDistinguishedName) && r.Level == level + 1);
                    break;
                case SearchScopeTypes.WholeSubtree:
                    result = _entries.Where(r => r.DistinguishedName.EndsWith(parameter.BaseDistinguishedName));
                    break;
            }

            if (parameter.Filter != null)
            {
                var exprTree = (Expression<Func<LDAPEntry, bool>>)BuildExpression(parameter.Filter);
                var compiledExprTree = exprTree.Compile();
                result = result.Where(compiledExprTree);
            }

            if (parameter.SizeLimit > 0)
            {
                result = result.Take(parameter.SizeLimit);
            }

            return Task.FromResult(result);
        }

        private Expression BuildExpression(LDAPAttributeFilter ldapAttributeFilter, ParameterExpression parentExpression = null)
        {
            ParameterExpression rootExpression = Expression.Parameter(typeof(LDAPEntry), "r");
            if (parentExpression != null)
            {
                rootExpression = parentExpression;
            }

            Expression comparison = null;
            if (ldapAttributeFilter.Type == SearchFilterTypes.Or || ldapAttributeFilter.Type == SearchFilterTypes.And)
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
                        switch (ldapAttributeFilter.Type)
                        {
                            case SearchFilterTypes.And:
                                leftExpr = Expression.And(leftExpr, childExpression);
                                break;
                            case SearchFilterTypes.Or:
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

            var lst = new List<string>();
            var methodInfo = typeof(List<string>).GetMethod("Contains", new Type[] { typeof(string) });
            var memberExpression = Expression.PropertyOrField(rootExpression, "Attributes");
            var attribute = Expression.Parameter(typeof(LDAPEntryAttribute), "a");
            var attributeName = Expression.PropertyOrField(attribute, "Name");
            var attributesValue = Expression.PropertyOrField(attribute, "Values");
            var nameConstant = Expression.Constant(ldapAttributeFilter.AttributeName, typeof(string));
            var valueConstant = Expression.Constant(ldapAttributeFilter.AttributeValue, typeof(string));
            var equalityName = Expression.Call(typeof(string), "Equals", null, attributeName, nameConstant, Expression.Constant(StringComparison.CurrentCultureIgnoreCase));
            var equalityValue = Expression.Call(attributesValue, methodInfo, valueConstant);
            switch (ldapAttributeFilter.Type)
            {
                case SearchFilterTypes.Present:
                    comparison = equalityName;
                    break;
                case SearchFilterTypes.EqualityMatch:
                    comparison = Expression.AndAlso(equalityName, equalityValue);
                    break;
                case SearchFilterTypes.GreaterOrEqual:
                    comparison = Expression.GreaterThanOrEqual(equalityName, equalityValue);
                    break;
                case SearchFilterTypes.LessOrEqual:
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
