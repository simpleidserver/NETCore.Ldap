// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.DER.Universals;
using System.Collections.Generic;

namespace NETCore.Ldap.DER.Applications.Requests
{
    /// <summary>
    /// The function of the Abandon operation is to allow a client to request that the server abandon an uncompleted operation
    /// [APPLICATION 16] MessageID
    /// </summary>
    public class AbandonRequest : DERApplicationType
    {
        public AbandonRequest()
        {
            Tag = new DERTag
            {
                LdapCommand = LdapCommands.UnbindRequest,
                TagClass = ClassTags.Application,
                TagNumber = (int)LdapCommands.UnbindRequest,
                PcType = PcTypes.Constructed
            };
        }

        /// <summary>
        /// The MessageID is that of an operation that was requested earlier at this LDAP message layer.
        /// </summary>
        public DEROctetString MessageId { get; set; }

        public static AbandonRequest Extract(ICollection<byte> buffer)
        {
            var abandonRequest = new AbandonRequest();
            abandonRequest.MessageId = DEROctetString.Extract(buffer);
            return abandonRequest;
        }

        public override ICollection<byte> Serialize()
        {
            var result = new List<byte>();
            return result;
        }
    }
}
