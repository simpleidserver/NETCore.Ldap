// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.Extensions;
using SimpleIdServer.Ldap.Core.Extensions;
using System.Collections.Generic;

namespace NETCore.Ldap.DER.Universals
{
    public class DERBoolean : DERUniversalType
    {
        public DERBoolean()
        {
            Tag = new DERTag
            {
                LdapCommand = null,
                TagClass = ClassTags.Universal,
                UniversalClassType = UniversalClassTypes.Boolean,
                TagNumber = (int)UniversalClassTypes.Boolean,
                PcType = PcTypes.Primitive
            };
        }

        public DERBoolean(bool value)
        {
            Value = value;
            Tag = new DERTag
            {
                LdapCommand = null,
                TagClass = ClassTags.Universal,
                UniversalClassType = UniversalClassTypes.Boolean,
                TagNumber = (int)UniversalClassTypes.Boolean,
                PcType = PcTypes.Primitive
            };
        }

        public bool Value { get; set; }

        public static DERBoolean Extract(ICollection<byte> buffer)
        {
            var derBoolean = new DERBoolean();
            derBoolean.ExtractTagAndLength(buffer);
            int valueBuffer = buffer.Dequeue();
            derBoolean.Value = valueBuffer.ToString() == "1";
            return derBoolean;
        }

        public override ICollection<byte> Serialize()
        {
            var result = new List<byte>();
            int i = Value ? 1 : 0;
            Length = 1;

            result.AddRange(SerializeDerStructure());
            result.Add((byte)i);

            return result;
        }
    }
}
