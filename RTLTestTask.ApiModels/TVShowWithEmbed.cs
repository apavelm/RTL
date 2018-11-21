using Newtonsoft.Json;

namespace RTLTestTask.ApiModels
{
    public class TVShowWithEmbed<TEmbedModel>
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("_embedded")] public TEmbedModel Embed { get; set; }
    }
}