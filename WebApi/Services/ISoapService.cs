using System.ServiceModel;
using WebApi.Models;

namespace WebApi.Services;

[ServiceContract]
public interface ISoapService
{
    [OperationContract]
    string Process(SoapModel model);
}