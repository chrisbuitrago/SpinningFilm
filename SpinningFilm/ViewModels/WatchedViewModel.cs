using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.ViewModels
{
    public class WatchedViewModel
    {
        public Guid WatchedId { get; set; }
        public string ImdbId { get; set; }
        public DateTime Date { get; set; }
        public string MediaType { get; set; }
        public string Title { get; set; }
        public int Count { get; set; }
        public string LastWatched { get; set; }
    }
}
