using Moq;
using SalaryCalculator.Core;
using SalaryCalculator.Core.Models;

namespace SalaryCalculator.UnitTest;

public class SalaryTest
{
    [Fact]
    public void When_ValidInput_Expect_True_CalculateReceived()
    {
        Mock<IOverTimeCalculator> overTime = new();

        Salary salary = new(10000, 1000, 1000, "14020802");

        salary.CalculateReceived(overTime.Object);

        Assert.Equal(11000, salary.ReceivedSalary);
    }

    [Theory]
    [InlineData("1402")]
    [InlineData("")]
    [InlineData("140201")]
    [InlineData("1402015")]
    [InlineData("140201553221121")]
    public void When_InDate_Expect_InvalidDateException(string date)
    {
        try
        {
            Salary salary = new(10000, 1000, 1000, date);
        }
        catch (Exception e)
        {
            Assert.Contains("date invalid", e.Message);
        }
    }


    [Theory]
    [InlineData(0)]
    [InlineData(-454545)]
    [InlineData(-1000)]
    public void When_BasicSalaryInvalid_Expect_InvalidBasicSalaryException(decimal basicSalary)
    {
        try
        {
            Salary salary = new(basicSalary, 1000, 1000, "14020203");
        }
        catch (Exception e)
        {
            Assert.Contains("basicSalary must bigger than zero", e.Message);
        }
    }

    [Theory]
    [InlineData(1,100)] 
    [InlineData(1000,1001)] 
    [InlineData(50000,700000)] 
    public void When_transportationBiggerThanBasicSalaryInvalid_Expect_InvalidBasicSalaryException(decimal basicSalary,decimal transportation)
    {
        try
        {
            Salary salary = new(basicSalary, 1000, transportation, "14020203");
        }
        catch (Exception e)
        {
            Assert.Contains("transportation should not be more than a basicSalary", e.Message);
        }
    }
}