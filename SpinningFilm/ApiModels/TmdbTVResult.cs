using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpinningFilm.Models;

namespace SpinningFilm.ApiModels
{
    public class TmdbTVResult
    {
        [JsonProperty(PropertyName = "id")]
        public string TmdbId { get; set; }
        [JsonProperty(PropertyName = "genres")]
        public List<Genre> Genres { get; set; }
        public string Name { get; set; }
        [JsonProperty(PropertyName = "overview")]
        public string Plot { get; set; }
        [JsonProperty(PropertyName = "first_air_date")]
        public DateTime FirstAired { get; set; }
        [JsonProperty(PropertyName = "last_air_date")]
        public DateTime LastAired { get; set; }
        [JsonProperty(PropertyName = "poster_path")]
        public string Poster { get; set; }
        [JsonProperty(PropertyName = "number_of_seasons")]
        public int NumberOfSeasons { get; set; }
        [JsonProperty(PropertyName = "external_ids")]
        public TmdbExternalIdResult ExternalIds { get; set; }
        public TmdbCreditsResult Credits { get; set; }
    }
}
