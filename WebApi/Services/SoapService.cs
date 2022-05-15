using WebApi.Models;

namespace WebApi.Services;

public class SoapService : ISoapService
{
    public string Run(SoapModel model)
    {
        return $"Hello {model.Name}";
    }
}