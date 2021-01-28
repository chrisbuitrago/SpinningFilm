using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.Models
{
    public class MediaFilter
    {
        public string UserId { get; set; }
        public string MediaType { get; set; }
        public List<int> FilteredGenreId { get; set; } = new List<int>();
        public List<int> FilteredDiscTypeId { get; set; } = new List<int>();
        public string SearchTerm { get; set; }
        public bool Watched { get; set; }
        public bool NotWatched { get; set; }
        public bool DigitalCopy { get; set; }
        public bool NoDigitalCopy { get; set; }
        public int OrderBy { get; set; }
        public MediaFilter() { }
    }
}
