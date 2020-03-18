// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using System.Linq;

namespace NETCore.Ldap.MatchingRule
{
    public class MatchingRuleHandlerFactory : IMatchingRuleHandlerFactory
    {
        private readonly IEnumerable<IMatchingRuleHandler> _matchingRuleHandlers;

        public MatchingRuleHandlerFactory(IEnumerable<IMatchingRuleHandler> matchingRuleHandlers)
        {
            _matchingRuleHandlers = matchingRuleHandlers;
        }

        public IMatchingRuleHandler Build(string matchingRuleName)
        {
            return _matchingRuleHandlers.FirstOrDefault(m => m.Name == matchingRuleName);
        }
    }
}
