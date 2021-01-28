using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.ApiModels
{
    public class RatingResult
    {
        public decimal ImdbRating { get; set; }
        public string Rated { get; set; }
        public string Metascore { get; set; }
        public string Plot { get; set; }
    }
}
