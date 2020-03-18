// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.DER.Applications.AuthChoices;
using NETCore.Ldap.DER.Universals;
using System.Collections.Generic;
using System.Linq;

namespace NETCore.Ldap.DER.Applications.Requests
{
    public enum BindRequestAuthenticationChoices
    {
        SIMPLE = 0,
        SASL = 3
    }


    /// <summary>
    /// [APPLICATION 0] SEQUENCE {
    /// version                 INTEGER (1 ..  127),
    /// name                    LDAPDN,
    /// authentication          AuthenticationChoice 
    /// }
    /// </summary>
    public class BindRequest : DERApplicationType
    {
        public BindRequest()
        {
            Tag = new DERTag
            {
                LdapCommand = LdapCommands.BindRequest,
                TagClass = ClassTags.Application,
                TagNumber = (int)LdapCommands.BindRequest,
                PcType = PcTypes.Constructed
            };
        }

        /// <summary>
        /// A version number indicating the version of the protocol to be used in this protocol session.
        /// </summary>
        public DERInteger Version { get; set; }
        /// <summary>
        /// The name (DN) of the directory object that the client wishes to bind as.
        /// </summary>
        public DEROctetString Name { get; set; }
        public BaseAuthChoice Authentication { get; set; }

        public static BindRequest Extract(ICollection<byte> buffer)
        {
            var result = new BindRequest();
            result.Version = DERInteger.Extract(buffer);
            result.Name = DEROctetString.Extract(buffer);
            var cloneBuffer = buffer.ToList();
            var tag = DERTag.Extract(cloneBuffer);
            var authMethod = (BindRequestAuthenticationChoices)tag.TagNumber;
            switch(authMethod)
            {
                case BindRequestAuthenticationChoices.SASL:
                    result.Authentication = SASLAuthChoice.Extract(buffer);
                    break;
                case BindRequestAuthenticationChoices.SIMPLE:
                    result.Authentication = SimpleAuthChoice.Extract(buffer);
                    break;
            }

            return result;
        }

        public override ICollection<byte> Serialize()
        {
            var result = new List<byte>();
            result.AddRange(Version.Serialize());
            result.AddRange(Name.Serialize());
            var flag = new DERTag
            {
                PcType = PcTypes.Primitive,
                TagClass = ClassTags.ContextSpecific,
                TagNumber = (int)Authentication.Type
            };
            result.Add(flag.Serialize());
            result.AddRange(Authentication.Serialize());
            return result;
        }
    }
}