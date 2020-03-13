// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.Extensions;
using SimpleIdServer.Ldap.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NETCore.Ldap.DER.Universals
{
    public class DERInteger : DERUniversalType
    {
        public DERInteger()
        {
            Tag = new DERTag
            {
                LdapCommand = null,
                TagClass = ClassTags.Universal,
                UniversalClassType = UniversalClassTypes.Integer,
                TagNumber = (int)UniversalClassTypes.Integer,
                PcType = PcTypes.Primitive
            };
        }

        public DERInteger(int value)
        {
            Value = value;
            Tag = new DERTag
            {
                LdapCommand = null,
                TagClass = ClassTags.Universal,
                UniversalClassType = UniversalClassTypes.Integer,
                TagNumber = (int)UniversalClassTypes.Integer,
                PcType = PcTypes.Primitive
            };
        }

        public int Value { get; set; }

        public static DERInteger Extract(ICollection<byte> buffer)
        {
            var derInteger = new DERInteger();
            derInteger.ExtractTagAndLength(buffer);
            var valueBuffer = buffer.Dequeue(derInteger.Length).Reverse().ToList();
            for (var i = valueBuffer.Count(); i <= 4; i++)
            {
                valueBuffer.Add(0x00);
            }

            derInteger.Value = BitConverter.ToInt32(valueBuffer.ToArray(), 0);
            return derInteger;
        }

        public override ICollection<byte> Serialize()
        {
            var result = new List<byte>();
            var values = BitConverter.GetBytes(Value).Reverse().SkipWhile(v => v == 0x00);
            Length = values.Count();
            result.AddRange(SerializeDerStructure());
            result.AddRange(values);
            return result;
        }
    }
}
