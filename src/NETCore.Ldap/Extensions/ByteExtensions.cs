using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleIdServer.Ldap.Core.Extensions
{
    public static class ByteExtensions
    {
        public static ICollection<char> ConcatBits(this byte b)
        {
            return Convert.ToString(b, 2).PadLeft(8, '0').ToCharArray().ToList();
        }

        public static ICollection<char> ConcatBits(this int b)
        {
            return Convert.ToString(b, 2).PadLeft(8, '0').ToCharArray().ToList();
        }

        public static string ConvertToBits(this int b)
        {
            return Convert.ToString(b, 2);
        }
    }
}
