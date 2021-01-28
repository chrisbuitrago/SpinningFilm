using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.ApiModels
{
    public class TmdbResult
    {
        public int Page { get; set; }
        public List<TmdbSearchResult> Results { get; set; }
        [JsonProperty(PropertyName = "total_results")]
        public int TotalResults { get; set; }
        [JsonProperty(PropertyName = "total_pages")]
        public bool TotalPages { get; set; }
    }
}
