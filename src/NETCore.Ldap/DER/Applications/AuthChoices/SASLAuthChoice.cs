// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.DER.Applications.Requests;
using NETCore.Ldap.DER.Universals;
using System.Collections.Generic;
using System.Linq;

namespace NETCore.Ldap.DER.Applications.AuthChoices
{
    public class SASLAuthChoice : BaseAuthChoice
    {
        public SASLAuthChoice()
        {
            Tag = new DERTag
            {
                PcType = PcTypes.Primitive,
                TagClass = ClassTags.ContextSpecific,
                TagNumber = (int)BindRequestAuthenticationChoices.SASL
            };
        }

        public override BindRequestAuthenticationChoices Type => BindRequestAuthenticationChoices.SASL;

        public DEROctetString Mechanism { get; set; }
        public DEROctetString Credentials { get; set; }

        public override ICollection<byte> Serialize()
        {
            var content = new List<byte>();
            content.AddRange(Mechanism.Serialize());
            content.AddRange(Credentials.Serialize());
            Length = content.Count();
            var result = new List<byte>();
            result.AddRange(SerializeDerStructure());
            result.AddRange(content);
            return result;
        }

        public static SASLAuthChoice Extract(ICollection<byte> buffer)
        {
            var result = new SASLAuthChoice();
            result.ExtractTagAndLength(buffer);
            result.Mechanism = DEROctetString.Extract(buffer);
            result.Credentials = DEROctetString.Extract(buffer);
            return result;
        }
    }
}
