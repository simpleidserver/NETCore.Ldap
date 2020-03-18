// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.DER.Universals;
using System.Collections.Generic;
using System.Linq;

namespace NETCore.Ldap.DER.Applications.Responses
{
    public class BindResponse : BaseOperationDone
    {
        public BindResponse() : base()
        {
            Tag = new DERTag
            {
                LdapCommand = LdapCommands.BindResponse,
                TagClass = ClassTags.Application,
                TagNumber = (int)LdapCommands.BindResponse,
                PcType = PcTypes.Constructed
            };
        }

        public DEROctetString ServerSaslCreds { get; set; }
        public List<byte> Buffer { get; set; }

        public static BindResponse Extract(ICollection<byte> buffer)
        {
            var result = new BindResponse();
            result.Result.ResultCode = DEREnumerated<LDAPResultCodes>.Extract(buffer);
            result.Result.MatchedDN = DEROctetString.Extract(buffer);
            result.Result.DiagnosticMessage = DEROctetString.Extract(buffer);
            return result;
        }

        public override ICollection<byte> Serialize()
        {
            var content = new List<byte>();
            content.AddRange(Result.ResultCode.Serialize());
            content.AddRange(Result.MatchedDN.Serialize());
            content.AddRange(Result.DiagnosticMessage.Serialize());
            if (Buffer != null)
            {
                content.AddRange(Buffer);
            }

            Length = content.Count();
            var result = new List<byte>();
            result.AddRange(content);
            return result;
        }
    }
}
