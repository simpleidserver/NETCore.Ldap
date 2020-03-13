// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Microsoft.Extensions.Options;
using NETCore.Ldap.Commands;
using NETCore.Ldap.DER;
using NETCore.Ldap.Exceptions;
using System;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace NETCore.Ldap
{
    public class LdapServer
    {
        private readonly LdapServerOptions _options;
        private readonly ILdapService _ldapService;
        private TcpListener _tcpListener;
        private CancellationTokenSource _tokenSource;

        public LdapServer(IOptions<LdapServerOptions> options, ILdapService ldapService)
        {
            _options = options.Value;
            _ldapService = ldapService;
            _tcpListener = new TcpListener(_options.IpAdr, _options.Port);
        }

        public void Start()
        {
            _tokenSource = new CancellationTokenSource();
            _tcpListener.Start();
            var task = new Task(async () => await Handle(), _tokenSource.Token, TaskCreationOptions.LongRunning);
            task.Start();
        }

        public void Stop()
        {
            _tcpListener.Stop();
            _tokenSource.Cancel();
        }

        private async Task Handle()
        {
            while(!_tokenSource.IsCancellationRequested)
            {
                var client = await _tcpListener.AcceptTcpClientAsync();
                var task = new Task(async () => await HandleTcpClient(client), _tokenSource.Token, TaskCreationOptions.LongRunning);
                task.Start();
            }
        }

        private async Task HandleTcpClient(TcpClient client)
        {
            var stream = client.GetStream();
            bool isContinue = true;
            while (!_tokenSource.IsCancellationRequested && isContinue)
            {
                try
                {
                    byte[] buffer = new byte[1024];
                    stream.Read(buffer, 0, buffer.Length);
                    var ldapRequest = LdapPacket.Extract(buffer.ToList());
                    if (ldapRequest.ProtocolOperation.Operation == null)
                    {
                        continue;
                    }

                    try
                    {
                        /*
                        if (!await _ldapPacketOperation.Execute(ldapRequest, stream).ConfigureAwait(false))
                        {
                            isContinue = false;
                            client.Client.Disconnect(false);
                        }
                        */
                    }
                    catch (LdapException ex)
                    {
                        // var res = BuildError(ldapRequest, ex).Serialize();
                        // stream.Write(res.ToArray(), 0, res.Count());
                    }
                }
                catch (Exception ex)
                {
                    isContinue = false;
                    client.Client.Disconnect(false);
                }
            }
        }
    }
}
