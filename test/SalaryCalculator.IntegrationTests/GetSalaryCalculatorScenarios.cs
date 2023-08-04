using System;
using System.Collections.Generic;
using System.Net;
using SalaryCalculator.FunctionalTests;
using SalaryCalculator.IntegrationTests.Extensions;
using SalaryCalculator.WebApi;
using SalaryCalculator.WebApi.Models;

namespace SalaryCalculatoring.IntegrationTests;

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

        var employee = response.Content.ReadResponse<GetSalaryEmployeeQueriesResult>();
        Assert.NotNull(employee.EmployeeId);
        Assert.NotNull(employee.Allowance);
        Assert.NotNull(employee.Date);
        Assert.NotNull(employee.ReceivedSalary);
        Assert.NotNull(employee.FirstName);
        Assert.NotNull(employee.LastName);
    }

    [Fact]
    public async Task When_Update_SalaryInfo_Expect_response_ok_status_code_changed_info()
    {
        using var server = CreateServer();

        var url = Get.GetUrl("Amir", "Afshani", "14020801");
        var response = await server.CreateClient()
            .GetAsync(url);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        response.EnsureSuccessStatusCode();


        var employee = response.Content.ReadResponse<GetSalaryEmployeeQueriesResult>();
        var basicSalaryUpdate = employee.BasicSalary + 10000;

        UpdateSalaryEmployeeCommand updateSalaryEmployeeCommand = new UpdateSalaryEmployeeCommand()
        {
            Allowance = employee.Allowance,
            OverTimeCalculator = "CalcurlatorA",
            Date = employee.Date,
            BasicSalary = basicSalaryUpdate,
            Transportation = employee.Transportation,
            SalaryId = employee.SalaryId,
        };

        var responsePut = await server.CreateClient()
            .PutAsync(Put.PutUrl, updateSalaryEmployeeCommand.CreateContent());
        Assert.Equal(HttpStatusCode.OK, responsePut.StatusCode);


        var responseAfterUpdate = await server.CreateClient()
            .GetAsync(url);
        Assert.Equal(HttpStatusCode.OK, responseAfterUpdate.StatusCode);
        responseAfterUpdate.EnsureSuccessStatusCode();

        var employeeUpdated = responseAfterUpdate.Content.ReadResponse<GetSalaryEmployeeQueriesResult>();
        Assert.Equal(basicSalaryUpdate, employeeUpdated.BasicSalary);
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

        var employee = response.Content.ReadResponse<List<GetSalaryEmployeeQueriesResult>>();
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

        var employee = response.Content.ReadResponse<GetSalaryEmployeeQueriesResult>();
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

    [Fact]
    public async Task When_Delete_Expect_response_ok_status_code()
    {
        using var server = CreateServer();
        var firstName = "Masoud";
        var LastName = "RezaeiPour";
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

        var employee = response.Content.ReadResponse<GetSalaryEmployeeQueriesResult>();
        Assert.NotNull(employee.SalaryId);

        #endregion

        #region Delete

        var responsePut = await server.CreateClient()
            .DeleteAsync(string.Format(Delete.DeleteUrl, employee.SalaryId));
        Assert.Equal(HttpStatusCode.OK, responsePut.StatusCode);

        #endregion
    }
}