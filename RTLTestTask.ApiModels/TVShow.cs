using Newtonsoft.Json;

namespace RTLTestTask.ApiModels
{
    public class TVShow
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }
    }
}