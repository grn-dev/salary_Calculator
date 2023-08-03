using System.Net;
using SalaryCalculator.FunctionalTests;
using SalaryCalculator.IntegrationTests.Extensions;
using SalaryCalculator.WebApi;

namespace SalaryCalculatoring.IntegrationTests
{
    public class RegisterSalaryCalculatorScenarios : SalaryCalculatorScenarioBase
    {
        [Fact]
        public async Task When_dataTypeIsCustom_Expect_response_ok_status_code()
        {
            using var server = CreateServer();
            var request = new RegisterSalaryEmployeeRequest()
            {
                Data = "Ali/Ahmadi/1200000/400000/350000/14010801",
                OverTimeCalculator = "CalcurlatorB"
            };
            var dataType = "custom";

            var response = await server.CreateClient()
                .PostAsync(Post.PostSalaryEmployee(dataType),
                    request.CreateContent());

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            //var s = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task When_dataType_json_Expect_response_ok_status_code()
        {
            using var server = CreateServer();
            var Json = @"{""FirstName"": ""mahmoud"",
            ""LastName"": ""sabzali"",
            ""BasicSalary"": 4200000.0,
            ""Allowance"": 500000.0,
            ""Transportation"": 750000.0,
            ""Date"": ""14020801""}";
            var request = new RegisterSalaryEmployeeRequest()
            {
                Data = Json,
                OverTimeCalculator = "CalcurlatorB"
            };
            var dataType = "json";

            var response = await server.CreateClient()
                .PostAsync(Post.PostSalaryEmployee(dataType),
                    request.CreateContent());

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            //var s = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task When_dataType_Xml_Expect_response_ok_status_code()
        {
            using var server = CreateServer();
            var xml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
                <root>
                    <FirstName>Amir</FirstName>
                    <LastName>Afshani</LastName>
                    <BasicSalary>45200000</BasicSalary>
                    <Allowance>5800000</Allowance>
                    <Transportation>710000</Transportation>
                    <Date>14020801</Date>
                </root>";
            var request = new RegisterSalaryEmployeeRequest()
            {
                Data = xml,
                OverTimeCalculator = "CalcurlatorB"
            };
            var dataType = "xml";

            var response = await server.CreateClient()
                .PostAsync(Post.PostSalaryEmployee(dataType),
                    request.CreateContent());

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            response.EnsureSuccessStatusCode();
        }
    }
}