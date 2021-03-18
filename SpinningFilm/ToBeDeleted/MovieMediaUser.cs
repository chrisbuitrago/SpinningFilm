using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.Models
{
    public class MovieMediaUser
    {
        public Guid MovieMediaUserId { get; set; }
        public Guid MediaUserId { get; set; }
        public Guid MovieId { get; set; }
        public int DiscTypeId { get; set; }
        public bool DigitalCopy { get; set; }
        public bool Watched { get; set; }
    }
}
