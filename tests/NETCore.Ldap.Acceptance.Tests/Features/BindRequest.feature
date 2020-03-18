Feature: BindRequest

Scenario: Login and password authentication
	Given Add LDAP entry 'uid=administrator,ou=users,ou=system'
	| Key          | Value                |
	| objectClass  | inetOrgPerson        |
	| objectClass  | organizationalPerson |
	| objectClass  | person               |
	| objectClass  | top                  |
	| cn           | administrator        |
	| sn           | administrator        |
	| uid          | administrator        |
	| userPassword | password             |

	When Authenticate user with login 'uid=administrator,ou=users,ou=system', password 'password' and MessageId '1'
	Then LDAP Packet 'MessageId.Value'='1'
	Then LDAP Packet 'ProtocolOperation.Operation.Result.ResultCode.Value'='0'
	Then LDAP Packet 'ProtocolOperation.Operation.Result.MatchedDN.Value'='uid=administrator,ou=users,ou=system'