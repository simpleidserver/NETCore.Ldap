// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.DER.Universals;
using NETCore.Ldap.Extensions;
using SimpleIdServer.Ldap.Core.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NETCore.Ldap.DER.Applications.Requests
{
    /// <summary>
    /// Allows a client to change the Relative Distinguied Name (RDN) of an entry in the directory and / or to mobe a subtree of entries to a new location in the Directory.
    /// ModifyDNRequest ::= [APPLICATION 12] SEQUENCE {
    // entry LDAPDN,
    // newrdn          RelativeLDAPDN,
    // deleteoldrdn BOOLEAN,
    // newSuperior[0] LDAPDN OPTIONAL }
    /// </summary>
    public class ModifyDNRequest : DERApplicationType
    {
        public ModifyDNRequest()
        {
            Tag = new DERTag
            {
                LdapCommand = LdapCommands.ModifyDNRequest,
                TagClass = ClassTags.Application,
                TagNumber = (int)LdapCommands.ModifyDNRequest,
                PcType = PcTypes.Constructed
            };
        }

        /// <summary>
        /// Name of the entry to be changed.
        /// </summary>
        public DEROctetString Entry { get; set; }
        /// <summary>
        /// The new RND of the entry
        /// </summary>
        public DEROctetString NewRDN { get; set; }
        /// <summary>
        /// A boolean field that controls whether the old RND attribute values are to be retained as attributes of the entry or deleted from the entry.
        /// </summary>
        private DERBoolean DeleteOldRDN { get; set; }
        /// <summary>
        /// The new RDN of the entry.
        /// </summary>
        public DEROctetString NewSuperior { get; set; }

        public override ICollection<byte> Serialize()
        {
            throw new System.NotImplementedException();
        }

        public static ModifyDNRequest Extract(ICollection<byte> buffer)
        {
            var result = new ModifyDNRequest();

            result.Entry = DEROctetString.Extract(buffer);
            result.NewRDN = DEROctetString.Extract(buffer);
            result.DeleteOldRDN = DERBoolean.Extract(buffer);
            var newSuperior = new DEROctetString();
            newSuperior.ExtractTagAndLength(buffer);
            if (newSuperior.Length > 0)
            {
                var valueBuffer = buffer.Dequeue(newSuperior.Length);
                newSuperior.Value = Encoding.ASCII.GetString(valueBuffer.ToArray());
                result.NewSuperior = newSuperior;
            }

            return result;
        }
    }
}