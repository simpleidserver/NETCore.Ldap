// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NETCore.Ldap.DER.Universals
{
    public class DEROctetString : DERUniversalType
    {
        public DEROctetString()
        {
            Tag = new DERTag
            {
                LdapCommand = null,
                TagClass = ClassTags.Universal,
                UniversalClassType = UniversalClassTypes.OctetString,
                TagNumber = (int)UniversalClassTypes.OctetString,
                PcType = PcTypes.Primitive
            };
        }

        public DEROctetString(string value)
        {
            Value = value;
            Payload = Encoding.ASCII.GetBytes(value).ToList();
            Tag = new DERTag
            {
                LdapCommand = null,
                TagClass = ClassTags.Universal,
                UniversalClassType = UniversalClassTypes.OctetString,
                TagNumber = (int)UniversalClassTypes.OctetString,
                PcType = PcTypes.Primitive
            };
        }

        public string Value { get; set; }
        public ICollection<byte> Payload { get; set; }

        public static DEROctetString Extract(ICollection<byte> buffer)
        {
            var result = new DEROctetString();
            result.ExtractTagAndLength(buffer);
            var valueBuffer = buffer.Dequeue(result.Length);
            result.Payload = valueBuffer.ToList();
            result.Value = Encoding.ASCII.GetString(valueBuffer.ToArray());
            return result;
        }

        public override ICollection<byte> Serialize()
        {
            var result = new List<byte>();
            Length = Payload.Count();
            result.AddRange(SerializeDerStructure());
            result.AddRange(Payload);
            var rr = result.ConvertToString();
            return result;
        }
    }
}
