using SalaryCalculator.FunctionalTests;
using SalaryCalculator.IntegrationTests.Extensions;
using SalaryCalculatoring.IntegrationTests.Extensions;

namespace SalaryCalculatoring.FunctionalTests
{
    public class SalaryCalculatorScenarios : IClassFixture<SalaryCalculatorScenarioBase>
    {
        private readonly SalaryCalculatorScenarioBase _scenarioBase;

        public SalaryCalculatorScenarios(SalaryCalculatorScenarioBase scenarioBase)
        {
            _scenarioBase = scenarioBase;
        }

        [Fact]
        public async Task When_StateUnderTest_Expect_ExpectedBehavior()
        {
            //_scenarioBase.CreateServer()

            // using var server = CreateServer();
            // var request = TestDataExtensions.GetRegisterSalaryEmployeeRequestDto();
            //
            //
            // var response = await server.CreateClient()
            //     .PostAsync(SalaryCalculatorScenarioBase.Post.PostSalaryEmployee("CalcurlatorB"),
            //         request.CreateContent());
            //
            // var s = await response.Content.ReadAsStringAsync();
            // response.EnsureSuccessStatusCode();
        }
    }

    public class RegisterEmployeeScenarios : SalaryCalculatorScenarioBase
    {
        [Theory]
        [InlineData("json")]
        [InlineData("cs")]
        [InlineData("custom")]
        [InlineData("xml")]
        public async Task When_StateUnderTest_Expect_ExpectedBehavior_2(string dataType)
        {
            using var server = CreateServer();
            var request = TestDataExtensions.GetRegisterSalaryEmployeeRequestDto();


            var response = await server.CreateClient()
                .PostAsync(Post.PostSalaryEmployee(dataType),
                    request.CreateContent());

            var s = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
        }
    }
}