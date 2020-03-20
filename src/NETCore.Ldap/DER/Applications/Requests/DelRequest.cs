// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.Extensions;
using SimpleIdServer.Ldap.Core.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NETCore.Ldap.DER.Applications.Requests
{
    /// <summary>
    /// [APPLICATION 10] LDAPDN
    /// </summary>
    public class DelRequest : DERApplicationType
    {
        public DelRequest()
        {
            Tag = new DERTag
            {
                LdapCommand = LdapCommands.DelRequest,
                TagClass = ClassTags.Application,
                TagNumber = (int)LdapCommands.DelRequest,
                PcType = PcTypes.Constructed
            };
        }

        public string Entry { get; set; }

        public override ICollection<byte> Serialize()
        {
            throw new System.NotImplementedException();
        }

        public static DelRequest Extract(ICollection<byte> buffer, int length)
        {
            var result = new DelRequest();
            var valueBuffer = buffer.Dequeue(length);
            result.Entry = Encoding.ASCII.GetString(valueBuffer.ToArray());
            return result;
        }
    }
}
