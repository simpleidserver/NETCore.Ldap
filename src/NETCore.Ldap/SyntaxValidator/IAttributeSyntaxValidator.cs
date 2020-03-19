// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;

namespace NETCore.Ldap.SyntaxValidator
{
    public interface IAttributeSyntaxValidator
    {
        string OID { get; }
        bool Check(ICollection<string> values);
    }
}
