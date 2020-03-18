// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.DER.Applications.AuthChoices;
using NETCore.Ldap.DER.Applications.Requests;
using NETCore.Ldap.DER.Universals;

namespace NETCore.Ldap.Builders
{
    public class BindRequestBuilder
    {
        private readonly int _version;
        private readonly string _name;
        private BaseAuthChoice _authentication;

        internal BindRequestBuilder(int version, string name)
        {
            _version = version;
            _name = name;
        }

        public BindRequestBuilder SetSimpleAuthentication(string value)
        {
            _authentication = new SimpleAuthChoice
            {
                Value = new DEROctetString(value)
            };
            return this;
        }

        internal BindRequest Build()
        {
            return new BindRequest()
            {
                Name = new DEROctetString(_name),
                Version = new DERInteger(_version),
                Authentication = _authentication
            };
        }
    }
}
