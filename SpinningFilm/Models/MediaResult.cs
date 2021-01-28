using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.Models
{
    public class MediaResult
    {        
        public string TmdbId { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ImdbId { get; set; }
        public string Type { get; set; }
        public string Poster { get; set; }
        public string Plot { get; set; }
        public int DiscType { get; set; }
        public bool? DigitalCopy { get; set; }
        public List<int> Genre { get; set; } = new List<int>();
        public List<string> ExtraGenres { get; set; } = new List<string>();
        public List<DiscType> DiscTypes { get; set; }
        public string ImdbLink
        {
            get
            {
                return string.Format("https://www.imdb.com/title/{0}", ImdbId);
            }
        }

        public List<SelectListItem> DiscTypeList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach(var item in DiscTypes)
            {
                list.Add(new SelectListItem { Text = item.Description, Value = item.DiscTypeId.ToString() });
            }

            return list;
        }
    }
}
