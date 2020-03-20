// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.DER.Applications.Filters;
using NETCore.Ldap.DER.Applications.Requests;
using NETCore.Ldap.DER.Universals;
using System;
using System.Collections.Generic;

namespace NETCore.Ldap.Builders
{
    public class SearchRequestBuilder
    {
        private readonly string _baseObject;
        private readonly SearchRequestScopes _scope;
        private readonly SearchRequestDeferAliases _deferAlias;
        private readonly int _sizeLimit;
        private readonly int _timeLimit;
        private readonly bool _typesOnly;
        private readonly ICollection<string> _attributes;
        private SearchRequestFilter _filter;

        internal SearchRequestBuilder(string baseObject, SearchRequestScopes scope, SearchRequestDeferAliases deferAlias, int sizeLimit, int timeLimit, bool typesOnly, ICollection<string> attributes)
        {
            _baseObject = baseObject;
            _scope = scope;
            _deferAlias = deferAlias;
            _sizeLimit = sizeLimit;
            _timeLimit = timeLimit;
            _typesOnly = typesOnly;
            _attributes = attributes;
        }

        public void SetEqualFilter(string name, string value)
        {
            var builder = new FilterBuilder();
            _filter = builder.BuildEqualFilter(name, value);
        }

        public void SetGreaterOrEqualFilter(string name, string value)
        {
            var builder = new FilterBuilder();
            _filter = builder.BuildGreaterOrEqualFilter(name, value);
        }

        public void SetLessOrEqualFilter(string name, string value)
        {
            var builder = new FilterBuilder();
            _filter = builder.BuildLessOrEqualFilter(name, value);
        }

        public void SetApproxMatchFilter(string name, string value)
        {
            var builder = new FilterBuilder();
            _filter = builder.BuildApproxMatchFilter(name, value);
        }

        public void SetAndFilter(IEnumerable<Func<FilterBuilder, SearchRequestFilter>> callbacks)
        {
            var builder = new FilterBuilder();
            _filter = builder.BuildAndFilter(callbacks);
        }

        public void SetOrFilter(IEnumerable<Func<FilterBuilder, SearchRequestFilter>> callbacks)
        {
            var builder = new FilterBuilder();
            _filter = builder.BuildOrFilter(callbacks);
        }

        public void SetPresentFilter(string name)
        {
            var builder = new FilterBuilder();
            _filter = builder.BuildPresentFilter(name);
        }

        internal SearchRequest Build()
        {
            var result = new SearchRequest
            {
                BaseObject = new DEROctetString(_baseObject),
                DeferAlias = new DEREnumerated<SearchRequestDeferAliases>(_deferAlias),
                Scope = new DEREnumerated<SearchRequestScopes>(_scope),
                SizeLimit = new DERInteger(_sizeLimit),
                TimeLimit = new DERInteger(_timeLimit),
                TypesOnly = new DERBoolean(_typesOnly),
                Attributes = new DERSequence<DEROctetString>(),
                Filter = _filter
            };
            foreach(var attr in _attributes)
            {
                result.Attributes.Values.Add(new DEROctetString(attr));
            }

            return result;
        }
    }
}
