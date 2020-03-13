// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;

namespace NETCore.Ldap.DER.Universals
{
    public class DERSet<T> : DERStructure where T : DERStructure
    {
        public DERSet()
        {
            Values = new List<T>();
            Tag = new DERTag
            {
                LdapCommand = null,
                TagClass = ClassTags.Universal,
                UniversalClassType = UniversalClassTypes.Set,
                TagNumber = (int)UniversalClassTypes.Set,
                PcType = PcTypes.Constructed
            };
        }

        public ICollection<T> Values { get; set; }

        public override ICollection<byte> Serialize()
        {
            var content = new List<byte>();
            foreach(var rec in Values)
            {
                content.AddRange(rec.Serialize());
            }

            Length = content.Count();
            var result = new List<byte>();
            result.AddRange(SerializeDerStructure(true));
            result.AddRange(content);
            return result;
        }

        public static DERSet<T> Extract(ICollection<byte> buffer)
        {
            var result = new DERSet<T>();
            result.ExtractTagAndLength(buffer);
            int i = 0;
            while (i < result.Length)
            {
                var record = Activator.CreateInstance(typeof(T));
                var method = typeof(T).GetMethod("Extract");
                var derStructure = method.Invoke(record, new[] { buffer }) as T;
                i += derStructure.Serialize().Count();
                result.Values.Add(derStructure);
            }

            return result;
        }
    }
}