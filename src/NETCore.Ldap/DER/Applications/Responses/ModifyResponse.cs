// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using System.Linq;

namespace NETCore.Ldap.DER.Applications.Responses
{
    /// <summary>
    /// AddResponse ::= [APPLICATION 7] LDAPResult
    /// </summary>
    public class ModifyResponse : BaseOperationDone
    {
        public ModifyResponse()
        {
            Tag = new DERTag
            {
                LdapCommand = LdapCommands.ModifyResponse,
                TagClass = ClassTags.Application,
                TagNumber = (int)LdapCommands.ModifyResponse,
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
