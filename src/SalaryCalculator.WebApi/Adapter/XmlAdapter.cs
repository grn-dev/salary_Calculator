using System.Xml.Serialization;
using SalaryCalculator.WebApi.Application.Commands; 

namespace SalaryCalculator.WebApi.Adapter;

public class XmlAdapter : ISalaryEmployeeAdapter
{
    public XmlAdapter(string inputData) : base(inputData)
    {
    }

    public override CreateSalaryEmployeeCommand AdapteToCreateCommand()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(CreateSalaryEmployeeCommand));
        using (TextReader reader = new StringReader(inputData))
        {
            return (CreateSalaryEmployeeCommand)serializer.Deserialize(reader);
        }
    }
}