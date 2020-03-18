Feature: BindRequestError

Scenario: LDAP Entry does not exist
	When Authenticate user with login 'administrator', password 'password' and MessageId '1'
	Then LDAP Packet 'MessageId.Value'='1'
	Then LDAP Packet 'ProtocolOperation.Operation.Result.ResultCode.Value'='32'
	Then LDAP Packet 'ProtocolOperation.Operation.Result.MatchedDN.Value'='administrator'
	Then LDAP Packet 'ProtocolOperation.Operation.Result.DiagnosticMessage.Value'='Entry 'administrator' doesn't exist'

Scenario: Authenticate with invalid credentials
	Given Add LDAP entry 'admin'
	| Key          | Value                |
	| objectClass  | inetOrgPerson        |
	| objectClass  | organizationalPerson |
	| objectClass  | person               |
	| objectClass  | top                  |
	| cn           | administrator        |
	| sn           | administrator        |
	| uid          | administrator        |
	| userPassword | password             |

	When Authenticate user with login 'admin', password 'invalidpassword' and MessageId '1'
	Then LDAP Packet 'MessageId.Value'='1'
	Then LDAP Packet 'ProtocolOperation.Operation.Result.ResultCode.Value'='49'
	Then LDAP Packet 'ProtocolOperation.Operation.Result.MatchedDN.Value'='admin'
	Then LDAP Packet 'ProtocolOperation.Operation.Result.DiagnosticMessage.Value'='Password is not correct'