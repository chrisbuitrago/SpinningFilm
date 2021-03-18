using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpinningFilm.Models;

namespace SpinningFilm.ViewModels
{
    public class MediaListViewModel
    {
        public string MediaType { get; set; }
        public List<PhysicalMediaMovie> PhysicalMedia { get; set; }
        public List<Genre> Genres { get; set; }
        public List<DiscType> DiscTypes { get; set; }
        public MediaFilter Filter { get; set; }
        public MediaListViewModel() { }
    }
}
