using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.Interfaces
{
    public interface IMediaService
    {
        string ImdbId { get; set; }
        string TmdbId { get; set; }
        string Title { get; set; }
        int Year { get; set; }
        string Type { get; set; }
        string Poster { get; set; }
    }
}
