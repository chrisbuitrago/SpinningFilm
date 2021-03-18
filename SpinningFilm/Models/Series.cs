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
    public class Series
    {
        [Key]
        public Guid SeriesId { get; set; }
        public Guid MediaId { get; set; }
        public int NumberOfSeasons { get; set; }
        public DateTime FirstAired { get; set; }
        public DateTime LastAired { get; set; }

        public Series()
        {

        }

        public Series(TmdbTVResult media, Guid mediaId)
        {
            SeriesId = Guid.NewGuid();
            MediaId = mediaId;
            NumberOfSeasons = media.NumberOfSeasons;
            FirstAired = media.FirstAired;
            LastAired = media.LastAired;
        }
    }
}
