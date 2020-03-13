// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.DER.Applications.Filters;
using NETCore.Ldap.DER.Universals;
using System.Collections.Generic;

namespace NETCore.Ldap.DER.Applications.Requests
{
    public enum SearchRequestScopes
    {
        BaseObject = 0,
        SingleLevel = 1,
        WholeSubtree = 2
    }

    public enum SearchRequestDeferAliases
    {
        NeverDerefAliases = 0,
        DerefInSearching = 1,
        DerefFindingBaseObj = 2,
        DerefAlways = 3
    }

    /// <summary>
    ///  [APPLICATION 3] SEQUENCE {
    ///  baseObject      LDAPDN,
    ///  scope           ENUMERATED {
    ///  }
    ///   derefAliases    ENUMERATED {
    ///  }
    ///  sizeLimit       INTEGER (0 ..  maxInt),
    ///  timeLimit       INTEGER (0 ..  maxInt),
    ///  typesOnly       BOOLEAN,
    ///  filter          Filter,
    ///  attributes      AttributeSelection
    /// </summary>
    public class SearchRequest : DERApplicationType
    {
        public SearchRequest()
        {
            Tag = new DERTag
            {
                LdapCommand = LdapCommands.SearchRequest,
                TagClass = ClassTags.Application,
                TagNumber = (int)LdapCommands.SearchRequest,
                PcType = PcTypes.Constructed
            };
        }

        public DEROctetString BaseObject { get; set; }
        public DEREnumerated<SearchRequestScopes> Scope { get; set; }
        public DEREnumerated<SearchRequestDeferAliases> DeferAlias { get; set; }
        public DERInteger SizeLimit { get; set; }
        public DERInteger TimeLimit { get; set; }
        public DERBoolean TypesOnly { get; set; }
        /// <summary>
        /// A filter that defines the conditions that must be fulfilled in order for the search to match a given entry.
        /// </summary>
        public SearchRequestFilter Filter { get; set; }
        /// <summary>
        /// A selection list of the attributes to be returned from each entry that match the search filter.
        /// </summary>
        public DERSequence<DEROctetString> Attributes { get; set; }

        public static SearchRequest Extract(ICollection<byte> buffer)
        {
            var searchRequest = new SearchRequest();
            searchRequest.BaseObject = DEROctetString.Extract(buffer);
            searchRequest.Scope = DEREnumerated<SearchRequestScopes>.Extract(buffer);
            searchRequest.DeferAlias = DEREnumerated<SearchRequestDeferAliases>.Extract(buffer);
            searchRequest.SizeLimit = DERInteger.Extract(buffer);
            searchRequest.TimeLimit = DERInteger.Extract(buffer);
            searchRequest.TypesOnly = DERBoolean.Extract(buffer);
            searchRequest.Filter = SearchRequestFilter.Extract(buffer);
            searchRequest.Attributes = DERSequence<DEROctetString>.Extract(buffer);
            return searchRequest;
        }

        public override ICollection<byte> Serialize()
        {
            throw new System.NotImplementedException();
        }
    }
}