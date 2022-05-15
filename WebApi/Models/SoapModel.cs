using System.Runtime.Serialization;

namespace WebApi.Models;

[DataContract]
public class SoapModel
{
    [DataMember]
    public string Name { get; set; }
}