using SpinningFilm.ApiModels;
using SpinningFilm.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.Models
{
    [Table("Media")]
    public class Media
    {        
        [Key]
        [Required]
        public Guid MediaId { get; set; }
        [Required]
        public Guid MediaTypeId { get; set; }
        [Required]
        public string ImdbId { get; set; }
        [Required]
        public string TmdbId { get; set; }
        public string Title { get; set; }
        public string Poster { get; set; }
        public decimal ImdbRating { get; set; }
        public string Rated { get; set; }
        public string Metascore { get; set; }
        public string PlotShort { get; set; }
        public string PlotLong { get; set; }

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

        public Media() 
        {
            MediaId = Guid.NewGuid();
        }

        public Media(TmdbTVResult media, MediaType mediaType, RatingResult rating, string imdbId)
        {
            MediaId = Guid.NewGuid();
            MediaTypeId = mediaType.MediaTypeId;
            ImdbId = imdbId;
            TmdbId = media.TmdbId;
            Title = media.Name;
            Poster = media.Poster;
            ImdbRating = rating.ImdbRating;
            Rated = rating.Rated;
            Metascore = rating.Metascore;
            PlotShort = rating.Plot;
            PlotLong = media.Plot;
        }

        public Media(TmdbMovieResult media, MediaType mediaType, RatingResult rating, string imdbId)
        {
            MediaId = Guid.NewGuid();
            MediaTypeId = mediaType.MediaTypeId;
            ImdbId = imdbId;
            TmdbId = media.TmdbId;
            Title = media.Title;
            Poster = media.Poster;
            ImdbRating = rating.ImdbRating;
            Rated = rating.Rated;
            Metascore = rating.Metascore;
            PlotShort = rating.Plot;
            PlotLong = media.Plot;
        }
    }
}
