// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using System.Linq;

namespace NETCore.Ldap.DER.Applications.Responses
{
    /// <summary>
    /// AddResponse ::= [APPLICATION 11] LDAPResult
    /// </summary>
    public class DelResponse : BaseOperationDone
    {
        public DelResponse()
        {
            Tag = new DERTag
            {
                LdapCommand = LdapCommands.DelResponse,
                TagClass = ClassTags.Application,
                TagNumber = (int)LdapCommands.DelResponse,
                PcType = PcTypes.Constructed
            };
        }

        public override ICollection<byte> Serialize()
        {
            var content = new List<byte>();
            content.AddRange(Result.Serialize());
            Length = content.Count();

            var result = new List<byte>();
            result.AddRange(content);
            return result;
        }
    }
}
