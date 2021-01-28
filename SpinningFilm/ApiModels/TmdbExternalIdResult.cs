using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.ApiModels
{
    public class TmdbExternalIdResult
    {
        [JsonProperty(PropertyName = "imdb_id")]
        public string ImdbId { get; set; }
    }
}
