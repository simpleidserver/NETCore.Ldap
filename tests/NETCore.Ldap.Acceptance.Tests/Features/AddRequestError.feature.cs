// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:3.0.0.0
//      SpecFlow Generator Version:3.0.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace NETCore.Ldap.Acceptance.Tests.Features
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.0.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class AddRequestErrorFeature : Xunit.IClassFixture<AddRequestErrorFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "AddRequestError.feature"
#line hidden
        
        public AddRequestErrorFeature(AddRequestErrorFeature.FixtureData fixtureData, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "AddRequestError", null, ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void TestInitialize()
        {
        }
        
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        void System.IDisposable.Dispose()
        {
            this.ScenarioTearDown();
        }
        
        [Xunit.FactAttribute(DisplayName="Try to add LDAP Entry but no LDAP session has been established")]
        [Xunit.TraitAttribute("FeatureTitle", "AddRequestError")]
        [Xunit.TraitAttribute("Description", "Try to add LDAP Entry but no LDAP session has been established")]
        public virtual void TryToAddLDAPEntryButNoLDAPSessionHasBeenEstablished()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Try to add LDAP Entry but no LDAP session has been established", null, ((string[])(null)));
#line 3
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "Key",
                        "Value"});
            table3.AddRow(new string[] {
                        "key",
                        "value"});
#line 4
 testRunner.When("Add LDAP entry \'invalid\' and MessageId \'1\'", ((string)(null)), table3, "When ");
#line 8
 testRunner.Then("LDAP Packet \'MessageId.Value\'=\'1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 9
 testRunner.Then("LDAP Packet \'ProtocolOperation.Operation.Result.ResultCode.Value\'=\'1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 10
 testRunner.Then("LDAP Packet \'ProtocolOperation.Operation.Result.DiagnosticMessage.Value\'=\'No LDAP" +
                    " session\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Add LDAP Entry but objectClass attribute is missing")]
        [Xunit.TraitAttribute("FeatureTitle", "AddRequestError")]
        [Xunit.TraitAttribute("Description", "Add LDAP Entry but objectClass attribute is missing")]
        public virtual void AddLDAPEntryButObjectClassAttributeIsMissing()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Add LDAP Entry but objectClass attribute is missing", null, ((string[])(null)));
#line 12
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line hidden
            TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                        "Key",
                        "Value"});
            table4.AddRow(new string[] {
                        "objectClass",
                        "inetOrgPerson"});
            table4.AddRow(new string[] {
                        "objectClass",
                        "organizationalPerson"});
            table4.AddRow(new string[] {
                        "objectClass",
                        "person"});
            table4.AddRow(new string[] {
                        "objectClass",
                        "top"});
            table4.AddRow(new string[] {
                        "cn",
                        "administrator"});
            table4.AddRow(new string[] {
                        "sn",
                        "administrator"});
            table4.AddRow(new string[] {
                        "uid",
                        "administrator"});
            table4.AddRow(new string[] {
                        "userPassword",
                        "password"});
#line 13
 testRunner.Given("Add LDAP entry \'admin1\'", ((string)(null)), table4, "Given ");
#line 24
 testRunner.When("Authenticate user with login \'admin1\', password \'password\' and MessageId \'1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                        "Key",
                        "Value"});
            table5.AddRow(new string[] {
                        "key",
                        "value"});
#line 25
 testRunner.And("Add LDAP entry \'invalid\' and MessageId \'1\'", ((string)(null)), table5, "And ");
#line 29
 testRunner.Then("LDAP Packet \'MessageId.Value\'=\'1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 30
 testRunner.Then("LDAP Packet \'ProtocolOperation.Operation.Result.ResultCode.Value\'=\'80\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 31
 testRunner.Then("LDAP Packet \'ProtocolOperation.Operation.Result.DiagnosticMessage.Value\'=\'Attribu" +
                    "te \'objectClass\' is missing\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Add existing LDAP entry")]
        [Xunit.TraitAttribute("FeatureTitle", "AddRequestError")]
        [Xunit.TraitAttribute("Description", "Add existing LDAP entry")]
        public virtual void AddExistingLDAPEntry()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Add existing LDAP entry", null, ((string[])(null)));
#line 33
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line hidden
            TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                        "Key",
                        "Value"});
            table6.AddRow(new string[] {
                        "objectClass",
                        "inetOrgPerson"});
            table6.AddRow(new string[] {
                        "objectClass",
                        "organizationalPerson"});
            table6.AddRow(new string[] {
                        "objectClass",
                        "person"});
            table6.AddRow(new string[] {
                        "objectClass",
                        "top"});
            table6.AddRow(new string[] {
                        "cn",
                        "administrator"});
            table6.AddRow(new string[] {
                        "sn",
                        "administrator"});
            table6.AddRow(new string[] {
                        "uid",
                        "administrator"});
            table6.AddRow(new string[] {
                        "userPassword",
                        "password"});
#line 34
 testRunner.Given("Add LDAP entry \'admin2\'", ((string)(null)), table6, "Given ");
#line 45
 testRunner.When("Authenticate user with login \'admin2\', password \'password\' and MessageId \'1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
                        "Key",
                        "Value"});
            table7.AddRow(new string[] {
                        "objectClass",
                        "inetOrgPerson"});
#line 46
 testRunner.And("Add LDAP entry \'admin2\' and MessageId \'1\'", ((string)(null)), table7, "And ");
#line 50
 testRunner.Then("LDAP Packet \'MessageId.Value\'=\'1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 51
 testRunner.Then("LDAP Packet \'ProtocolOperation.Operation.Result.ResultCode.Value\'=\'68\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 52
 testRunner.Then("LDAP Packet \'ProtocolOperation.Operation.Result.DiagnosticMessage.Value\'=\'Entry \'" +
                    "admin2\' already exists\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Add LDAP entry with unknown parent")]
        [Xunit.TraitAttribute("FeatureTitle", "AddRequestError")]
        [Xunit.TraitAttribute("Description", "Add LDAP entry with unknown parent")]
        public virtual void AddLDAPEntryWithUnknownParent()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Add LDAP entry with unknown parent", null, ((string[])(null)));
#line 54
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line hidden
            TechTalk.SpecFlow.Table table8 = new TechTalk.SpecFlow.Table(new string[] {
                        "Key",
                        "Value"});
            table8.AddRow(new string[] {
                        "objectClass",
                        "inetOrgPerson"});
            table8.AddRow(new string[] {
                        "objectClass",
                        "organizationalPerson"});
            table8.AddRow(new string[] {
                        "objectClass",
                        "person"});
            table8.AddRow(new string[] {
                        "objectClass",
                        "top"});
            table8.AddRow(new string[] {
                        "cn",
                        "administrator"});
            table8.AddRow(new string[] {
                        "sn",
                        "administrator"});
            table8.AddRow(new string[] {
                        "uid",
                        "administrator"});
            table8.AddRow(new string[] {
                        "userPassword",
                        "password"});
#line 55
 testRunner.Given("Add LDAP entry \'admin3\'", ((string)(null)), table8, "Given ");
#line 66
 testRunner.When("Authenticate user with login \'admin3\', password \'password\' and MessageId \'1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table9 = new TechTalk.SpecFlow.Table(new string[] {
                        "Key",
                        "Value"});
            table9.AddRow(new string[] {
                        "objectClass",
                        "inetOrgPerson"});
#line 67
 testRunner.And("Add LDAP entry \'uid=administrator,ou=computers,ou=system\' and MessageId \'1\'", ((string)(null)), table9, "And ");
#line 71
 testRunner.Then("LDAP Packet \'MessageId.Value\'=\'1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 72
 testRunner.Then("LDAP Packet \'ProtocolOperation.Operation.Result.ResultCode.Value\'=\'32\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 73
 testRunner.Then("LDAP Packet \'ProtocolOperation.Operation.Result.DiagnosticMessage.Value\'=\'Parent " +
                    "\'ou=computers,ou=system\' doesn\'t exist\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Add LDAP entry with unknown objectClass")]
        [Xunit.TraitAttribute("FeatureTitle", "AddRequestError")]
        [Xunit.TraitAttribute("Description", "Add LDAP entry with unknown objectClass")]
        public virtual void AddLDAPEntryWithUnknownObjectClass()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Add LDAP entry with unknown objectClass", null, ((string[])(null)));
#line 75
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line hidden
            TechTalk.SpecFlow.Table table10 = new TechTalk.SpecFlow.Table(new string[] {
                        "Key",
                        "Value"});
            table10.AddRow(new string[] {
                        "objectClass",
                        "inetOrgPerson"});
            table10.AddRow(new string[] {
                        "objectClass",
                        "organizationalPerson"});
            table10.AddRow(new string[] {
                        "objectClass",
                        "person"});
            table10.AddRow(new string[] {
                        "objectClass",
                        "top"});
            table10.AddRow(new string[] {
                        "cn",
                        "administrator"});
            table10.AddRow(new string[] {
                        "sn",
                        "administrator"});
            table10.AddRow(new string[] {
                        "uid",
                        "administrator"});
            table10.AddRow(new string[] {
                        "userPassword",
                        "password"});
#line 76
 testRunner.Given("Add LDAP entry \'admin4\'", ((string)(null)), table10, "Given ");
#line 87
 testRunner.When("Authenticate user with login \'admin4\', password \'password\' and MessageId \'1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table11 = new TechTalk.SpecFlow.Table(new string[] {
                        "Key",
                        "Value"});
            table11.AddRow(new string[] {
                        "objectClass",
                        "invalidObjectClass"});
            table11.AddRow(new string[] {
                        "objectClass",
                        "person"});
#line 88
 testRunner.And("Add LDAP entry \'uid=user,ou=users,ou=system\' and MessageId \'1\'", ((string)(null)), table11, "And ");
#line 93
 testRunner.Then("LDAP Packet \'MessageId.Value\'=\'1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 94
 testRunner.Then("LDAP Packet \'ProtocolOperation.Operation.Result.ResultCode.Value\'=\'80\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 95
 testRunner.Then("LDAP Packet \'ProtocolOperation.Operation.Result.DiagnosticMessage.Value\'=\'Unknown" +
                    " object classes \'invalidObjectClass\'\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Add LDAP entry and required attributes are missing")]
        [Xunit.TraitAttribute("FeatureTitle", "AddRequestError")]
        [Xunit.TraitAttribute("Description", "Add LDAP entry and required attributes are missing")]
        public virtual void AddLDAPEntryAndRequiredAttributesAreMissing()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Add LDAP entry and required attributes are missing", null, ((string[])(null)));
#line 97
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line hidden
            TechTalk.SpecFlow.Table table12 = new TechTalk.SpecFlow.Table(new string[] {
                        "Key",
                        "Value"});
            table12.AddRow(new string[] {
                        "objectClass",
                        "inetOrgPerson"});
            table12.AddRow(new string[] {
                        "objectClass",
                        "organizationalPerson"});
            table12.AddRow(new string[] {
                        "objectClass",
                        "person"});
            table12.AddRow(new string[] {
                        "objectClass",
                        "top"});
            table12.AddRow(new string[] {
                        "cn",
                        "administrator"});
            table12.AddRow(new string[] {
                        "sn",
                        "administrator"});
            table12.AddRow(new string[] {
                        "uid",
                        "administrator"});
            table12.AddRow(new string[] {
                        "userPassword",
                        "password"});
#line 98
 testRunner.Given("Add LDAP entry \'admin5\'", ((string)(null)), table12, "Given ");
#line 109
 testRunner.When("Authenticate user with login \'admin5\', password \'password\' and MessageId \'1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table13 = new TechTalk.SpecFlow.Table(new string[] {
                        "Key",
                        "Value"});
            table13.AddRow(new string[] {
                        "objectClass",
                        "person"});
#line 110
 testRunner.And("Add LDAP entry \'uid=user,ou=users,ou=system\' and MessageId \'1\'", ((string)(null)), table13, "And ");
#line 114
 testRunner.Then("LDAP Packet \'MessageId.Value\'=\'1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 115
 testRunner.Then("LDAP Packet \'ProtocolOperation.Operation.Result.ResultCode.Value\'=\'80\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 116
 testRunner.Then("LDAP Packet \'ProtocolOperation.Operation.Result.DiagnosticMessage.Value\'=\'Require" +
                    "d attributes \'sn,cn\' are missing\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Add LDAP entry and required attributes are undefined")]
        [Xunit.TraitAttribute("FeatureTitle", "AddRequestError")]
        [Xunit.TraitAttribute("Description", "Add LDAP entry and required attributes are undefined")]
        public virtual void AddLDAPEntryAndRequiredAttributesAreUndefined()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Add LDAP entry and required attributes are undefined", null, ((string[])(null)));
#line 118
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line hidden
            TechTalk.SpecFlow.Table table14 = new TechTalk.SpecFlow.Table(new string[] {
                        "Key",
                        "Value"});
            table14.AddRow(new string[] {
                        "objectClass",
                        "inetOrgPerson"});
            table14.AddRow(new string[] {
                        "objectClass",
                        "organizationalPerson"});
            table14.AddRow(new string[] {
                        "objectClass",
                        "person"});
            table14.AddRow(new string[] {
                        "objectClass",
                        "top"});
            table14.AddRow(new string[] {
                        "cn",
                        "administrator"});
            table14.AddRow(new string[] {
                        "sn",
                        "administrator"});
            table14.AddRow(new string[] {
                        "uid",
                        "administrator"});
            table14.AddRow(new string[] {
                        "userPassword",
                        "password"});
#line 119
 testRunner.Given("Add LDAP entry \'admin6\'", ((string)(null)), table14, "Given ");
#line 130
 testRunner.When("Authenticate user with login \'admin6\', password \'password\' and MessageId \'1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table15 = new TechTalk.SpecFlow.Table(new string[] {
                        "Key",
                        "Value"});
            table15.AddRow(new string[] {
                        "objectClass",
                        "person"});
            table15.AddRow(new string[] {
                        "objectClass",
                        "top"});
            table15.AddRow(new string[] {
                        "sn",
                        "surname"});
            table15.AddRow(new string[] {
                        "cn",
                        "commonname"});
            table15.AddRow(new string[] {
                        "bad",
                        "bad"});
#line 131
 testRunner.And("Add LDAP entry \'uid=user,ou=users,ou=system\' and MessageId \'1\'", ((string)(null)), table15, "And ");
#line 139
 testRunner.Then("LDAP Packet \'MessageId.Value\'=\'1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 140
 testRunner.Then("LDAP Packet \'ProtocolOperation.Operation.Result.ResultCode.Value\'=\'80\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 141
 testRunner.Then("LDAP Packet \'ProtocolOperation.Operation.Result.DiagnosticMessage.Value\'=\'Attribu" +
                    "tes \'bad\' are undefined\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Add LDAP entry with duplicate values")]
        [Xunit.TraitAttribute("FeatureTitle", "AddRequestError")]
        [Xunit.TraitAttribute("Description", "Add LDAP entry with duplicate values")]
        public virtual void AddLDAPEntryWithDuplicateValues()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Add LDAP entry with duplicate values", null, ((string[])(null)));
#line 143
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line hidden
            TechTalk.SpecFlow.Table table16 = new TechTalk.SpecFlow.Table(new string[] {
                        "Key",
                        "Value"});
            table16.AddRow(new string[] {
                        "objectClass",
                        "inetOrgPerson"});
            table16.AddRow(new string[] {
                        "objectClass",
                        "organizationalPerson"});
            table16.AddRow(new string[] {
                        "objectClass",
                        "person"});
            table16.AddRow(new string[] {
                        "objectClass",
                        "top"});
            table16.AddRow(new string[] {
                        "cn",
                        "administrator"});
            table16.AddRow(new string[] {
                        "sn",
                        "administrator"});
            table16.AddRow(new string[] {
                        "uid",
                        "administrator"});
            table16.AddRow(new string[] {
                        "userPassword",
                        "password"});
#line 144
 testRunner.Given("Add LDAP entry \'admin7\'", ((string)(null)), table16, "Given ");
#line 155
 testRunner.When("Authenticate user with login \'admin7\', password \'password\' and MessageId \'1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table17 = new TechTalk.SpecFlow.Table(new string[] {
                        "Key",
                        "Value"});
            table17.AddRow(new string[] {
                        "objectClass",
                        "person"});
            table17.AddRow(new string[] {
                        "objectClass",
                        "top"});
            table17.AddRow(new string[] {
                        "objectClass",
                        "metaAttributeType"});
            table17.AddRow(new string[] {
                        "sn",
                        "surname"});
            table17.AddRow(new string[] {
                        "cn",
                        "commonname"});
            table17.AddRow(new string[] {
                        "m-obsolete",
                        "true"});
            table17.AddRow(new string[] {
                        "m-obsolete",
                        "true"});
#line 156
 testRunner.And("Add LDAP entry \'uid=user,ou=users,ou=system\' and MessageId \'1\'", ((string)(null)), table17, "And ");
#line 166
 testRunner.Then("LDAP Packet \'MessageId.Value\'=\'1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 167
 testRunner.Then("LDAP Packet \'ProtocolOperation.Operation.Result.ResultCode.Value\'=\'20\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 168
 testRunner.Then("LDAP Packet \'ProtocolOperation.Operation.Result.DiagnosticMessage.Value\'=\'Attribu" +
                    "te \'m-obsolete\' must be a single value\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Add LDAP entry with bad syntax")]
        [Xunit.TraitAttribute("FeatureTitle", "AddRequestError")]
        [Xunit.TraitAttribute("Description", "Add LDAP entry with bad syntax")]
        public virtual void AddLDAPEntryWithBadSyntax()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Add LDAP entry with bad syntax", null, ((string[])(null)));
#line 170
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line hidden
            TechTalk.SpecFlow.Table table18 = new TechTalk.SpecFlow.Table(new string[] {
                        "Key",
                        "Value"});
            table18.AddRow(new string[] {
                        "objectClass",
                        "inetOrgPerson"});
            table18.AddRow(new string[] {
                        "objectClass",
                        "organizationalPerson"});
            table18.AddRow(new string[] {
                        "objectClass",
                        "person"});
            table18.AddRow(new string[] {
                        "objectClass",
                        "top"});
            table18.AddRow(new string[] {
                        "cn",
                        "administrator"});
            table18.AddRow(new string[] {
                        "sn",
                        "administrator"});
            table18.AddRow(new string[] {
                        "uid",
                        "administrator"});
            table18.AddRow(new string[] {
                        "userPassword",
                        "password"});
#line 171
 testRunner.Given("Add LDAP entry \'admin8\'", ((string)(null)), table18, "Given ");
#line 182
 testRunner.When("Authenticate user with login \'admin8\', password \'password\' and MessageId \'1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table19 = new TechTalk.SpecFlow.Table(new string[] {
                        "Key",
                        "Value"});
            table19.AddRow(new string[] {
                        "objectClass",
                        "person"});
            table19.AddRow(new string[] {
                        "objectClass",
                        "top"});
            table19.AddRow(new string[] {
                        "objectClass",
                        "metaAttributeType"});
            table19.AddRow(new string[] {
                        "sn",
                        "surname"});
            table19.AddRow(new string[] {
                        "cn",
                        "commonname"});
            table19.AddRow(new string[] {
                        "m-obsolete",
                        "str"});
#line 183
 testRunner.And("Add LDAP entry \'uid=user,ou=users,ou=system\' and MessageId \'1\'", ((string)(null)), table19, "And ");
#line 192
 testRunner.Then("LDAP Packet \'MessageId.Value\'=\'1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 193
 testRunner.Then("LDAP Packet \'ProtocolOperation.Operation.Result.ResultCode.Value\'=\'21\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 194
 testRunner.Then("LDAP Packet \'ProtocolOperation.Operation.Result.DiagnosticMessage.Value\'=\'Attribu" +
                    "te \'m-obsolete\' does not respect the syntax \'1.3.6.1.4.1.1466.115.121.1.7\'\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.0.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                AddRequestErrorFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                AddRequestErrorFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
