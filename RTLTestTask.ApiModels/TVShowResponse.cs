using System.Collections.Generic;
using Newtonsoft.Json;

namespace RTLTestTask.ApiModels
{
    public class TVShowResponse
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("cast")] public virtual ICollection<CastPerson> Casts { get; set; }
    }
}