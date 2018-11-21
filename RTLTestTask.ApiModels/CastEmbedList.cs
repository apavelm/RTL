using System.Collections.Generic;
using Newtonsoft.Json;

namespace RTLTestTask.ApiModels
{
    public class CastEmbedList
    {
        [JsonProperty("cast")] public virtual List<Cast> Cast { get; set; }
    }
}
