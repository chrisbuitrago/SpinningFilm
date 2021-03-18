using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.Models
{
    [Table("PhysicalMedia")]

    public class PhysicalMedia
    {
        [Key]
        public Guid PhysicalMediaId { get; set; }
        public Guid MediaId { get; set; }
        public Guid AppUserId { get; set; }
        public bool DigitalCopy { get; set; }
        public int DiscTypeId { get; set; }
        public bool Watched { get; set; }

        public PhysicalMedia()
        {

        }

        public PhysicalMedia(Guid mediaId, Guid userId)
        {
            PhysicalMediaId = Guid.NewGuid();
            MediaId = mediaId;
            AppUserId = userId;
        }

        public PhysicalMedia(Guid mediaId, Guid userId, bool digitalCopy, int discTypeId)
        {
            PhysicalMediaId = Guid.NewGuid();
            MediaId = mediaId;
            AppUserId = userId;
            DigitalCopy = digitalCopy;
            DiscTypeId = discTypeId;
        }
    }
}
