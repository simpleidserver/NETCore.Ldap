// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Net;

namespace NETCore.Ldap
{
    public class LdapServerOptions
    {
        public LdapServerOptions()
        {
            IpAdr = IPAddress.Parse("127.0.0.1");
            Port = 389;
        }

        public IPAddress IpAdr { get; set; }
        public int Port { get; set; }
    }
}
