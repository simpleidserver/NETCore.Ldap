Feature: AddRequest

Scenario: Add LDAP entry
	Given Add LDAP entry 'admin10'
	| Key          | Value                |
	| objectClass  | inetOrgPerson        |
	| objectClass  | organizationalPerson |
	| objectClass  | person               |
	| objectClass  | top                  |
	| cn           | administrator        |
	| sn           | administrator        |
	| uid          | administrator        |
	| userPassword | password             |
		
	When Authenticate user with login 'admin10', password 'password' and MessageId '1'
	And Add LDAP entry 'uid=newuser,ou=users,ou=system' and MessageId '1'
	| Key         | Value             |
	| objectClass | person            |
	| objectClass | top               |
	| sn          | surname           |
	| cn          | commonname        |
	
	Then LDAP Packet 'MessageId.Value'='1'
	Then LDAP Packet 'ProtocolOperation.Operation.Result.ResultCode.Value'='0'
	Then LDAP Packet 'ProtocolOperation.Operation.Result.MatchedDN.Value'='uid=newuser,ou=users,ou=system'