// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.DER.Universals;
using System.Collections.Generic;
using System.Linq;

namespace NETCore.Ldap.DER.Applications
{
    public class PartialAttribute : DERStructure
    {
        public PartialAttribute()
        {
            Vals = new DERSet<DEROctetString>();
            Tag = new DERTag
            {
                LdapCommand = null,
                TagClass = ClassTags.Universal,
                UniversalClassType = UniversalClassTypes.Sequence,
                TagNumber = (int)UniversalClassTypes.Sequence,
                PcType = PcTypes.Constructed
            };
        }

        public DEROctetString Type { get; set; }
        public DERSet<DEROctetString> Vals { get; set; }

        public override ICollection<byte> Serialize()
        {
            var content = new List<byte>();
            content.AddRange(Type.Serialize());
            content.AddRange(Vals.Serialize());
            Length = content.Count();

            var result = new List<byte>();
            result.AddRange(SerializeDerStructure(true));
            result.AddRange(content);

            return result;;
        }

        public static PartialAttribute Extract(ICollection<byte> buffer)
        {
            var result = new PartialAttribute();
            result.ExtractTagAndLength(buffer);
            result.Type = DEROctetString.Extract(buffer);
            result.Vals = DERSet<DEROctetString>.Extract(buffer);
            return result;
        }
    }
}
