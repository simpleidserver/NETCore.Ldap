// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.DER;
using NETCore.Ldap.DER.Universals;

namespace NETCore.Ldap.Commands
{
    public class AddRequestCommand
    {
        public DERInteger MessageId { get; set; }
        public DERProtocolOperation ProtocolOperation { get; set; }
        public DERSequence<DERControl> Controls { get; set; }
    }
}
