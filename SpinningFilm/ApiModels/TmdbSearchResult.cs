using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using SpinningFilm.Models;

namespace SpinningFilm.ApiModels
{
    public class TmdbSearchResult
    {
        [JsonProperty(PropertyName = "poster_path")]
        public string PosterPath { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string ApiTitle { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string ApiName { get; set; }
        [JsonProperty(PropertyName = "release_date")]
        public string ReleaseDate { get; set; }
        [JsonProperty(PropertyName = "first_air_date")]
        public string FirstAired { get; set; }
        [JsonProperty(PropertyName = "media_type")]
        public string Type { get; set; }
        public string Id { get; set; }
        public string Overview { get; set; }
        public decimal Popularity { get; set; }
        public int Runtime { get; set; }
        [JsonProperty(PropertyName = "genre_ids")]
        public List<int> Genres { get; set; }
        public List<DiscType> DiscTypes { get; set; }

        public string Year 
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(ReleaseDate))
                {
                    return ReleaseDate.Split('-')[0];
                }
                else if (!string.IsNullOrWhiteSpace(FirstAired))
                {
                    return FirstAired.Split('-')[0];
                }
                else
                {
                    return "";
                }
            }
        }

        public string Title
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(ApiTitle))
                {
                    return ApiTitle;
                }
                else if (!string.IsNullOrWhiteSpace(ApiName))
                {
                    return ApiName;
                }
                else
                {
                    return "";
                }
            }
        }

        public string Poster
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(PosterPath))
                {
                    return "https://image.tmdb.org/t/p/w185_and_h278_bestv2" + PosterPath;
                }
                else
                {
                    return "";
                }
            }
        }

        public List<SelectListItem> DiscTypeList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in DiscTypes)
            {
                list.Add(new SelectListItem { Text = item.Description, Value = item.DiscTypeId.ToString() });
            }

            return list;
        }
    }
}
