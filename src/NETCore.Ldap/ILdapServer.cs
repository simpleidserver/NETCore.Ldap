// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace NETCore.Ldap
{
    public interface ILdapServer
    {
        void Start();
        void Stop();
        event EventHandler Started;
        event EventHandler Stopped;
        event EventHandler ClientConnected;
        event EventHandler ClientDisconnected;
    }
}
