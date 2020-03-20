// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.DER.Applications.Filters;
using NETCore.Ldap.DER.Universals;
using System.Collections.Generic;

namespace NETCore.Ldap.DER.Applications.Requests
{
    public enum SearchRequestScopes
    {
        /// <summary>
        /// The scope is constrained to the entry named by baseObject.
        /// </summary>
        BaseObject = 0,
        /// <summary>
        /// The scope is constrained to the immediate subordinates of the entry named by baseObject.
        /// </summary>
        SingleLevel = 1,
        /// <summary>
        /// The scope is constrained to the entry named by baseObject and to all its subordinates.
        /// </summary>
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

        /// <summary>
        /// An LDAPDN that is the base object entry relative to which the search is to be performed.
        /// </summary>
        public DEROctetString BaseObject { get; set; }
        /// <summary>
        /// An indicator of the scope of the search to be performed.
        /// </summary>
        public DEREnumerated<SearchRequestScopes> Scope { get; set; }
        public DEREnumerated<SearchRequestDeferAliases> DeferAlias { get; set; }
        /// <summary>
        /// A sizelimit that restricts the maximum number of entries to be returned as a result of the search.
        /// </summary>
        public DERInteger SizeLimit { get; set; }
        /// <summary>
        ///  A timelimit that restricts the maximum time (in seconds) allowed for a search.
        /// </summary>
        public DERInteger TimeLimit { get; set; }
        /// <summary>
        ///  An indicator as to whether search results will contain both attribute types and values, or just attribute types.
        /// </summary>
        public DERBoolean TypesOnly { get; set; }
        /// <summary>
        /// A filter that defines the conditions that must be fulfilled in order for the search to match a given entry.
        /// </summary>
        public SearchRequestFilter Filter { get; set; }
        /// <summary>
        /// A selection list of the attributes to be returned from each entry that match the search filter.
        /// </summary>
        public DERSequence<DEROctetString> Attributes { get; set; }

        public override ICollection<byte> Serialize()
        {
            var content = new List<byte>();
            content.AddRange(BaseObject.Serialize());
            content.AddRange(Scope.Serialize());
            content.AddRange(DeferAlias.Serialize());
            content.AddRange(SizeLimit.Serialize());
            content.AddRange(TimeLimit.Serialize());
            content.AddRange(TypesOnly.Serialize());
            content.AddRange(Filter.Serialize());
            content.AddRange(Attributes.Serialize());
            return content;
        }

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
    }
}