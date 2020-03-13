// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.DER;
using NETCore.Ldap.DER.Applications;
using NETCore.Ldap.DER.Applications.Requests;
using NETCore.Ldap.DER.Applications.Responses;
using NETCore.Ldap.DER.Universals;
using NETCore.Ldap.Domain;
using NETCore.Ldap.Exceptions;
using NETCore.Ldap.Extensions;
using NETCore.Ldap.Persistence;
using NETCore.Ldap.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Ldap.Commands.Handlers
{
    public class AddRequestCommandHandler : IAddRequestCommandHandler
    {
        private readonly ILDAPSchemaQueryStore _ldapQueryStore;
        private readonly ILDAPEntryCommandStore _ldapCommandStore;
        private readonly ILDAPSchemaQueryStore _ldapSchemaQueryStore;

        public AddRequestCommandHandler(ILDAPSchemaQueryStore ldapQueryStore, ILDAPEntryCommandStore ldapCommandStore, ILDAPSchemaQueryStore ldapSchemaQueryStore)
        {
            _ldapQueryStore = ldapQueryStore;
            _ldapCommandStore = ldapCommandStore;
            _ldapSchemaQueryStore = ldapSchemaQueryStore;
        }

        public async Task<ICollection<LdapPacket>> Execute(AddRequestCommand addRequestCommand)
        {
            var addRequest = addRequestCommand.ProtocolOperation.Operation as AddRequest;
            var objectClassAttribute = addRequest.Attributes.Values.FirstOrDefault(v => v.Type.Value == LdapConstants.StandardAttributeNames.ObjectClass);
            var dn = addRequest.Entry.Value;
            if (objectClassAttribute == null)
            {
                throw new LdapException(string.Format(Global.AttributeIsMissing, LdapConstants.StandardAttributeNames.ObjectClass), LDAPResultCodes.Other, dn);
            }

            var rootDN = dn.ExtractRootDN();
            var schema = await _ldapSchemaQueryStore.Get(rootDN);
            if (schema == null)
            {
                throw new LdapException(string.Format(Global.SchemaDoesntExist, rootDN), LDAPResultCodes.NoSuchObject, rootDN);
            }

            var existingRepresentation = await _ldapQueryStore.Get(dn);
            if (existingRepresentation != null)
            {
                throw new LdapException(string.Format(Global.EntryAlreadyExists, dn), LDAPResultCodes.EntryAlreadyExists, dn);
            }

            var parentDN = dn.ExtractParentDN();
            var parentRepresentation = await _ldapQueryStore.Get(parentDN);
            if (parentRepresentation == null)
            {
                throw new LdapException(string.Format(Global.ParentDoesntExist, parentDN), LDAPResultCodes.NoSuchObject, parentDN);
            }

            var attributes = new List<LDAPEntryAttribute>();
            foreach(var attr in addRequest.Attributes.Values)
            {
                foreach(var val in attr.Vals.Values)
                {
                    // TODO : Vérifier le schéma.
                    attributes.Add(new LDAPEntryAttribute
                    {
                        Value = val.Value,
                        Name = attr.Type.Value
                    });
                }
            }

            var newRepresentation = new LDAPEntry
            {
                Attributes = attributes,
                DistinguishedName = dn
            };
            _ldapCommandStore.Add(newRepresentation);
            await _ldapCommandStore.SaveChanges();
            var donePacket = new LdapPacket
            {
                MessageId = addRequestCommand.MessageId,
                ProtocolOperation = new DERProtocolOperation
                {
                    Operation = new AddResponse
                    {
                        Result = new LDAPResult
                        {
                            MatchedDN = addRequest.Entry,
                            ResultCode = new DEREnumerated<LDAPResultCodes>
                            {
                                Value = LDAPResultCodes.Success
                            },
                            DiagnosticMessage = new DEROctetString(string.Empty)
                        }
                    }
                }
            };

            return new List<LdapPacket>
            {
                donePacket
            };
        }
    }
}
