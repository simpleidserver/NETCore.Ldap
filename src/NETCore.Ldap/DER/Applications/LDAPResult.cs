// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.DER.Universals;
using System.Collections.Generic;
using System.Linq;

namespace NETCore.Ldap.DER.Applications
{
    public enum LDAPResultCodes
    {
        /// <summary>
        /// Successful completion of an operation
        /// </summary>
        Success = 0,
        /// <summary>
        /// Operation is not properly sequenced with relation to other operations.
        /// For example, this code is returned if the client attempts to StartTLS [RFC4346] while there are other uncompleted operations or if a TLS layer was already installed.
        /// </summary>
        OperationsError = 1,
        /// <summary>
        /// Indicates the server received data that is not well-formed.
        /// </summary>
        ProtocolError = 2,
        TimeLimitExceeded = 3,
        SizeLimitExceeded = 4,
        CompareFalse = 5,
        CompareTrue = 6,
        AuthMethodNotSupported = 7,
        StrongerAuthRequired = 8,
        Referral = 10,
        AdminLimitExceeded = 11,
        UnavailableCriticalExtension = 12,
        ConfidentialityRequired = 13,
        SaslBindInProgress = 14,
        NoSuchAttribute = 16,
        UndefinedAttributeType = 17,
        InappropriateMatching = 18,
        ConstraintViolation = 19,
        AttributeOrValueExists = 20,
        InvalidAttributeSyntax = 21,
        NoSuchObject = 32,
        AliasProblem = 33,
        InvalidDNSyntax = 34,
        AliasDereferencingProblem = 36,
        InappropriateAuthentication = 48,
        InvalidCredentials = 49,
        InsufficientAccessRights = 50,
        Busy = 51,
        Unavailable = 52,
        UnwillingToPerform = 53,
        LoopDetect = 54,
        NamingViolation = 64,
        ObjectClassViolation = 65,
        NotAllowedOnNonLeaf = 66,
        NotAllowedOnRDN = 67,
        EntryAlreadyExists = 68,
        ObjectClassModsProhibited = 69,
        AffectsMultipleDSAs = 71,
        Other = 80
    }

    /// <summary>
    /// SEQUENCE {
    ///  resultCode         ENUMERATED {
    ///  
    /// }
    /// matchedDN          LDAPDN,
    /// diagnosticMessage  LDAPString
    /// }
    /// </summary>
    public class LDAPResult : DERStructure
    {
        public LDAPResult()
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

        public DEREnumerated<LDAPResultCodes> ResultCode { get; set; }
        public DEROctetString MatchedDN { get; set; }
        public DEROctetString DiagnosticMessage { get; set; }

        public static LDAPResult Extract(ICollection<byte> buffer)
        {
            var ldapResult = new LDAPResult();
            ldapResult.ResultCode = DEREnumerated<LDAPResultCodes>.Extract(buffer);
            ldapResult.MatchedDN = DEROctetString.Extract(buffer);
            ldapResult.DiagnosticMessage = DEROctetString.Extract(buffer);
            return ldapResult;
        }

        public override ICollection<byte> Serialize()
        {
            var content = new List<byte>();
            content.AddRange(ResultCode.Serialize());
            content.AddRange(MatchedDN.Serialize());
            content.AddRange(DiagnosticMessage.Serialize());
            Length = content.Count();

            var result = new List<byte>();
            result.AddRange(content);

            return result;
        }
    }
}
