﻿// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;

namespace NETCore.Ldap.DER.Universals
{
    /// <summary>
    /// SEQUENCE OF [type]
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DERSequence<T> : DERStructure where T : DERStructure
    {
        public DERSequence()
        {
            Values = new List<T>();
            Tag = new DERTag
            {
                LdapCommand = null,
                TagClass = ClassTags.Universal,
                UniversalClassType = UniversalClassTypes.Sequence,
                TagNumber = (int)UniversalClassTypes.Sequence,
                PcType = PcTypes.Constructed
            };
        }

        public ICollection<T> Values { get; set; }

        public static DERSequence<T> Extract(ICollection<byte> buffer)
        {
            var result = new DERSequence<T>();
            result.ExtractTagAndLength(buffer);
            int i = 0;
            while(i < result.Length)
            {
                var record = Activator.CreateInstance(typeof(T));
                var method = typeof(T).GetMethod("Extract");
                var derStructure = method.Invoke(record, new[] { buffer }) as T;
                if (derStructure == null)
                {
                    break;
                }

                i += derStructure.Serialize().Count();
                result.Values.Add(derStructure);
            }

            return result;
        }

        public override ICollection<byte> Serialize()
        {
            return Serialize(null);
        }

        public ICollection<byte> Serialize(byte? b = null)
        {
            var content = new List<byte>();
            foreach (var value in Values)
            {
                content.AddRange(value.Serialize());
            }

            Length = content.Count();
            var result = new List<byte>();
            result.AddRange(SerializeDerStructure(true, b));
            result.AddRange(content);

            return result;
        }
    }
}
