using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace SalaryCalculator.IntegrationTests.Extensions;

public static class TestScenariosExtensions
{
    public static T? ReadResponse<T>(this HttpContent content)
    {
        var result = content.ReadAsStringAsync().GetAwaiter().GetResult();
        return JsonConvert.DeserializeObject<T>(result);
    }

    public static StringContent CreateContent<TInput>(this TInput input)
    {
        var content = new StringContent(input.ToJson(), Encoding.UTF8, "application/json");

        return content;
    }

    public static string ToJson(this object input)
    {
        return JsonConvert.SerializeObject(input);
    }

    public static StringContent CreateContent(this string input)
    {
        var content = new StringContent(input, Encoding.UTF8, "application/json");

        return content;
    }
}