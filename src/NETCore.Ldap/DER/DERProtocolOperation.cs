// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.DER.Applications;
using NETCore.Ldap.DER.Applications.Requests;
using NETCore.Ldap.DER.Applications.Responses;
using System.Collections.Generic;
using System.Linq;

namespace NETCore.Ldap.DER
{
    public class DERProtocolOperation : DERStructure
    {
        public DERProtocolOperation()
        {
        }

        public DERProtocolOperation(DERApplicationType operation)
        {
            Operation = operation;
        }

        public DERApplicationType Operation { get; set; }

        public static DERProtocolOperation Extract(ICollection<byte> buffer)
        {
            var result = new DERProtocolOperation();
            result.ExtractTagAndLength(buffer);
            switch(result.Tag.LdapCommand)
            {
                case LdapCommands.SearchRequest:
                    result.Operation = SearchRequest.Extract(buffer);
                    break;
                case LdapCommands.BindRequest:
                    result.Operation = BindRequest.Extract(buffer);
                    break;
                case LdapCommands.UnbindRequest:
                    result.Operation = UnbindRequest.Extract(buffer);
                    break;
                case LdapCommands.AddRequest:
                    result.Operation = AddRequest.Extract(buffer);
                    break;
                case LdapCommands.DelRequest:
                    result.Operation = DelRequest.Extract(buffer, result.Length);
                    break;
                case LdapCommands.ModifyDNRequest:
                    result.Operation = ModifyDNRequest.Extract(buffer);
                    break;
                case LdapCommands.CompareRequest:
                    result.Operation = CompareRequest.Extract(buffer);
                    break;
                case LdapCommands.AbandonRequest:
                    result.Operation = AbandonRequest.Extract(buffer);
                    break;
                case LdapCommands.ModifyRequest:
                    result.Operation = ModifyRequest.Extract(buffer);
                    break;
                case LdapCommands.SearchResultDone:
                    result.Operation = SearchResultDone.Extract(buffer);
                    break;
            }

            return result;
        }

        public override ICollection<byte> Serialize()
        {
            var content = Operation.Serialize();
            Tag = Operation.Tag;
            Length = content.Count();

            var result = new List<byte>();
            result.AddRange(SerializeDerStructure(true));
            result.AddRange(content);
            return result;
        }
    }
}