// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NETCore.Ldap.Extensions
{
    public static class CollectionExtensions
    {
        public static string Dequeue(this ICollection<char> chars)
        {
            var result = chars.First();
            chars.Remove(result);
            return result.ToString();
        }

        public static string Dequeue(this ICollection<char> chars, int size)
        {
            var result = chars.Take(size).ToList();
            for (var i = 0; i < size; i++)
            {
                chars.Remove(result.ElementAt(i));
            }

            return new string(result.ToArray());
        }

        public static byte Dequeue(this ICollection<byte> ba)
        {
            var result = ba.First();
            ba.Remove(result);
            return result;
        }

        public static ICollection<byte> Dequeue(this ICollection<byte> ba, int size)
        {
            var result = ba.Take(size).ToList();
            for (var i = 0; i < size; i++)
            {
                ba.Remove(result.ElementAt(i));
            }

            return result.ToList();
        }

        public static string ConvertToString(this ICollection<byte> ba)
        {
            var hex = new StringBuilder(ba.Count() * 2);
            foreach (byte b in ba)
            {
                hex.AppendFormat("{0:x2}", b);
            }

            return hex.ToString();
        }

        public static byte[] ConvertToBytes(this string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        public static int ConvertToInt32(this ICollection<byte> ba)
        {
            if (ba.Count() > 4)
            {
                // TODO : THROW EXCEPTION
            }

            var newBa = new List<byte>();
            if (ba.Count() != 4)
            {
                for (var i = 0; i < 4 - ba.Count(); i++)
                {
                    newBa.Add(0);
                }
            }

            newBa.AddRange(ba);
            newBa.Reverse();
            return BitConverter.ToInt32(newBa.ToArray(), 0);
        }
    }
}
