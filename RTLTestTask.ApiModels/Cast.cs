using Newtonsoft.Json;

namespace RTLTestTask.ApiModels
{
    public class Cast
    {
        [JsonProperty("person")] public CastPerson Person { get; set; }
    }
}
