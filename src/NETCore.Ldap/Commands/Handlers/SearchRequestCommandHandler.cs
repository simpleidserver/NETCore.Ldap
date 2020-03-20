// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Microsoft.Extensions.Options;
using NETCore.Ldap.Authentication;
using NETCore.Ldap.DER;
using NETCore.Ldap.DER.Applications;
using NETCore.Ldap.DER.Applications.Filters;
using NETCore.Ldap.DER.Applications.Requests;
using NETCore.Ldap.DER.Applications.Responses;
using NETCore.Ldap.DER.Universals;
using NETCore.Ldap.Exceptions;
using NETCore.Ldap.Persistence;
using NETCore.Ldap.Persistence.Parameters;
using NETCore.Ldap.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Ldap.Commands.Handlers
{
    public class SearchRequestCommandHandler : ISearchRequestCommandHandler
    {
        private readonly LdapServerOptions _options;
        private readonly ILDAPEntryQueryStore _ldapEntryQueryStore;
        private readonly IEnumerable<IAuthenticationHandler> _authenticationHandlers;

        public SearchRequestCommandHandler(IOptions<LdapServerOptions> options, ILDAPEntryQueryStore ldapEntryQueryStore, IEnumerable<IAuthenticationHandler> authenticationHandlers)
        {
            _options = options.Value;
            _ldapEntryQueryStore = ldapEntryQueryStore;
            _authenticationHandlers = authenticationHandlers;
        }

        public async Task<ICollection<LdapPacket>> Execute(SearchRequestCommand searchRequestCommand)
        {
            var searchRequest = searchRequestCommand.ProtocolOperation.Operation as SearchRequest;
            var dn = searchRequest.BaseObject.Value;
            var result = new List<LdapPacket>();
            if (string.IsNullOrWhiteSpace(dn))
            {
                result.Add(GetRootDSE(searchRequestCommand));
            }
            else
            {
                // Ajouter des tests pour la recherche.
                // Ajouter des tests pour les contrôles.
                // Externaliser namingContexts
                // Externaliser subSchemaEntry.
                var record = await _ldapEntryQueryStore.Get(searchRequest.BaseObject.Value);
                if (record == null)
                {
                    throw new LdapException(string.Format(Global.EntryDoesntExist, dn), LDAPResultCodes.NoSuchObject, searchRequest.BaseObject.Value);
                }

                var ldapEntries = await _ldapEntryQueryStore.Search(Build(searchRequest));
                foreach(var ldapEntry in ldapEntries)
                {
                    var searchResultEntry = new SearchResultEntry
                    {
                        ObjectName = new DEROctetString(ldapEntry.DistinguishedName)
                    };
                    foreach(var attr in ldapEntry.Attributes)
                    {
                        if (!searchRequest.Attributes.Values.Any() || searchRequest.Attributes.Values.Any(v => v.Value == "*" || v.Value == attr.Name))
                        {
                            var partialAttribute = new PartialAttribute
                            {
                                Type = new DEROctetString(attr.Name),
                                Vals = new DERSet<DEROctetString>
                                {
                                    Values = attr.Values.Select(s => new DEROctetString(s)).ToList()
                                }
                            };
                            searchResultEntry.PartialAttributes.Values.Add(partialAttribute);
                        }
                    }

                    result.Add(new LdapPacket
                    {
                        MessageId = searchRequestCommand.MessageId,
                        ProtocolOperation = new DERProtocolOperation
                        {
                            Operation = searchResultEntry
                        }
                    });
                }
            }

            result.Add(new LdapPacket
            {
                MessageId = searchRequestCommand.MessageId,
                ProtocolOperation = new DERProtocolOperation
                {
                    Operation = new SearchResultDone
                    {
                        Result = new LDAPResult
                        {
                            MatchedDN = searchRequest.BaseObject,
                            ResultCode = new DEREnumerated<LDAPResultCodes>
                            {
                                Value = LDAPResultCodes.Success
                            },
                            DiagnosticMessage = new DEROctetString("")
                        }
                    }
                }
            });
            return result;
        }

        private LdapPacket GetRootDSE(SearchRequestCommand searchRequestCommand)
        {
            var searchRequest = searchRequestCommand.ProtocolOperation.Operation as SearchRequest;
            var resultEntry = new SearchResultEntry
            {
                ObjectName = new DEROctetString(string.Empty)
            };
            resultEntry.PartialAttributes.Values.Add(new PartialAttribute
            {
                Type = new DEROctetString(LdapConstants.StandardAttributeTypeNames.VendorName),
                Vals = new DERSet<DEROctetString>
                {
                    Values = new List<DEROctetString> { new DEROctetString(_options.VendorName) }
                }
            });
            resultEntry.PartialAttributes.Values.Add(new PartialAttribute
            {
                Type = new DEROctetString(LdapConstants.StandardAttributeTypeNames.VendorVersion),
                Vals = new DERSet<DEROctetString>
                {
                    Values = new List<DEROctetString> { new DEROctetString(_options.VendorVersion) }
                }
            });
            resultEntry.PartialAttributes.Values.Add(new PartialAttribute
            {
                Type = new DEROctetString(LdapConstants.StandardAttributeTypeNames.NamingContexts),
                Vals = new DERSet<DEROctetString>
                {
                    Values = _options.NamingContexts.Select(n => new DEROctetString(n)).ToList()// new List<DEROctetString> { new DEROctetString(namingContext) }
                }
            });
            resultEntry.PartialAttributes.Values.Add(new PartialAttribute
            {
                Type = new DEROctetString(LdapConstants.StandardAttributeTypeNames.SupportedLDAPVersion),
                Vals = new DERSet<DEROctetString>
                {
                    Values = new List<DEROctetString> { new DEROctetString(_options.SupportedLDAPVersion.ToString()) }
                }
            });
            foreach (var authenticationHandler in _authenticationHandlers)
            {
                resultEntry.PartialAttributes.Values.Add(new PartialAttribute
                {
                    Type = new DEROctetString(LdapConstants.StandardAttributeTypeNames.SupportedSASLMechanisms),
                    Vals = new DERSet<DEROctetString>
                    {
                        Values = new List<DEROctetString> { new DEROctetString(Enum.GetName(typeof(BindRequestAuthenticationChoices), authenticationHandler.AuthChoice)) }
                    }
                });
            }
            resultEntry.PartialAttributes.Values.Add(new PartialAttribute
            {
                Type = new DEROctetString(LdapConstants.StandardAttributeTypeNames.SubschemaSubentry),
                Vals = new DERSet<DEROctetString>
                {
                    Values = new List<DEROctetString> { new DEROctetString(_options.SubSchemaSubEntry) }
                }
            });
            return new LdapPacket
            {
                MessageId = searchRequestCommand.MessageId,
                ProtocolOperation = new DERProtocolOperation
                {
                    Operation = resultEntry
                }
            };
        }

        private static SearchLDAPEntriesParameter Build(SearchRequest searchRequest)
        {
            var result = new SearchLDAPEntriesParameter
            {
                SizeLimit = searchRequest.SizeLimit.Value,
                BaseDistinguishedName = searchRequest.BaseObject.Value,
                Scope = (SearchScopeTypes)searchRequest.Scope.Value,
                Filter = Build(searchRequest.Filter)
            };
            return result;
        }

        private static LDAPAttributeFilter Build(SearchRequestFilter filter)
        {
            var result = new LDAPAttributeFilter
            {
                Type = (SearchFilterTypes)filter.Type
            };
            switch (filter.Type)
            {
                case SearchRequestFilterTypes.Present:
                    result.AttributeName = filter.Value;
                    break;
                case SearchRequestFilterTypes.EqualityMatch:
                case SearchRequestFilterTypes.GreaterOrEqual:
                case SearchRequestFilterTypes.LessOrEqual:
                case SearchRequestFilterTypes.ApproxMatch:
                    result.AttributeName = filter.Attribute.AttributeDescription.Value;
                    result.AttributeValue = filter.Attribute.AssertionValue.Value;
                    break;
                case SearchRequestFilterTypes.Or:
                case SearchRequestFilterTypes.And:
                    foreach (var f in filter.Filters)
                    {
                        var child = Build(f);
                        result.Filters.Add(child);
                    }

                    break;
            }
            return result;
        }
    }
}