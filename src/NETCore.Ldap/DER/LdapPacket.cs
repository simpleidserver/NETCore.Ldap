// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.DER.Universals;
using System.Collections.Generic;
using System.Linq;

namespace NETCore.Ldap.DER
{
    public class LdapPacket : DERStructure
    {
        public LdapPacket()
        {
            Tag = new DERTag
            {
                LdapCommand = null,
                TagClass = ClassTags.Universal,
                UniversalClassType = UniversalClassTypes.Sequence,
                TagNumber = (int)UniversalClassTypes.Sequence,
                PcType = PcTypes.Constructed
            };
        }

        public DERInteger MessageId { get; set; }
        public DERProtocolOperation ProtocolOperation { get; set; }
        public DERSequence<DERControl> Controls { get; set; }

        public static LdapPacket Extract(ICollection<byte> buffer)
        {
            var ldapPacket = new LdapPacket();
            ldapPacket.ExtractTagAndLength(buffer);
            ldapPacket.MessageId = DERInteger.Extract(buffer);
            ldapPacket.ProtocolOperation = DERProtocolOperation.Extract(buffer);
            ldapPacket.Controls = DERSequence<DERControl>.Extract(buffer);
            return ldapPacket;
        }

        public override ICollection<byte> Serialize()
        {
            var content = new List<byte>();
            content.AddRange(MessageId.Serialize());
            content.AddRange(ProtocolOperation.Serialize());
            if (Controls != null && Controls.Values.Any())
            {
                content.AddRange(Controls.Serialize(0xa0));
            }

            Length = content.Count();            

            var result = new List<byte>();
            result.AddRange(SerializeDerStructure(true));
            result.AddRange(content);
            return result;
        }
    }
}