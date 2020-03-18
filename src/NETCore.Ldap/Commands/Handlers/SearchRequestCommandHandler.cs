// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.DER.Applications.Requests;
using System.Threading.Tasks;

namespace NETCore.Ldap.Commands.Handlers
{
    public class SearchRequestCommandHandler : ISearchRequestCommandHandler
    {
        public async Task Handle(SearchRequestCommand searchRequestCommand)
        {
            var searchRequest = searchRequestCommand.ProtocolOperation.Operation as SearchRequest; 
            var dn = searchRequest.BaseObject.Value;
            if (string.IsNullOrWhiteSpace(dn))
            {
                await GetRootDSE(searchRequest);
                return;
            }
        }

        private async Task GetRootDSE(SearchRequest searchRequest)
        {
            // TODO : RETOURNER ROOTDSE.
        }
    }
}