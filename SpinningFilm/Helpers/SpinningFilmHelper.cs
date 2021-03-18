using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.Helpers
{
    public class SpinningFilmHelper
    {
        public static int MaxPlotLength { get; } = 150;
        public static string SeriesType { get; } = "tv";
        public static string MovieType { get; } = "movie";
        public static int MaxCastOrder { get; } = 3;
        public static string Director { get; } = "Director";
    }
}
