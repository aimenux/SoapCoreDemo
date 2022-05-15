using SoapCore;
using WebApi.Services;

namespace WebApi;

public class Startup
{
    private const string SvcName = "SoapService.svc";
    private const string AsmxName = "SoapService.asmx";

    public void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddSoapCore();
        builder.Services.AddSingleton<ISoapService, SoapService>();
    }

    public void Configure(WebApplication app)
    {
        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.UseSoapEndpoint<ISoapService>($"/{AsmxName}", new SoapEncoderOptions(), SoapSerializer.XmlSerializer);
            endpoints.UseSoapEndpoint<ISoapService>($"/{SvcName}", new SoapEncoderOptions(), SoapSerializer.DataContractSerializer);
        });
    }
}