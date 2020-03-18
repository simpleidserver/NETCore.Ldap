// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Linq;

namespace NETCore.Ldap.Parser
{
    public class LDIFAttribute
    {
        public LDIFAttribute(string type, string value)
        {
            Type = type;
            Value = value;
        }

        public string Type { get; private set; }
        public string Value { get; private set; }

        public static LDIFAttribute Parse(string line)
        {
            var splitted = line.Split(':');
            return new LDIFAttribute(splitted.First(), splitted.Last());
        }
    }
}
