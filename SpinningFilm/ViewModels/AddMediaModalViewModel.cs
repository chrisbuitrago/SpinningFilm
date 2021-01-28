using Microsoft.AspNetCore.Mvc.Rendering;
using SpinningFilm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.ViewModels
{
    public class AddMediaModalViewModel
    {
        public string TmdbId { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public string ImdbId { get; set; }
        public string Type { get; set; }
        public string Poster { get; set; }
        public int DiscType { get; set; }
        public bool? DigitalCopy { get; set; }
        public string PlotLong { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime FirstAired { get; set; }
        public int Runtime { get; set; }
        public List<int> Genre { get; set; } = new List<int>();
        public List<Genre> Genres { get; set; }
        public List<string> ExtraGenres { get; set; } = new List<string>();
        public List<DiscType> DiscTypes { get; set; } = new List<DiscType>();
        public int NumberOfSeaons { get; set; }
        public int[] Season { get; set; }
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
