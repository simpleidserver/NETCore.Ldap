// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;

namespace NETCore.Ldap.DER.Applications.Requests
{
    /// <summary>
    /// The function of the Unbind operation is to terminate an LDAP session.
    /// [APPLICATION 2] NULL
    /// </summary>
    public class UnbindRequest : DERApplicationType
    {
        public UnbindRequest()
        {
            Tag = new DERTag
            {
                LdapCommand = LdapCommands.UnbindRequest,
                TagClass = ClassTags.Application,
                TagNumber = (int)LdapCommands.UnbindRequest,
                PcType = PcTypes.Constructed
            };
        }

        public override ICollection<byte> Serialize()
        {
            var result = new List<byte>();
            return result;
        }

        public static UnbindRequest Extract(ICollection<byte> buffer)
        {
            var unbindRequest = new UnbindRequest();
            return unbindRequest;
        }
    }
}