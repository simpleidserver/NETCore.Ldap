// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.DER.Applications.Filters;
using NETCore.Ldap.DER.Universals;
using System.Collections.Generic;

namespace NETCore.Ldap.DER.Applications.Requests
{
    /// <summary>
    /// The Compare operation allows a client to compare an assertion value with the values of a particular attribute in a particular entry in the Directory.
    //  CompareRequest ::= [APPLICATION 14] SEQUENCE {
    //       entry LDAPDN,
    //       ava             AttributeValueAssertion
    //  
    /// </summary>
    public class CompareRequest : DERApplicationType
    {
        public CompareRequest()
        {
            Tag = new DERTag
            {
                LdapCommand = LdapCommands.ModifyDNRequest,
                TagClass = ClassTags.Application,
                TagNumber = (int)LdapCommands.ModifyDNRequest,
                PcType = PcTypes.Constructed
            };
        }

        /// <summary>
        /// The name of the entry to be compared.
        /// </summary>
        private DEROctetString Entry { get; set; }
        /// <summary>
        /// Holds the attribute value assertion to be compared.
        /// </summary>
        public AttributeValueAssertion Ava { get; set; }

        public static CompareRequest Extract(ICollection<byte> buffer)
        {
            var result = new CompareRequest();

            result.Entry = DEROctetString.Extract(buffer);
            result.Ava = AttributeValueAssertion.Extract(buffer, true);

            return result;
        }

        public override ICollection<byte> Serialize()
        {
            throw new System.NotImplementedException();
        }
    }
}
