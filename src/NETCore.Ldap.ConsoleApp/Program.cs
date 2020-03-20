// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace NETCore.Ldap.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "LDIF.txt");
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLdapServer().ImportLDIF(path);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var ldapServer = serviceProvider.GetService<ILdapServer>();
            ldapServer.Start();
            ldapServer.ClientConnected += HandleClientConnected;
            ldapServer.ClientDisconnected += HandleClientDisconnected;
            Console.WriteLine("Press any key to quit the application");
            Console.ReadLine();
        }
        
        private static void HandleClientConnected(object sender, EventArgs e)
        {
            Console.WriteLine("Client is connected");
        }

        private static void HandleClientDisconnected(object sender, EventArgs e)
        {
            Console.WriteLine("Client is disconnected");
        }
    }
}