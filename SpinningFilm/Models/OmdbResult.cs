using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpinningFilm.Models;
using Newtonsoft.Json;

namespace SpinningFilm.Models
{
    public class OmdbResult
    {
        [JsonProperty(PropertyName = "search")]
        public List<MediaResult> Results { get; set; }
        public int TotalResults { get; set; }
        public bool Response { get; set; }
    }
}
