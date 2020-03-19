// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.DER.Universals;
using System.Collections.Generic;
using System.Linq;

namespace NETCore.Ldap.DER.Applications.Requests
{
    /// <summary>
    /// The Add operation allows a client to request the addition of an entry into the Directory.
    /// [APPLICATION 8] SEQUENCE {
    ///      entry LDAPDN,
    ///      attributes      AttributeList
    /// }
    /// </summary>
    public class AddRequest : DERApplicationType
    {
        public AddRequest()
        {
            Tag = new DERTag
            {
                LdapCommand = LdapCommands.AddRequest,
                TagClass = ClassTags.Application,
                TagNumber = (int)LdapCommands.AddRequest,
                PcType = PcTypes.Constructed
            };
        }

        public DEROctetString Entry { get; set; }
        public DERSequence<PartialAttribute> Attributes { get; set; }

        public override ICollection<byte> Serialize()
        {
            var result = new List<byte>();
            result.AddRange(Entry.Serialize());
            result.AddRange(Attributes.Serialize());
            return result;
        }

        public static AddRequest Extract(ICollection<byte> buffer)
        {
            var addRequest = new AddRequest();
            addRequest.Entry = DEROctetString.Extract(buffer);
            addRequest.Attributes = DERSequence<PartialAttribute>.Extract(buffer);
            return addRequest;
        }
    }
}
