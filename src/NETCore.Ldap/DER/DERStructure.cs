// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.Extensions;
using SimpleIdServer.Ldap.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NETCore.Ldap.DER
{
    public abstract class DERStructure
    {
        private const int MAX_OCTET = 4;
        public DERTag Tag { get; set; }
        public int Length { get; set; }

        public void ExtractTagAndLength(ICollection<byte> buffer)
        {
            var tag = DERTag.Extract(buffer);
            int length = ExtractLength(buffer);
            Tag = tag;
            Length = length;
        }

        protected ICollection<byte> SerializeDerStructure(bool skipCheck = false, byte? b = null)
        {
            var result = new List<byte>();
            if (b == null)
            {
                result.Add(Tag.Serialize());
            }
            else
            {
                result.Add(b.Value);
            }

            var bits = Length.ConcatBits();
            // https://en.wikipedia.org/wiki/X.690
            var sk = bits.SkipWhile(s => s == '0');
            if(sk.Count() > 7 || skipCheck)
            {
                var numberOfOctets = MAX_OCTET;
                var firstOctet = "1" + numberOfOctets.ConvertToBits().PadLeft(7, '0');
                var mod = sk.Count() % 8;
                var str = new string(sk.ToArray());
                str = str.PadLeft(8 * MAX_OCTET, '0');
                var combinedStr = firstOctet + str;
                for(int i = 0; i < combinedStr.Length / 8; i++)
                {
                    result.Add(Convert.ToByte(new string(combinedStr.Skip(i * 8).Take(8).ToArray()), 2));
                }
            }
            else
            {
                result.Add((byte)Length);
            }

            return result;
        }

        public abstract ICollection<byte> Serialize();

        protected static int ExtractLength(ICollection<byte> buffer)
        {
            int length = buffer.Dequeue();
            var bits = length.ConcatBits();
            if (bits.First() == '1')
            {
                bits = bits.Skip(1).ToList();
                var l = Convert.ToByte(new string(bits.ToArray()), 2);
                length = buffer.Dequeue(l).ConvertToInt32();
            }

            return length;
        }
    }
}
