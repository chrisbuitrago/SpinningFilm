using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.Helpers
{
    public static class TmdbSettings
    {
        public static string PosterSmall { get; } = "http://image.tmdb.org/t/p/w185/";
        public static string PosterMedium { get; } = "http://image.tmdb.org/t/p/w342/";
        public static string PosterLarge { get; } = "http://image.tmdb.org/t/p/780/";
        public static string PosterOriginal { get;} = "http://image.tmdb.org/t/p/original/";
        public static string SeriesType { get; } = "tv";
        public static string MovieType { get; } = "movie";
    }
}
