using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.Models
{
    [Table("SeriesSeason")]
    public class SeriesSeason
    {
        [Key]
        public Guid SeriesSeasonId { get; set; }
        public string ImdbId { get; set; }
        public int SeasonNumber { get; set; }
        public int DiscTypeId { get; set; }
        public bool DigitalCopy { get; set; }
        public bool Watched { get; set; }

        public SeriesSeason()
        {
            SeriesSeasonId = Guid.NewGuid();
        }

        public SeriesSeason(string imdbId, int seasonNumber)
        {
            SeriesSeasonId = Guid.NewGuid();
            ImdbId = imdbId;
            SeasonNumber = seasonNumber;
        }
    }
}
