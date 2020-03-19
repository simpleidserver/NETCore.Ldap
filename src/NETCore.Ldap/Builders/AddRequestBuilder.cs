// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.DER.Applications;
using NETCore.Ldap.DER.Applications.Requests;
using NETCore.Ldap.DER.Universals;
using System.Collections.Generic;

namespace NETCore.Ldap.Builders
{
    public class AddRequestBuilder
    {
        private readonly string _distinguishedName;
        private readonly DERSequence<PartialAttribute> _attributes;

        internal AddRequestBuilder(string distinguishedName)
        {
            _distinguishedName = distinguishedName;
            _attributes = new DERSequence<PartialAttribute>();
        }

        public AddRequestBuilder AddAttribute(string type, ICollection<string> values)
        {
            var attribute = new PartialAttribute
            {
                Type = new DEROctetString(type),
                Vals = new DERSet<DEROctetString>()
            };
            foreach(var value in values)
            {
                attribute.Vals.Values.Add(new DEROctetString(value));
            }

            _attributes.Values.Add(attribute);
            return this;
        }

        internal AddRequest Build()
        {
            return new AddRequest
            {
                Entry = new DEROctetString(_distinguishedName),
                Attributes = _attributes
            };
        }
    }
}
