Feature: SearchRequestError

Scenario: Search request and base object does not exist
	Given Add LDAP entry 'admin11'
	| Key          | Value                |
	| objectClass  | inetOrgPerson        |
	| objectClass  | organizationalPerson |
	| objectClass  | person               |
	| objectClass  | top                  |
	| cn           | administrator        |
	| sn           | administrator        |
	| uid          | administrator        |
	| userPassword | password             |
		
	When Authenticate user with login 'admin11', password 'password' and MessageId '1'
	And Search LDAP entries, base object is 'invalid', message identifier is '1'
	Then extract JSON 'searchResultDone', JSON 'MessageId.Value'='1'
	Then extract JSON 'searchResultDone', JSON 'ProtocolOperation.Operation.Result.ResultCode.Value'='32'	
	Then extract JSON 'searchResultDone', JSON 'ProtocolOperation.Operation.Result.DiagnosticMessage.Value'='entry 'invalid' doesn't exist'