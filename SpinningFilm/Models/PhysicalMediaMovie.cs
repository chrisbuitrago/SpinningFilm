using SpinningFilm.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.Models
{
    public class PhysicalMediaMovie
    {
        public Guid MediaId { get; set; }
        public Guid AppUserId { get; set; }
        public Guid PhysicalMediaId { get; set; }
        public string ImdbId { get; set; }
        public decimal ImdbRating { get; set; }
        public string Metascore { get; set; }
        public string Rated { get; set; }
        public string Title { get; set; }
        public string Poster { get; set; }
        public string PlotShort { get; set; }
        public bool DigitalCopy { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int DiscTypeId { get; set; }
        public string DiscDescription { get; set; }
        public bool Watched { get; set; }
        public DateTime? LastWatched { get; set; }
        public int? WatchedCount { get; set; }

        [NotMapped]
        public string DiscTypeLogoLink
        {
            get
            {
                if (DiscTypeId == 1)
                {
                    return "/img/blu-ray.svg";
                }
                else if (DiscTypeId == 2)
                {
                    return "/img/4k-uhd2.svg";
                }
                else if (DiscTypeId == 3)
                {
                    return "/img/dvd.svg";
                }
                else
                {
                    return "";
                }
            }
        }

        [NotMapped]
        public string PlotTruncated
        {
            get
            {
                string plotTruncated = "";
                if (PlotShort.Length > SpinningFilmHelper.MaxPlotLength)
                {
                    string[] plotSplit = PlotShort.Split(' ');
                    int i = 0;
                    while (plotTruncated.Length < SpinningFilmHelper.MaxPlotLength)
                    {
                        plotTruncated += plotSplit[i] + " ";
                        i++;
                    }
                    plotTruncated = plotTruncated.Trim() + "...";

                    return plotTruncated;
                }
                else
                {
                    return PlotShort;
                }
            }
        }

        public string ImdbLink
        {
            get
            {
                return string.Format("https://www.imdb.com/title/{0}", ImdbId);
            }
        }
        public string PosterSmall
        {
            get
            {
                return TmdbSettings.PosterSmall + Poster;
            }
        }
        public string PosterMedium
        {
            get
            {
                return TmdbSettings.PosterMedium + Poster;
            }
        }
        public string PosterLarge
        {
            get
            {
                return TmdbSettings.PosterLarge + Poster;
            }
        }
        public string PosterOriginal
        {
            get
            {
                return TmdbSettings.PosterOriginal + Poster;
            }
        }
    }
}

//me.ImdbId,
//	me.ImdbRating,
//	me.Metascore,
//	me.Title,
//	pm.DigitalCopy,
//	mo.ReleaseDate,
//	dt.[Description]