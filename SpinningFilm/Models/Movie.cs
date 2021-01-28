using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using SpinningFilm.ApiModels;
using SpinningFilm.Helpers;
using SpinningFilm.Interfaces;

namespace SpinningFilm.Models
{
    [Table("Movie")]
    public class Movie : IMedia
    {
        [Key]
        [Required]
        public string ImdbId { get; set; }
        public string TmdbId { get; set; }
        public string MediaUserId { get; set; }
        public string Title { get; set; }
        [Required]
        public string Type { get; set; }
        public string Poster { get; set; }
        [Required]        
        public int DiscTypeId { get; set; }
        public decimal ImdbRating { get; set; }
        public string Rated { get; set; }
        public string Metascore { get; set; }
        public bool DigitalCopy { get; set; }
        public string PlotShort { get; set; }
        public string PlotLong { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Runtime { get; set; }
        public bool Watched { get; set; }
        [NotMapped]
        public string Genre { get; set; }
        [NotMapped]
        public DiscType DiscType { get; set; }
        [NotMapped]
        public List<Watched> WatchedList { get; set; } = new List<Watched>();

        [NotMapped]
        public string PlotTruncated
        {
            get
            {
                string plotTruncated = "";
                if(PlotShort.Length > SpinningFilmHelper.MaxPlotLength)
                {
                    string[] plotSplit = PlotShort.Split(' ');
                    int i = 0;
                    while(plotTruncated.Length < SpinningFilmHelper.MaxPlotLength)
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

        [NotMapped]
        public string LastWatched
        {
            get
            {
                if (WatchedList != null && WatchedList.Count > 0)
                {
                    return WatchedList.Max(w => w.Date).ToString("MM/dd/yyyy");
                }
                else
                {
                    return "";
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


        public Movie() { }

        public Movie(MediaResult media, RatingResult rating)
        {
            ImdbId = media.ImdbId;
            TmdbId = media.TmdbId;
            Title = media.Title;
            Type = media.Type;
            Poster = media.Poster;
            DiscTypeId = media.DiscType;
            DigitalCopy = (bool)media.DigitalCopy;
            PlotShort = rating.Plot;
            ImdbRating = rating.ImdbRating;
            Metascore = rating.Metascore;
            Rated = rating.Rated;
            ReleaseDate = media.ReleaseDate;
        }

        public Movie(string mediaUserId, TmdbMovieResult media, RatingResult rating, string type, int discType, bool digitalCopy)
        {
            ImdbId = media.ImdbId;
            TmdbId = media.TmdbId;
            MediaUserId = mediaUserId;
            Title = media.Title;
            Type = type;
            Poster = media.Poster;
            DiscTypeId = discType;
            DigitalCopy = digitalCopy;
            PlotShort = rating.Plot;
            PlotLong = media.Plot;
            ImdbRating = rating.ImdbRating;
            Metascore = rating.Metascore;
            Rated = rating.Rated;
            ReleaseDate = media.ReleaseDate;
            Runtime = media.Runtime;
        }
    }
}
