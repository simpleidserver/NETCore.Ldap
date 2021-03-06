﻿// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.DER.Universals;
using NETCore.Ldap.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NETCore.Ldap.DER.Applications.Filters
{
    public enum SearchRequestFilterTypes
    {
        And = 0,
        Or = 1,
        Not = 2,
        EqualityMatch = 3,
        SubStrings = 4,
        GreaterOrEqual = 5,
        LessOrEqual = 6,
        Present = 7,
        ApproxMatch = 8
    }

    /// <summary>
    /// SEQUENCE {
    //      attributeDesc AttributeDescription,
    //      assertionValue  AssertionValue
    // }
    /// </summary>
    public class AttributeValueAssertion : DERStructure
    {
        /// <summary>
        /// Attribute type and zero or more options.
        /// </summary>
        public DEROctetString AttributeDescription { get; set; }
        /// <summary>
        /// Matches a value of an attribute.
        /// </summary>
        public DEROctetString AssertionValue { get; set; }

        public static AttributeValueAssertion Extract(ICollection<byte> buffer, bool isTagIncluded = false)
        {
            var result = new AttributeValueAssertion();
            if (isTagIncluded)
            {
                result.ExtractTagAndLength(buffer);
            }

            result.AttributeDescription = DEROctetString.Extract(buffer);
            result.AssertionValue = DEROctetString.Extract(buffer);
            return result;
        }

        public override ICollection<byte> Serialize()
        {
            var content = new List<byte>();
            content.AddRange(AttributeDescription.Serialize());
            content.AddRange(AssertionValue.Serialize());

            Length = content.Count();
            var result = new List<byte>();
            if (Tag != null)
            {
                result.AddRange(SerializeDerStructure(true));
            }

            result.AddRange(content);
            return result;
        }
    }

    public class SearchRequestFilter : DERStructure
    {
        /// <summary>
        ///   A filter that defines the conditions that must be fulfilled in order for the Search to match a given entry.
        /// </summary>
        public SearchRequestFilterTypes Type { get; set; }        
        /// <summary>
        /// An attribute description is an attribute type and zero more options.
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// The AttributeValueAssertion (AVA) type definition is similar to the one in the X.500 Directory standards
        /// </summary>
        public AttributeValueAssertion Attribute { get; set; }
        /// <summary>
        /// Filters
        /// </summary>
        public ICollection<SearchRequestFilter> Filters { get; set; }

        public override ICollection<byte> Serialize()
        {
            var content = new List<byte>();
            var b = new DERTag
            {
                PcType = PcTypes.Primitive,
                TagClass = ClassTags.ContextSpecific,
                TagNumber = (int)Type
            }.Serialize();
            switch(Type)
            {
                case SearchRequestFilterTypes.Present:
                    var payload = Encoding.ASCII.GetBytes(Value);
                    content.AddRange(payload);
                    break;
                case SearchRequestFilterTypes.GreaterOrEqual:
                case SearchRequestFilterTypes.LessOrEqual:
                case SearchRequestFilterTypes.ApproxMatch:
                case SearchRequestFilterTypes.EqualityMatch:
                    content.AddRange(Attribute.Serialize());
                    break;
                case SearchRequestFilterTypes.Or:
                case SearchRequestFilterTypes.And:
                    foreach (var filter in Filters)
                    {
                        content.AddRange(filter.Serialize());
                    }

                    break;
            }

            Length = content.Count();
            var result = new List<byte>();
            result.AddRange(SerializeDerStructure(true, b));
            result.AddRange(content);
            return result;
        }
        
        public static SearchRequestFilter Extract(ICollection<byte> buffer)
        {
            var result = new SearchRequestFilter();
            result.ExtractTagAndLength(buffer);
            result.Type = (SearchRequestFilterTypes)result.Tag.TagNumber;
            switch (result.Type)
            {
                case SearchRequestFilterTypes.Present:
                    var valueBuffer = buffer.Dequeue(result.Length);
                    result.Value = Encoding.ASCII.GetString(valueBuffer.ToArray());
                    break;
                case SearchRequestFilterTypes.GreaterOrEqual:
                case SearchRequestFilterTypes.LessOrEqual:
                case SearchRequestFilterTypes.ApproxMatch:
                case SearchRequestFilterTypes.EqualityMatch:
                    result.Attribute = AttributeValueAssertion.Extract(buffer);
                    break;
                case SearchRequestFilterTypes.Or:
                case SearchRequestFilterTypes.And:
                    int i = 0;
                    var filters = new List<SearchRequestFilter>();
                    while (i < result.Length)
                    {
                        var searchRequestFilter = SearchRequestFilter.Extract(buffer);
                        if (searchRequestFilter == null)
                        {
                            break;
                        }

                        filters.Add(searchRequestFilter);
                        i += searchRequestFilter.Serialize().Count;
                    }

                    result.Filters = filters;
                    break;
            }

            return result;
        }
    }
}