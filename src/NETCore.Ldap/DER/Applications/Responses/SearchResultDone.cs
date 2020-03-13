// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using System.Linq;

namespace NETCore.Ldap.DER.Applications.Responses
{
    /// <summary>
    /// [APPLICATION 5] LDAPResult
    /// </summary>
    public class SearchResultDone : BaseOperationDone
    {
        public SearchResultDone()
        {
            Tag = new DERTag
            {
                LdapCommand = LdapCommands.SearchResultDone,
                TagClass = ClassTags.Application,
                TagNumber = (int)LdapCommands.SearchResultDone,
                PcType = PcTypes.Constructed
            };
        }

        public static SearchResultDone Extract(ICollection<byte> buffer)
        {
            var result = new SearchResultDone();
            result.Result = LDAPResult.Extract(buffer);
            return result;
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
