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
    public partial class BindRequestErrorFeature : Xunit.IClassFixture<BindRequestErrorFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "BindRequestError.feature"
#line hidden
        
        public BindRequestErrorFeature(BindRequestErrorFeature.FixtureData fixtureData, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "BindRequestError", null, ProgrammingLanguage.CSharp, ((string[])(null)));
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
        
        [Xunit.FactAttribute(DisplayName="LDAP Entry does not exist")]
        [Xunit.TraitAttribute("FeatureTitle", "BindRequestError")]
        [Xunit.TraitAttribute("Description", "LDAP Entry does not exist")]
        public virtual void LDAPEntryDoesNotExist()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("LDAP Entry does not exist", null, ((string[])(null)));
#line 3
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 4
 testRunner.When("Authenticate user with login \'administrator\', password \'password\' and MessageId \'" +
                    "1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 5
 testRunner.Then("LDAP Packet \'MessageId.Value\'=\'1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 6
 testRunner.Then("LDAP Packet \'ProtocolOperation.Operation.Result.ResultCode.Value\'=\'32\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 7
 testRunner.Then("LDAP Packet \'ProtocolOperation.Operation.Result.MatchedDN.Value\'=\'administrator\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 8
 testRunner.Then("LDAP Packet \'ProtocolOperation.Operation.Result.DiagnosticMessage.Value\'=\'Entry \'" +
                    "administrator\' doesn\'t exist\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Authenticate with invalid credentials")]
        [Xunit.TraitAttribute("FeatureTitle", "BindRequestError")]
        [Xunit.TraitAttribute("Description", "Authenticate with invalid credentials")]
        public virtual void AuthenticateWithInvalidCredentials()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Authenticate with invalid credentials", null, ((string[])(null)));
#line 10
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "Key",
                        "Value"});
            table2.AddRow(new string[] {
                        "objectClass",
                        "inetOrgPerson"});
            table2.AddRow(new string[] {
                        "objectClass",
                        "organizationalPerson"});
            table2.AddRow(new string[] {
                        "objectClass",
                        "person"});
            table2.AddRow(new string[] {
                        "objectClass",
                        "top"});
            table2.AddRow(new string[] {
                        "cn",
                        "administrator"});
            table2.AddRow(new string[] {
                        "sn",
                        "administrator"});
            table2.AddRow(new string[] {
                        "uid",
                        "administrator"});
            table2.AddRow(new string[] {
                        "userPassword",
                        "password"});
#line 11
 testRunner.Given("Add LDAP entry \'admin\'", ((string)(null)), table2, "Given ");
#line 22
 testRunner.When("Authenticate user with login \'admin\', password \'invalidpassword\' and MessageId \'1" +
                    "\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 23
 testRunner.Then("LDAP Packet \'MessageId.Value\'=\'1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 24
 testRunner.Then("LDAP Packet \'ProtocolOperation.Operation.Result.ResultCode.Value\'=\'49\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 25
 testRunner.Then("LDAP Packet \'ProtocolOperation.Operation.Result.MatchedDN.Value\'=\'admin\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 26
 testRunner.Then("LDAP Packet \'ProtocolOperation.Operation.Result.DiagnosticMessage.Value\'=\'Passwor" +
                    "d is not correct\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.0.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                BindRequestErrorFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                BindRequestErrorFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
