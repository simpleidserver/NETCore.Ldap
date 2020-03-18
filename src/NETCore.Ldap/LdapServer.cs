// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NETCore.Ldap.Commands;
using NETCore.Ldap.DER;
using NETCore.Ldap.DER.Applications;
using NETCore.Ldap.DER.Applications.Responses;
using NETCore.Ldap.DER.Universals;
using NETCore.Ldap.Exceptions;
using NETCore.Ldap.Extensions;
using System;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace NETCore.Ldap
{
    public class LdapServer : ILdapServer
    {
        private readonly LdapServerOptions _options;
        private readonly ILdapService _ldapService;
        private readonly ILogger _logger;
        private TcpListener _tcpListener;
        private CancellationTokenSource _tokenSource;

        public LdapServer(IOptions<LdapServerOptions> options, ILdapService ldapService, ILogger<LdapServer> logger)
        {
            _options = options.Value;
            _ldapService = ldapService;
            _logger = logger;
            _tcpListener = new TcpListener(_options.IpAdr, _options.Port);
        }

        public event EventHandler Started;
        public event EventHandler Stopped;
        public event EventHandler ClientConnected;
        public event EventHandler ClientDisconnected;

        public void Start()
        {
            _tokenSource = new CancellationTokenSource();
            _tcpListener.Start();
            var task = new Task(async () => await Handle(), _tokenSource.Token, TaskCreationOptions.LongRunning);
            task.Start();
            if (Started != null)
            {
                Started(this, EventArgs.Empty);
            }
        }

        public void Stop()
        {
            _tokenSource.Cancel();
            _tcpListener.Stop();
            if (Stopped != null)
            {
                Stopped(this, EventArgs.Empty);
            }
        }

        private async Task Handle()
        {
            while(!_tokenSource.IsCancellationRequested)
            {
                try
                {
                    var client = await _tcpListener.AcceptTcpClientAsync().WithCancellation(_tokenSource.Token);
                    var task = new Task(async () => await HandleTcpClient(client), _tokenSource.Token, TaskCreationOptions.LongRunning);
                    task.Start();
                }
                catch(OperationCanceledException) { }
            }
        }

        private async Task HandleTcpClient(TcpClient client)
        {
            if(ClientConnected != null)
            {
                ClientConnected(this, EventArgs.Empty);
            }

            var stream = client.GetStream();            
            bool isContinue = true;
            while (!_tokenSource.IsCancellationRequested && isContinue)
            {
                LdapPacket ldapRequest = null;
                try
                {
                    byte[] buffer = new byte[1024];
                    stream.Read(buffer, 0, buffer.Length);
                    ldapRequest = LdapPacket.Extract(buffer.ToList());
                    var packetLst = await _ldapService.Handle(ldapRequest);
                    foreach (var packet in packetLst)
                    {
                        var payload = packet.Serialize();
                        await stream.WriteAsync(payload.ToArray(), 0, payload.Count());
                    }
                }
                catch (LdapException ex)
                {
                    _logger.LogError(ex, ex.Message);
                    var res = BuildError(ldapRequest, ex).Serialize();
                    await stream.WriteAsync(res.ToArray(), 0, res.Count());
                }
                catch (ObjectDisposedException)
                {
                    isContinue = false;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.ToString());
                    isContinue = false;
                    client.Client.Disconnect(false);
                }
            }

            if (ClientDisconnected != null)
            {
                ClientDisconnected(this, EventArgs.Empty);
            }
        }

        private static LdapPacket BuildError(LdapPacket request, LdapException ex)
        {
            BaseOperationDone operation = null;
            switch (request.ProtocolOperation.Tag.LdapCommand)
            {
                case LdapCommands.AddRequest:
                    operation = new AddResponse();
                    break;
                case LdapCommands.BindRequest:
                    operation = new BindResponse();
                    break;
                case LdapCommands.DelRequest:
                    operation = new DelResponse();
                    break;
                case LdapCommands.SearchRequest:
                    operation = new SearchResultDone();
                    break;
                case LdapCommands.ModifyRequest:
                    operation = new ModifyResponse();
                    break;
            }

            operation.Result = new LDAPResult
            {
                MatchedDN = new DEROctetString(ex.Target),
                DiagnosticMessage = new DEROctetString(ex.Message),
                ResultCode = new DEREnumerated<LDAPResultCodes>
                {
                    Value = ex.Code
                }
            };
            var ldapPacket = new LdapPacket
            {
                MessageId = request.MessageId,
                ProtocolOperation = new DERProtocolOperation
                {
                    Operation = operation
                }
            };

            return ldapPacket;
        }
    }
}
