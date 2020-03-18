// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.Extensions;
using SimpleIdServer.Ldap.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NETCore.Ldap.DER
{
    public enum ClassTags
    {
        /// <summary>
        /// The type is native to ASN.1
        /// </summary>
        Universal = 0,
        /// <summary>
        /// The type is only valid for one specific application
        /// </summary>
        Application = 1,
        /// <summary>
        /// Meaning of this type depends on the context (such as within a sequence, set or choice)
        /// </summary>
        ContextSpecific = 2,
        /// <summary>
        /// Defined in private specifications
        /// </summary>
        Private = 3
    }

    public enum PcTypes
    {
        /// <summary>
        /// The contents octets directly encode the element value.
        /// </summary>
        Primitive = 0,
        /// <summary>
        /// The contents octets contain 0, 1, or more element encodings.
        /// </summary>
        Constructed = 1 
    }

    public enum LdapCommands
    {
        BindRequest = 0,
        BindResponse = 1,
        UnbindRequest = 2,
        SearchRequest = 3,
        SearchResultEntry = 4,
        SearchResultDone = 5,
        ModifyRequest = 6,
        ModifyResponse = 7,
        AddRequest = 8,
        AddResponse = 9,
        DelRequest = 10,
        DelResponse = 11,
        ModifyDNRequest = 12,
        ModifyDNResponse = 13,
        CompareRequest = 14,
        AbandonRequest = 16
    }

    public enum UniversalClassTypes
    {
        EndOfContent = 0,
        Boolean = 1,
        Integer = 2,
        BigString = 3,
        OctetString = 4,
        Null = 5,
        ObjectIdentifier = 6,
        ObjectDescriptor = 7,
        External = 8,
        Real = 9,
        Enumerated = 10,
        EmbeddedPdv = 11,
        UTF8String = 12,
        RelativeOID = 13,
        Sequence = 16,
        Set = 17
    }

    public class DERTag
    {
        public ClassTags TagClass { get; set; }
        public PcTypes PcType { get; set; }
        public LdapCommands? LdapCommand { get; set; }
        public UniversalClassTypes? UniversalClassType { get; set; }
        public int TagNumber { get; set; }

        public static DERTag Extract(ICollection<byte> buffer)
        {
            var bits = buffer.Dequeue().ConcatBits();
            var classBits = Convert.ToByte(bits.Dequeue(2), 2);
            var pcType = (PcTypes)Convert.ToInt32(bits.Dequeue());
            var tagNumberBits = bits.Dequeue(5);
            var tagNumber = Convert.ToInt32(tagNumberBits, 2);
            var tagClass = (ClassTags)classBits;
            LdapCommands? ldapCommand = null;
            UniversalClassTypes? universalClassType = null;
            if (tagClass == ClassTags.Application && Enum.GetValues(typeof(LdapCommands)).Cast<int>().Contains(tagNumber))
            {
                ldapCommand = (LdapCommands)tagNumber;
            }

            if (tagClass == ClassTags.Universal && Enum.GetValues(typeof(UniversalClassTypes)).Cast<int>().Contains(tagNumber))
            {
                universalClassType = (UniversalClassTypes)tagNumber;
            }

            return new DERTag
            {
                TagClass = tagClass,
                PcType = pcType,
                LdapCommand = ldapCommand,
                UniversalClassType = universalClassType,
                TagNumber = tagNumber
            };
        }

        public byte Serialize()
        {
            var cl = Convert.ToString((byte)TagClass, 2).PadLeft(2, '0');
            var pc = Convert.ToString((byte)PcType);
            string tabNumber = string.Empty;
            if (TagClass == ClassTags.Application)
            {
                tabNumber = Convert.ToString((byte)LdapCommand.Value, 2).PadLeft(5, '0');
            }

            if (TagClass == ClassTags.Universal)
            {
                tabNumber = Convert.ToString((byte)UniversalClassType.Value, 2).PadLeft(5, '0');
            }

            if (TagClass == ClassTags.ContextSpecific)
            {
                tabNumber = Convert.ToString((byte)TagNumber).PadLeft(5, '0');
            }

            var bits = $"{cl}{pc}{tabNumber}";
            return Convert.ToByte(bits, 2);
        }
    }
}
