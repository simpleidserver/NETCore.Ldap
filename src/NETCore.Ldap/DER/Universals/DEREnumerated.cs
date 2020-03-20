// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.Extensions;
using System;
using System.Collections.Generic;

namespace NETCore.Ldap.DER.Universals
{
    public class DEREnumerated<T> : DERUniversalType where T : struct, IConvertible
    {
        public DEREnumerated()
        {
            Tag = new DERTag
            {
                LdapCommand = null,
                TagClass = ClassTags.Universal,
                UniversalClassType = UniversalClassTypes.Enumerated,
                TagNumber = (int)UniversalClassTypes.Enumerated,
                PcType = PcTypes.Primitive
            };
        }

        public DEREnumerated(T value) : this()
        {
            Value = value;
        }

        public T Value { get; set; }

        public static DEREnumerated<T> Extract(ICollection<byte> buffer)
        {
            var derEnum = new DEREnumerated<T>();
            derEnum.ExtractTagAndLength(buffer);
            var valueBuffer = (int)buffer.Dequeue();
            derEnum.Value = (T)(object)valueBuffer;
            return derEnum;
        }

        public override ICollection<byte> Serialize()
        {
            var result = new List<byte>();
            int i = (int)Enum.Parse(typeof(T), Value.ToString());
            Length = 1;

            result.AddRange(SerializeDerStructure());
            result.Add((byte)i);

            return result;
        }
    }
}