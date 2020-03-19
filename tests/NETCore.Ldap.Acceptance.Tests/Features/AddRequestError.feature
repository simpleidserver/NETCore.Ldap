Feature: AddRequestError

Scenario: Try to add LDAP Entry but no LDAP session has been established
	When Add LDAP entry 'invalid' and MessageId '1'
	| Key | Value |
	| key | value |

	Then LDAP Packet 'MessageId.Value'='1'	
	Then LDAP Packet 'ProtocolOperation.Operation.Result.ResultCode.Value'='1'	
	Then LDAP Packet 'ProtocolOperation.Operation.Result.DiagnosticMessage.Value'='No LDAP session'

Scenario: Add LDAP Entry but objectClass attribute is missing	
	Given Add LDAP entry 'admin1'
	| Key          | Value                |
	| objectClass  | inetOrgPerson        |
	| objectClass  | organizationalPerson |
	| objectClass  | person               |
	| objectClass  | top                  |
	| cn           | administrator        |
	| sn           | administrator        |
	| uid          | administrator        |
	| userPassword | password             |

	When Authenticate user with login 'admin1', password 'password' and MessageId '1'
	And Add LDAP entry 'invalid' and MessageId '1'
	| Key | Value |
	| key | value |

	Then LDAP Packet 'MessageId.Value'='1'
	Then LDAP Packet 'ProtocolOperation.Operation.Result.ResultCode.Value'='80'	
	Then LDAP Packet 'ProtocolOperation.Operation.Result.DiagnosticMessage.Value'='Attribute 'objectClass' is missing'

Scenario: Add existing LDAP entry	
	Given Add LDAP entry 'admin2'
	| Key          | Value                |
	| objectClass  | inetOrgPerson        |
	| objectClass  | organizationalPerson |
	| objectClass  | person               |
	| objectClass  | top                  |
	| cn           | administrator        |
	| sn           | administrator        |
	| uid          | administrator        |
	| userPassword | password             |

	When Authenticate user with login 'admin2', password 'password' and MessageId '1'
	And Add LDAP entry 'admin2' and MessageId '1'
	| Key         | Value         |
	| objectClass | inetOrgPerson |

	Then LDAP Packet 'MessageId.Value'='1'
	Then LDAP Packet 'ProtocolOperation.Operation.Result.ResultCode.Value'='68'	
	Then LDAP Packet 'ProtocolOperation.Operation.Result.DiagnosticMessage.Value'='Entry 'admin2' already exists'

Scenario: Add LDAP entry with unknown parent
	Given Add LDAP entry 'admin3'
	| Key          | Value                |
	| objectClass  | inetOrgPerson        |
	| objectClass  | organizationalPerson |
	| objectClass  | person               |
	| objectClass  | top                  |
	| cn           | administrator        |
	| sn           | administrator        |
	| uid          | administrator        |
	| userPassword | password             |
		
	When Authenticate user with login 'admin3', password 'password' and MessageId '1'
	And Add LDAP entry 'uid=administrator,ou=computers,ou=system' and MessageId '1'
	| Key         | Value         |
	| objectClass | inetOrgPerson |	

	Then LDAP Packet 'MessageId.Value'='1'
	Then LDAP Packet 'ProtocolOperation.Operation.Result.ResultCode.Value'='32'	
	Then LDAP Packet 'ProtocolOperation.Operation.Result.DiagnosticMessage.Value'='Parent 'ou=computers,ou=system' doesn't exist'

Scenario: Add LDAP entry with unknown objectClass
	Given Add LDAP entry 'admin4'
	| Key          | Value                |
	| objectClass  | inetOrgPerson        |
	| objectClass  | organizationalPerson |
	| objectClass  | person               |
	| objectClass  | top                  |
	| cn           | administrator        |
	| sn           | administrator        |
	| uid          | administrator        |
	| userPassword | password             |
		
	When Authenticate user with login 'admin4', password 'password' and MessageId '1'
	And Add LDAP entry 'uid=user,ou=users,ou=system' and MessageId '1'
	| Key         | Value              |
	| objectClass | invalidObjectClass |
	| objectClass | person             |
	
	Then LDAP Packet 'MessageId.Value'='1'
	Then LDAP Packet 'ProtocolOperation.Operation.Result.ResultCode.Value'='80'	
	Then LDAP Packet 'ProtocolOperation.Operation.Result.DiagnosticMessage.Value'='Unknown object classes 'invalidObjectClass''

Scenario: Add LDAP entry and required attributes are missing
	Given Add LDAP entry 'admin5'
	| Key          | Value                |
	| objectClass  | inetOrgPerson        |
	| objectClass  | organizationalPerson |
	| objectClass  | person               |
	| objectClass  | top                  |
	| cn           | administrator        |
	| sn           | administrator        |
	| uid          | administrator        |
	| userPassword | password             |
		
	When Authenticate user with login 'admin5', password 'password' and MessageId '1'
	And Add LDAP entry 'uid=user,ou=users,ou=system' and MessageId '1'
	| Key         | Value              |
	| objectClass | person             |
	
	Then LDAP Packet 'MessageId.Value'='1'
	Then LDAP Packet 'ProtocolOperation.Operation.Result.ResultCode.Value'='80'	
	Then LDAP Packet 'ProtocolOperation.Operation.Result.DiagnosticMessage.Value'='Required attributes 'sn,cn' are missing'

Scenario: Add LDAP entry and required attributes are undefined
	Given Add LDAP entry 'admin6'
	| Key          | Value                |
	| objectClass  | inetOrgPerson        |
	| objectClass  | organizationalPerson |
	| objectClass  | person               |
	| objectClass  | top                  |
	| cn           | administrator        |
	| sn           | administrator        |
	| uid          | administrator        |
	| userPassword | password             |
		
	When Authenticate user with login 'admin6', password 'password' and MessageId '1'
	And Add LDAP entry 'uid=user,ou=users,ou=system' and MessageId '1'
	| Key         | Value      |
	| objectClass | person     |
	| objectClass | top        |
	| sn          | surname    |
	| cn          | commonname |
	| bad         | bad        |
	
	Then LDAP Packet 'MessageId.Value'='1'
	Then LDAP Packet 'ProtocolOperation.Operation.Result.ResultCode.Value'='80'	
	Then LDAP Packet 'ProtocolOperation.Operation.Result.DiagnosticMessage.Value'='Attributes 'bad' are undefined'

Scenario: Add LDAP entry with duplicate values
	Given Add LDAP entry 'admin7'
	| Key          | Value                |
	| objectClass  | inetOrgPerson        |
	| objectClass  | organizationalPerson |
	| objectClass  | person               |
	| objectClass  | top                  |
	| cn           | administrator        |
	| sn           | administrator        |
	| uid          | administrator        |
	| userPassword | password             |
		
	When Authenticate user with login 'admin7', password 'password' and MessageId '1'
	And Add LDAP entry 'uid=user,ou=users,ou=system' and MessageId '1'
	| Key         | Value             |
	| objectClass | person            |
	| objectClass | top               |
	| objectClass | metaAttributeType |
	| sn          | surname           |
	| cn          | commonname        |
	| m-obsolete  | true              |
	| m-obsolete  | true              |
	
	Then LDAP Packet 'MessageId.Value'='1'
	Then LDAP Packet 'ProtocolOperation.Operation.Result.ResultCode.Value'='20'	
	Then LDAP Packet 'ProtocolOperation.Operation.Result.DiagnosticMessage.Value'='Attribute 'm-obsolete' must be a single value'
	
Scenario: Add LDAP entry with bad syntax
	Given Add LDAP entry 'admin8'
	| Key          | Value                |
	| objectClass  | inetOrgPerson        |
	| objectClass  | organizationalPerson |
	| objectClass  | person               |
	| objectClass  | top                  |
	| cn           | administrator        |
	| sn           | administrator        |
	| uid          | administrator        |
	| userPassword | password             |
		
	When Authenticate user with login 'admin8', password 'password' and MessageId '1'
	And Add LDAP entry 'uid=user,ou=users,ou=system' and MessageId '1'
	| Key         | Value             |
	| objectClass | person            |
	| objectClass | top               |
	| objectClass | metaAttributeType |
	| sn          | surname           |
	| cn          | commonname        |
	| m-obsolete  | str               |
	
	Then LDAP Packet 'MessageId.Value'='1'
	Then LDAP Packet 'ProtocolOperation.Operation.Result.ResultCode.Value'='21'	
	Then LDAP Packet 'ProtocolOperation.Operation.Result.DiagnosticMessage.Value'='Attribute 'm-obsolete' does not respect the syntax '1.3.6.1.4.1.1466.115.121.1.7''