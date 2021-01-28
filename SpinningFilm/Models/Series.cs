using SpinningFilm.ApiModels;
using SpinningFilm.Helpers;
using SpinningFilm.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.Models
{
    [Table("Series")]
    public class Series : IMedia
    {
        [Key]
        public string ImdbId { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public int NumberOfSeasons { get; set; }
        public string Poster { get; set; }
        public decimal ImdbRating { get; set; }
        public string Rated { get; set; }
        public string Metascore { get; set; }
        public string PlotShort { get; set; }
        public string TmdbId { get; set; }
        public string PlotLong { get; set; }
        public DateTime FirstAired { get; set; }
        public DateTime LastAired { get; set; }
        [StringLength(36)]
        public string MediaUserId { get; set; }

        public Series()
        {

        }

        public Series(string mediaUserId, string imdbId, TmdbTVResult media, RatingResult rating)
        {
            ImdbId = imdbId;
            Title = media.Name;
            Type = TmdbSettings.SeriesType;
            NumberOfSeasons = media.NumberOfSeasons;
            Poster = media.Poster;
            ImdbRating = rating.ImdbRating;
            Rated = rating.Rated;
            Metascore = rating.Metascore;
            PlotShort = rating.Plot;
            TmdbId = media.TmdbId;
            PlotLong = media.Plot;
            FirstAired = media.FirstAired;
            LastAired = media.LastAired;
            MediaUserId = mediaUserId;
        }
    }
}
