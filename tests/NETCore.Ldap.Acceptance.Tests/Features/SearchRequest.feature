Feature: SearchRequest

Scenario: Get ROOT DSE
	Given Add LDAP entry 'admin12'
	| Key          | Value                |
	| objectClass  | inetOrgPerson        |
	| objectClass  | organizationalPerson |
	| objectClass  | person               |
	| objectClass  | top                  |
	| cn           | administrator        |
	| sn           | administrator        |
	| uid          | administrator        |
	| userPassword | password             |
		
	When Authenticate user with login 'admin12', password 'password' and MessageId '1'
	And Search LDAP entries, base object is '', message identifier is '1'
	Then extract JSON 'searchResultEntry-0', JSON 'MessageId.Value'='1'
	Then extract JSON 'searchResultEntry-0', JSON 'ProtocolOperation.Operation.PartialAttributes.Values[?(@.Type.Value == 'vendorName')].Vals.Values[0].Value'='SimpleIdServer'
	Then extract JSON 'searchResultEntry-0', JSON 'ProtocolOperation.Operation.PartialAttributes.Values[?(@.Type.Value == 'vendorVersion')].Vals.Values[0].Value'='1.0.0'
	Then extract JSON 'searchResultEntry-0', JSON 'ProtocolOperation.Operation.PartialAttributes.Values[?(@.Type.Value == 'supportedLDAPVersion')].Vals.Values[0].Value'='3'