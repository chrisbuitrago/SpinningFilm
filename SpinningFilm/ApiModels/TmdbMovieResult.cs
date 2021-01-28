using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpinningFilm.Models;

namespace SpinningFilm.ApiModels
{
    public class TmdbMovieResult
    {
        [JsonProperty(PropertyName = "id")]
        public string TmdbId { get; set; }
        [JsonProperty(PropertyName = "genres")]
        public List<Genre> Genres { get; set; }
        [JsonProperty(PropertyName = "imdb_id")]
        public string ImdbId { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        [JsonProperty(PropertyName = "poster_path")]
        public string Poster { get; set; }
        public decimal ImdbRating { get; set; }
        public string Rated { get; set; }
        public string Metascore { get; set; }
        [JsonProperty(PropertyName = "release_date")]
        public DateTime ReleaseDate { get; set; }
        [JsonProperty(PropertyName = "overview")]
        public string Plot { get; set; }
        public int Runtime { get; set; }
        [JsonProperty(PropertyName = "external_ids")]
        public TmdbExternalIdResult ExternalIds { get; set; }

        public TmdbCreditsResult Credits { get; set; }
    }
}
