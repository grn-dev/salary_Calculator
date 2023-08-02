namespace SalaryCalculator.WebApi;

public class RegisterSalaryEmployeeRequest
{
    public string Data { get; set; }
    public string OverTimeCalculator { get; set; }
}

public class GetSalaryEmployeeRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Date { get; set; }
}

public class GetRangeSalaryEmployeeRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FromDate { get; set; }
    public string ToDate { get; set; }
}

public class UpdateSalaryEmployeeRequest
{ 
    public int SalaryId { get; set; }
    public decimal BasicSalary { get; set; }
    public decimal Allowance { get; set; }
    public decimal Transportation { get; set; }
    public string Date { get; set; }
    public string OverTimeCalculator { get; set; }
}

public class DeleteSalaryEmployeeRequest
{
    public int Id { get; set; }
}
