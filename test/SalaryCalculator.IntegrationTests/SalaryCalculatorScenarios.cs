using System;
using System.Collections.Generic;
using System.Net;
using SalaryCalculator.FunctionalTests;
using SalaryCalculator.IntegrationTests.Extensions;
using SalaryCalculator.WebApi;
using SalaryCalculator.WebApi.Models;

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

    public class GetSalaryCalculatorScenarios : SalaryCalculatorScenarioBase
    {
        [Fact]
        public async Task When_Get_Expect_response_ok_status_code()
        {
            using var server = CreateServer();
            var url = Get.GetUrl("Amir", "Afshani", "14020801");
            var response = await server.CreateClient()
                .GetAsync(url);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            response.EnsureSuccessStatusCode();

            var employee = response.Content.ReadResponse<EmployeeDtoQuery>();
            Assert.NotNull(employee.EmployeeId);
            Assert.NotNull(employee.Allowance);
            Assert.NotNull(employee.Date);
            Assert.NotNull(employee.ReceivedSalary);
            Assert.NotNull(employee.FirstName);
            Assert.NotNull(employee.LastName);
        }

        [Fact]
        public async Task When_GetRange_Expect_response_ok_status_code()
        {
            using var server = CreateServer();
            var url = Get.GetRangeUrl("Amir", "Afshani", "14000801", "14020801");
            var response = await server.CreateClient()
                .GetAsync(url);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            response.EnsureSuccessStatusCode();

            var employee = response.Content.ReadResponse<List<EmployeeDtoQuery>>();
            Assert.NotEqual(0, employee.Count);
        }

        [Fact]
        public async Task When_Post_Get_Expect_response_ok_status_code()
        {
            using var server = CreateServer();
            var firstName = "Masoud";
            var LastName = "Rezaei";
            var Date = "14020801";

            #region Register

            var Json = $@"{{""FirstName"": ""{firstName}"",
            ""LastName"": ""{LastName}"",
            ""BasicSalary"": 50000.0,
            ""Allowance"": 3000.0,
            ""Transportation"": 2000.0,
            ""Date"": ""{Date}""}}";
            var request = new RegisterSalaryEmployeeRequest()
            {
                Data = Json,
                OverTimeCalculator = "CalcurlatorB"
            };
            var dataType = "json";
            var responseRegister = await server.CreateClient()
                .PostAsync(Post.PostSalaryEmployee(dataType),
                    request.CreateContent());

            Assert.Equal(HttpStatusCode.OK, responseRegister.StatusCode);

            #endregion

            #region Get

            var url = Get.GetUrl(firstName, LastName, Date);
            var response = await server.CreateClient()
                .GetAsync(url);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            response.EnsureSuccessStatusCode();

            var employee = response.Content.ReadResponse<EmployeeDtoQuery>();
            Assert.NotNull(employee.EmployeeId);
            Assert.NotNull(employee.Allowance);
            Assert.NotNull(employee.Date);
            Assert.NotNull(employee.ReceivedSalary);
            Assert.NotNull(employee.FirstName);
            Assert.NotNull(employee.LastName);

            var ReceivedSalaryMasoud = 50290.697674418604651162790698;
            var ReceivedSalaryMasoudRound = Math.Round(ReceivedSalaryMasoud, 0);
            Assert.Equal(ReceivedSalaryMasoudRound, Math.Round(Convert.ToDouble(employee.ReceivedSalary), 0));

            #endregion
        }
    }
}