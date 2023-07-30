using System.IO;
using Microsoft.Extensions.DependencyInjection;

namespace SalaryCalculator.FunctionalTests;

public class SalaryCalculatorScenarioBase
{
    private const string ApiUrlBase = "api/v1/basket";

    public TestServer CreateServer()
    {
        var factory = new SalaryCalculatorApplication();
        return factory.Server;
    }

    private class SalaryCalculatorApplication : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                //services.AddSingleton<IStartupFilter, AuthStartupFilter>();
            });

            builder.ConfigureAppConfiguration(c =>
            {
                //var directory = Path.GetDirectoryName(typeof(SalaryCalculatorScenarioBase).Assembly.Location)!;

                //c.AddJsonFile(Path.Combine(directory, "appsettings.SalaryCalculator.json"), optional: false);
            });

            return base.CreateHost(builder);
        }
    }


    public static class Get
    {
        public static string Get_ = "api/v1/SalaryCalculators";
        public static string GetAll = "api/v1/SalaryCalculators";
    }

    public static class Put
    {
        public static string CancelSalaryCalculator = "api/v1/SalaryCalculators/cancel";
    }

    public static class Post
    {
        public static string PostSalaryEmployee(string datatype)
        {
            return $"/SalaryEmployee/{datatype}/SalaryEmployee";
        }
    }

    public static class Delete
    {
        public static string CancelSalaryCalculator = "api/v1/SalaryCalculators/cancel";
    }
}