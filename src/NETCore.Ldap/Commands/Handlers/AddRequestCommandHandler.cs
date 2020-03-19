// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Microsoft.Extensions.Options;
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
using NETCore.Ldap.SyntaxValidator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Ldap.Commands.Handlers
{
    public class AddRequestCommandHandler : IAddRequestCommandHandler
    {
        private readonly ILDAPEntryQueryStore _ldapQueryStore;
        private readonly ILDAPEntryCommandStore _ldapCommandStore;
        private readonly LdapServerOptions _options;
        private readonly IEnumerable<IAttributeSyntaxValidator> _attributeSyntaxLst;

        public AddRequestCommandHandler(ILDAPEntryQueryStore ldapQueryStore, ILDAPEntryCommandStore ldapCommandStore, IOptions<LdapServerOptions> options, IEnumerable<IAttributeSyntaxValidator> attributeSyntaxLst)
        {
            _ldapQueryStore = ldapQueryStore;
            _ldapCommandStore = ldapCommandStore;
            _options = options.Value;
            _attributeSyntaxLst = attributeSyntaxLst;
        }

        public async Task<ICollection<LdapPacket>> Execute(AddRequestCommand addRequestCommand)
        {
            var addRequest = addRequestCommand.ProtocolOperation.Operation as AddRequest;
            var dn = addRequest.Entry.Value;
            var objectClassAttributes = addRequest.Attributes.Values.Where(v => v.Type.Value == _options.ObjectClassAttributeName);
            if (!objectClassAttributes.Any())
            {
                throw new LdapException(string.Format(Global.AttributeIsMissing, _options.ObjectClassAttributeName), LDAPResultCodes.Other, dn);
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

            var existingObjectClasses = await _ldapQueryStore.GetByAttributes(objectClassAttributes.Select(attr => new KeyValuePair<string, string>(_options.NameAttributeName, attr.Vals.Values.First().Value)).ToList());
            var mustAttributeTypes = existingObjectClasses.SelectMany(obj => obj.Attributes.Where(attr => attr.Name == _options.MustAttributeName));
            var mayAttributeTypes = existingObjectClasses.SelectMany(obj => obj.Attributes.Where(attr => attr.Name == _options.MayAttributeName));
            var attributeTypes = new List<string>();
            attributeTypes.AddRange(mustAttributeTypes.SelectMany(m => m.Values));
            attributeTypes.AddRange(mayAttributeTypes.SelectMany(m => m.Values));
            var existingObjectClassNames = existingObjectClasses.Select(entry => entry.Attributes.First(attr => attr.Name == _options.NameAttributeName).Values.First());
            var unknownObjectClasses = objectClassAttributes.Where(obj => !existingObjectClassNames.Contains(obj.Vals.Values.First().Value)).Select(kvp => kvp.Vals.Values.First().Value);
            if (unknownObjectClasses.Any())
            {
                throw new LdapException(string.Format(Global.UnknownObjectClasses, string.Join(",", unknownObjectClasses)), LDAPResultCodes.Other, dn);
            }

            var missingRequiredAttributes = mustAttributeTypes.Where(attr => !addRequest.Attributes.Values.Any(a => attr.Values.Contains(a.Type.Value)));
            if (missingRequiredAttributes.Any())
            {
                throw new LdapException(string.Format(Global.RequiredAttributeMissing, string.Join(",", missingRequiredAttributes.SelectMany(m => m.Values))), LDAPResultCodes.Other, dn);
            }

            var undefinedAttributes = addRequest.Attributes.Values.Where(attr => !attributeTypes.Any(d => d.Equals(attr.Type.Value, StringComparison.InvariantCultureIgnoreCase)));
            if (undefinedAttributes.Any())
            {
                throw new LdapException(string.Format(Global.AttributesUndefined, string.Join(",", undefinedAttributes.Select(a => a.Type.Value))), LDAPResultCodes.Other, dn);
            }

            var record = new LDAPEntry
            {
                DistinguishedName = dn,
                Attributes = new List<LDAPEntryAttribute>()
            };
            var attributes = await _ldapQueryStore.GetByAttributes(attributeTypes.Select(attr => new KeyValuePair<string, string>(_options.NameAttributeName, attr)).ToList());
            foreach(var attr in addRequest.Attributes.Values)
            {
                var attribute = attributes.First(a => a.Attributes.Any(at => at.Name == _options.NameAttributeName && at.Values.Contains(attr.Type.Value)));
                CheckSyntax(attribute, attr, dn);
                var existingAttributes = addRequest.Attributes.Values.Where(a => a.Type.Value == attr.Type.Value);
                if (IsSingleValue(attribute) && existingAttributes.Count() > 1)
                {
                    throw new LdapException(string.Format(Global.SingleValue, attr.Type.Value), LDAPResultCodes.AttributeOrValueExists, dn);
                }

                record.Attributes.Add(new LDAPEntryAttribute
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = attr.Type.Value,
                    Values = attr.Vals.Values.Select(v => v.Value).ToList()
                });
            }

            _ldapCommandStore.Add(record);
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

        private bool IsSingleValue(LDAPEntry entry)
        {
            var singleValueAttribute = entry.Attributes.FirstOrDefault(a => a.Name == _options.SingleValueAttributeName);
            bool isSingleValue;
            if (singleValueAttribute != null && bool.TryParse(singleValueAttribute.Values.First(), out isSingleValue))
            {
                return isSingleValue;
            }

            return false;
        }

        private void CheckSyntax(LDAPEntry entry, PartialAttribute attribute, string dn)
        {
            var syntax = entry.Attributes.FirstOrDefault(a => a.Name == _options.SyntaxAttributeName);
            if (syntax != null)
            {
                var attributeSyntax = _attributeSyntaxLst.FirstOrDefault(a => a.OID == syntax.Values.First());
                if (attributeSyntax != null && !attributeSyntax.Check(attribute.Vals.Values.Select(v => v.Value).ToList()))
                {
                    throw new LdapException(string.Format(Global.AttributeSyntaxNotValid, attribute.Type.Value, attributeSyntax.OID), LDAPResultCodes.InvalidAttributeSyntax, dn);
                }
            }
        }
    }
}
