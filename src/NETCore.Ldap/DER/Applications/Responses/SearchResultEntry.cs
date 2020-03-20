// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.DER.Universals;
using System.Collections.Generic;
using System.Linq;

namespace NETCore.Ldap.DER.Applications.Responses
{
    /// <summary>
    /// SearchResultEntry ::= [APPLICATION 4] SEQUENCE {
    /// objectName      LDAPDN,
    /// attributes      PartialAttributeList }
    /// </summary>
    public class SearchResultEntry : DERApplicationType
    {
        public SearchResultEntry()
        {
            PartialAttributes = new DERSequence<PartialAttribute>();
            Tag = new DERTag
            {
                LdapCommand = LdapCommands.SearchResultEntry,
                TagClass = ClassTags.Application,
                TagNumber = (int)LdapCommands.SearchResultEntry,
                PcType = PcTypes.Constructed
            };
        }
        
        public DEROctetString ObjectName { get; set; }
        public DERSequence<PartialAttribute> PartialAttributes { get; set; }

        public override ICollection<byte> Serialize()
        {
            var content = new List<byte>();
            content.AddRange(ObjectName.Serialize());
            content.AddRange(PartialAttributes.Serialize());
            Length = content.Count();
            var result = new List<byte>();
            result.AddRange(content);
            return result;
        }

        public static SearchResultEntry Extract(ICollection<byte> payload)
        {
            var result = new SearchResultEntry();
            result.ObjectName = DEROctetString.Extract(payload);
            result.PartialAttributes = DERSequence<PartialAttribute>.Extract(payload);
            return result;
        }
    }
}