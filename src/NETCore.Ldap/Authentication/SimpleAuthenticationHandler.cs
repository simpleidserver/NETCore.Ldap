// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Microsoft.Extensions.Options;
using NETCore.Ldap.DER.Applications;
using NETCore.Ldap.DER.Applications.AuthChoices;
using NETCore.Ldap.DER.Applications.Requests;
using NETCore.Ldap.Domain;
using NETCore.Ldap.Exceptions;
using NETCore.Ldap.MatchingRule;
using NETCore.Ldap.Persistence;
using NETCore.Ldap.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NETCore.Ldap.Authentication
{
    public class SimpleAuthenticationHandler : IAuthenticationHandler
    {
        private readonly LdapServerOptions _options;
        private readonly ILDAPEntryQueryStore _ldapEntryQueryStore;
        private readonly IMatchingRuleHandlerFactory _matchingRuleHandlerFactory;

        public SimpleAuthenticationHandler(IOptions<LdapServerOptions> options, ILDAPEntryQueryStore ldapEntryQueryStore, IMatchingRuleHandlerFactory matchingRuleHandlerFactory)
        {
            _options = options.Value;
            _ldapEntryQueryStore = ldapEntryQueryStore;
            _matchingRuleHandlerFactory = matchingRuleHandlerFactory;
        }

        public BindRequestAuthenticationChoices AuthChoice => BindRequestAuthenticationChoices.SIMPLE;

        public async Task Authenticate(LDAPEntry entry, BindRequest bindRequest)
        {
            var dn = bindRequest.Name.Value;
            var simpleAuth = bindRequest.Authentication as SimpleAuthChoice;
            var passwordAttribute = entry.Attributes.FirstOrDefault(a => a.Name == _options.UserPasswordAttributeName);
            if (passwordAttribute == null)
            {
                throw new LdapException(Global.NoUserPassword, LDAPResultCodes.Other, dn);
            }

            var attributeType = await _ldapEntryQueryStore.GetByAttribute(_options.NameAttributeName, passwordAttribute.Name);
            var equality = attributeType.Attributes.First(attr => attr.Name == _options.EqualityAttributeName).Values.First();
            var matchingRuleHandler = _matchingRuleHandlerFactory.Build(equality);
            var hashed = Build(simpleAuth.Value.Value, passwordAttribute.Values.First());
            if (!matchingRuleHandler.Handle(passwordAttribute, new List<string> { hashed }))
            {
                throw new LdapException(Global.InvalidCredentials, LDAPResultCodes.InvalidCredentials, dn);
            } 
        }

        private string Build(string receivedValue, string currentValue)
        {
            var regex = new Regex(@"({\w*})(\w*)");
            if (regex.IsMatch(currentValue))
            {
                // TODO : Externalize the HASH methods.
                var match = regex.Match(currentValue);
                var hashMethod = match.Groups[1].Value.TrimStart('{').TrimEnd('}');
                if (hashMethod == "SHA")
                {
                    var computed = ComputeSHA(Encoding.ASCII.GetBytes(receivedValue)).ToArray();
                    return "{SHA}" + Encoding.ASCII.GetString(computed);
                }
            }

            return receivedValue;
        }

        private static IEnumerable<byte> ComputeSHA(ICollection<byte> payload)
        {
            using (var sha1 = new SHA1Managed())
            {
                return sha1.ComputeHash(payload.ToArray());
            }
        }
    }
}