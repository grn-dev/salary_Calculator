

using SalaryCalculator.Core;

namespace SalaryCalculator.UnitTest;

public class OverTimeServiceFactoryTest
{ 

    [Fact]
    public void When_calculatorType_is_CalcurlatorA_Expect_Type_Is_OverTimeCalculatorA()
    {
        //arrange
        List<IOverTimeCalculator> timeCalculators = new();
        OverTimeCalculatorA calculatorA = new();
        OverTimeCalculatorB calculatorB = new();
        OverTimeCalculatorC calculatorC = new();
        timeCalculators.Add(calculatorA);
        timeCalculators.Add(calculatorB);
        timeCalculators.Add(calculatorC);

        //act
        OverTimeServiceFactory serviceFactory = new(timeCalculators);
        var service = serviceFactory.GetService("CalcurlatorA");

        //Assert 
        Assert.Equal(calculatorA.GetType(), service.GetType());
        Assert.NotEqual(calculatorB.GetType(), service.GetType());
        Assert.NotEqual(calculatorC.GetType(), service.GetType());
    }
    
    [Fact]
    public void When_calculatorType_is_CalcurlatorB_Expect_Type_Is_OverTimeCalculatorB()
    {
        //arrange
        List<IOverTimeCalculator> timeCalculators = new();
        OverTimeCalculatorA calculatorA = new();
        OverTimeCalculatorB calculatorB = new();
        OverTimeCalculatorC calculatorC = new();
        timeCalculators.Add(calculatorA);
        timeCalculators.Add(calculatorB);
        timeCalculators.Add(calculatorC);

        //act
        OverTimeServiceFactory serviceFactory = new(timeCalculators);
        var service = serviceFactory.GetService("CalcurlatorB");

        //Assert 
        Assert.Equal(calculatorB.GetType(), service.GetType());
        
        Assert.NotEqual(calculatorA.GetType(), service.GetType());
        Assert.NotEqual(calculatorC.GetType(), service.GetType());
    }
    
    [Fact]
    public void When_calculatorType_is_CalcurlatorC_Expect_Type_Is_OverTimeCalculatorC()
    {
        //arrange
        List<IOverTimeCalculator> timeCalculators = new();
        OverTimeCalculatorA calculatorA = new();
        OverTimeCalculatorB calculatorB = new();
        OverTimeCalculatorC calculatorC = new();
        timeCalculators.Add(calculatorA);
        timeCalculators.Add(calculatorB);
        timeCalculators.Add(calculatorC);

        //act
        OverTimeServiceFactory serviceFactory = new(timeCalculators);
        var service = serviceFactory.GetService("CalcurlatorC");

        //Assert 
        Assert.Equal(calculatorC.GetType(), service.GetType());
        
        Assert.NotEqual(calculatorA.GetType(), service.GetType());
        Assert.NotEqual(calculatorB.GetType(), service.GetType());
    }
}