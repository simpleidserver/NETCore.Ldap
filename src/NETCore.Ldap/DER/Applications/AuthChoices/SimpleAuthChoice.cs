// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.DER.Applications.Requests;
using NETCore.Ldap.DER.Universals;
using System.Collections.Generic;

namespace NETCore.Ldap.DER.Applications.AuthChoices
{
    public class SimpleAuthChoice : BaseAuthChoice
    {
        public SimpleAuthChoice()
        {
        }

        public override BindRequestAuthenticationChoices Type => BindRequestAuthenticationChoices.SIMPLE;

        public DEROctetString Value { get; set; }

        public static SimpleAuthChoice Extract(ICollection<byte> buffer)
        {
            var result = new SimpleAuthChoice();
            result.Value = DEROctetString.Extract(buffer);
            return result;
        }

        public override ICollection<byte> Serialize()
        {
            var result = new List<byte>();
            Value.Tag = new DERTag
            {
                PcType = PcTypes.Primitive,
                TagClass = ClassTags.ContextSpecific,
                TagNumber = (int)BindRequestAuthenticationChoices.SIMPLE
            };
            result.AddRange(Value.Serialize());
            return result;
        }
    }
}
