// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Linq;

namespace NETCore.Ldap.Extensions
{
    public static class StringExtensions
    {
        public static ICollection<string> ExtractRDN(this string str)
        {
            return str.Split(',');
        }

        public static string ExtractParentDN(this string str)
        {
            var result = str.ExtractRDN();
            return string.Join(",", result.Skip(1));
        }

        public static string ExtractRootDN(this string str)
        {
            var result = str.ExtractRDN();
            return result.Last();
        }
    }
}
