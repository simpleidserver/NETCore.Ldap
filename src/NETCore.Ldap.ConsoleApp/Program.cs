// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Microsoft.Extensions.DependencyInjection;
using System;

namespace NETCore.Ldap.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLdapServer();
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var ldapServer = serviceProvider.GetService<ILdapServer>();
            ldapServer.Start();
            Console.Write("Press any key to quit the application");
            Console.ReadLine();
        }
    }
}