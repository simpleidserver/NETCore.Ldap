// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.DER.Applications.Requests;
using NETCore.Ldap.DER.Universals;
using System;
using System.Collections.Generic;

namespace NETCore.Ldap.DER.Applications.AuthChoices
{
    public class SASLAuthChoice : BaseAuthChoice
    {
        public override BindRequestAuthenticationChoices Type => BindRequestAuthenticationChoices.SASL;

        public DEROctetString Mechanism { get; set; }
        public DEROctetString Credentials { get; set; }

        public static SASLAuthChoice Extract(ICollection<byte> buffer)
        {
            var result = new SASLAuthChoice();
            result.ExtractTagAndLength(buffer);
            result.Mechanism = DEROctetString.Extract(buffer);
            result.Credentials = DEROctetString.Extract(buffer);
            return result;
        }

        public override ICollection<byte> Serialize()
        {
            throw new NotImplementedException();
        }
    }
}
