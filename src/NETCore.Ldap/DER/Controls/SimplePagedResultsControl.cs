// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.DER.Universals;
using System.Collections.Generic;
using System.Linq;

namespace NETCore.Ldap.DER.Controls
{
    public class RealSearchControlValue : DERStructure
    {
        public RealSearchControlValue()
        {
            Tag = new DERTag
            {
                LdapCommand = null,
                TagClass = ClassTags.Universal,
                UniversalClassType = UniversalClassTypes.Sequence,
                TagNumber = (int)UniversalClassTypes.Sequence,
                PcType = PcTypes.Constructed
            };
        }

        /// <summary>
        /// Requested page size from the client.
        /// </summary>
        public DERInteger Size { get; set; }
        public DEROctetString Cookie { get; set; }

        public static RealSearchControlValue Extract(ICollection<byte> buffer)
        {
            var result = new RealSearchControlValue();
            result.ExtractTagAndLength(buffer);
            result.Size = DERInteger.Extract(buffer);
            result.Cookie = DEROctetString.Extract(buffer);
            return result;
        }

        public override ICollection<byte> Serialize()
        {
            var content = new List<byte>();
            content.AddRange(Size.Serialize());
            content.AddRange(Cookie.Serialize());
            Length = content.Count;
            var result = new List<byte>();
            result.AddRange(SerializeDerStructure());
            result.AddRange(content);
            return result;
        }
    }

    public class SimplePagedResultsControl : DERControl
    {
        public SimplePagedResultsControl()
        {
            ControlType = new DEROctetString("1.2.840.113556.1.4.319");
        }

        public DERBoolean Criticality { get; set; }
        public RealSearchControlValue ControlValue { get; set; }

        public static SimplePagedResultsControl ExtractControl(ICollection<byte> buffer)
        {
            var result = new SimplePagedResultsControl();
            result.Criticality = DERBoolean.Extract(buffer);
            var controlValue = DEROctetString.Extract(buffer);
            result.ControlValue = RealSearchControlValue.Extract(controlValue.Payload);
            return result;
        }

        public override ICollection<byte> SerializeControl()
        {
            var content = new List<byte>();
            var controlValue = ControlValue.Serialize();
            var serialized = new DEROctetString
            {
                Payload = controlValue.ToList()
            };

            content.AddRange(ControlType.Serialize());
            content.AddRange(Criticality.Serialize());
            content.AddRange(serialized.Serialize());

            Length = content.Count;
            var result = new List<byte>();
            result.AddRange(SerializeDerStructure());
            result.AddRange(content);

            return result;
        }
    }
}
