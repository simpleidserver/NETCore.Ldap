// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.DER.Universals;
using System.Collections.Generic;
using System.Linq;

namespace NETCore.Ldap.DER.Applications.Requests
{
    public enum ModifyRequestChangeOperations
    {
        Add = 0,
        Delete = 1,
        Replace = 2
    }

    /// <summary>
    /// SEQUENCE {
    ///  operation       ENUMERATED {
    /// add     (0),
    /// delete  (1),
    ///  replace (2),
    ///  modification    PartialAttribute }
    /// </summary>
    public class ModifyRequestChange : DERStructure
    {
        public ModifyRequestChange()
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
        /// Used to specify the type of modification being performed.
        /// </summary>
        public DEREnumerated<ModifyRequestChangeOperations> Operation { get; set; }
        /// <summary>
        ///  A PartialAttribute (which may have an empty SET of vals) used to hold the attribute type or attribute type and  values being modified.
        /// </summary>
        public PartialAttribute Modification { get; set; }

        public override ICollection<byte> Serialize()
        {
            var content = new List<byte>();
            content.AddRange(Operation.Serialize());
            content.AddRange(Modification.Serialize());
            Length = content.Count();
            var result = new List<byte>();
            result.AddRange(SerializeDerStructure());
            result.AddRange(content);
            return result;
        }

        public static ModifyRequestChange Extract(ICollection<byte> buffer)
        {
            var result = new ModifyRequestChange();

            result.ExtractTagAndLength(buffer);
            result.Operation = DEREnumerated<ModifyRequestChangeOperations>.Extract(buffer);
            result.Modification = PartialAttribute.Extract(buffer);

            return result;
        }
    }

    /// <summary>
    /// [APPLICATION 6] SEQUENCE {
    ///  object          LDAPDN,
    ///  changes         SEQUENCE OF change SEQUENCE {
    ///  operation       ENUMERATED {
    /// add     (0),
    /// delete  (1),
    ///  replace (2),
    ///  modification    PartialAttribute } }
    /// </summary>
    public class ModifyRequest : DERApplicationType
    {
        public ModifyRequest()
        {
            Tag = new DERTag
            {
                LdapCommand = LdapCommands.ModifyRequest,
                TagClass = ClassTags.Application,
                TagNumber = (int)LdapCommands.ModifyRequest,
                PcType = PcTypes.Constructed
            };
        }

        /// <summary>
        /// The value of this field contains the name of the entry to be modified.
        /// </summary>
        public DEROctetString Object { get; set; }
        /// <summary>
        /// A list of modifications to be performed on the entry.
        /// </summary>
        public DERSequence<ModifyRequestChange> Changes { get; set; }

        public override ICollection<byte> Serialize()
        {
            throw new System.NotImplementedException();
        }

        public static ModifyRequest Extract(ICollection<byte> buffer)
        {
            var modifyRequest = new ModifyRequest();

            modifyRequest.Object = DEROctetString.Extract(buffer);
            modifyRequest.Changes = DERSequence<ModifyRequestChange>.Extract(buffer);

            return modifyRequest;
        }
    }
}
