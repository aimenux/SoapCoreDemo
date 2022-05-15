using WebApi.Models;

namespace WebApi.Services;

public class SoapService : ISoapService
{
    public string Process(SoapModel model)
    {
        return $"Hello {model.Name}";
    }
}