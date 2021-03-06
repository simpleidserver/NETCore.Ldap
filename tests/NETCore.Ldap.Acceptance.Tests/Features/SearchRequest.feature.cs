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
    public partial class SearchRequestFeature : Xunit.IClassFixture<SearchRequestFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "SearchRequest.feature"
#line hidden
        
        public SearchRequestFeature(SearchRequestFeature.FixtureData fixtureData, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "SearchRequest", null, ProgrammingLanguage.CSharp, ((string[])(null)));
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
        
        [Xunit.FactAttribute(DisplayName="Get ROOT DSE")]
        [Xunit.TraitAttribute("FeatureTitle", "SearchRequest")]
        [Xunit.TraitAttribute("Description", "Get ROOT DSE")]
        public virtual void GetROOTDSE()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Get ROOT DSE", null, ((string[])(null)));
#line 3
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line hidden
            TechTalk.SpecFlow.Table table22 = new TechTalk.SpecFlow.Table(new string[] {
                        "Key",
                        "Value"});
            table22.AddRow(new string[] {
                        "objectClass",
                        "inetOrgPerson"});
            table22.AddRow(new string[] {
                        "objectClass",
                        "organizationalPerson"});
            table22.AddRow(new string[] {
                        "objectClass",
                        "person"});
            table22.AddRow(new string[] {
                        "objectClass",
                        "top"});
            table22.AddRow(new string[] {
                        "cn",
                        "administrator"});
            table22.AddRow(new string[] {
                        "sn",
                        "administrator"});
            table22.AddRow(new string[] {
                        "uid",
                        "administrator"});
            table22.AddRow(new string[] {
                        "userPassword",
                        "password"});
#line 4
 testRunner.Given("Add LDAP entry \'admin12\'", ((string)(null)), table22, "Given ");
#line 15
 testRunner.When("Authenticate user with login \'admin12\', password \'password\' and MessageId \'1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 16
 testRunner.And("Search LDAP entries, base object is \'\', message identifier is \'1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 17
 testRunner.Then("extract JSON \'searchResultEntry-0\', JSON \'MessageId.Value\'=\'1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 18
 testRunner.Then("extract JSON \'searchResultEntry-0\', JSON \'ProtocolOperation.Operation.PartialAttr" +
                    "ibutes.Values[?(@.Type.Value == \'vendorName\')].Vals.Values[0].Value\'=\'SimpleIdSe" +
                    "rver\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 19
 testRunner.Then("extract JSON \'searchResultEntry-0\', JSON \'ProtocolOperation.Operation.PartialAttr" +
                    "ibutes.Values[?(@.Type.Value == \'vendorVersion\')].Vals.Values[0].Value\'=\'1.0.0\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 20
 testRunner.Then("extract JSON \'searchResultEntry-0\', JSON \'ProtocolOperation.Operation.PartialAttr" +
                    "ibutes.Values[?(@.Type.Value == \'supportedLDAPVersion\')].Vals.Values[0].Value\'=\'" +
                    "3\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.0.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                SearchRequestFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                SearchRequestFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
