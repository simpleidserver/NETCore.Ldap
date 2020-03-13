// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.DER.Controls;
using NETCore.Ldap.DER.Universals;
using System.Collections.Generic;

namespace NETCore.Ldap.DER
{
    public class DERControl : DERStructure
    {
        public DEROctetString ControlType;

        public DERControl()
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

        public static DERControl Extract(ICollection<byte> buffer)
        {
            DERControl result = new DERControl();
            result.ExtractTagAndLength(buffer);
            if (result.Tag == null || result.Tag.UniversalClassType != UniversalClassTypes.Sequence)
            {
                return null;
            }

            result.ControlType = DEROctetString.Extract(buffer);
            if (result.ControlType.Value == "1.2.840.113556.1.4.319")
            {
                result = SimplePagedResultsControl.ExtractControl(buffer);
            }

            return result;
        }

        public override ICollection<byte> Serialize()
        {
            return SerializeControl();
        }

        public virtual ICollection<byte> SerializeControl()
        {
            throw new System.NotImplementedException();
        }
    }
}
