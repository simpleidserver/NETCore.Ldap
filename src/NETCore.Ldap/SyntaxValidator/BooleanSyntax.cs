// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;

namespace NETCore.Ldap.SyntaxValidator
{
    public class BooleanSyntax : IAttributeSyntaxValidator
    {
        public string OID => LdapConstants.StandardAttributeSyntaxOIDS.BooleanOID;
        
        public bool Check(ICollection<string> values)
        {
            bool b;
            foreach (var value in values) 
            {
                if (!bool.TryParse(value, out b))
                {
                    return false;
                }
            }


            return true;
        }
    }
}
