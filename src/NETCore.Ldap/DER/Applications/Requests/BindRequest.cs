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
        Simple = 0,
        SASL = 3,
        SicilyPackageDiscovery = 9,
        SicilyNegotiate = 10,
        SicilyResponse = 11
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
        public DERInteger Version { get; set; }
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
                case BindRequestAuthenticationChoices.Simple:
                    result.Authentication = SimpleAuthChoice.Extract(buffer);
                    break;
            }

            return result;
        }

        public override ICollection<byte> Serialize()
        {
            throw new System.NotImplementedException();
        }
    }
}