using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.Models
{
    [Table("PhysicalSeason")]
    public class PhysicalSeason
    {
        [Key]
        public Guid PhysicalSeasonId { get; set; }
        public Guid PhysicalMediaId { get; set; }
        public int SeasonNumber { get; set; }

        public PhysicalSeason()
        {
            PhysicalSeasonId = Guid.NewGuid();
        }

        public PhysicalSeason(Guid physicalMediaId, int seasonNumber)
        {
            PhysicalSeasonId = Guid.NewGuid();
            PhysicalMediaId = physicalMediaId;
            SeasonNumber = seasonNumber;
        }
    }
}
