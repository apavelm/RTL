using System;
using Newtonsoft.Json;

namespace RTLTestTask.ApiModels
{
    public class CastPerson
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("birthday")] public DateTime? DoB { get; set; }
    }
}