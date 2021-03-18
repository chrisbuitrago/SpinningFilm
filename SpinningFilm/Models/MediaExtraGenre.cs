using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.Models
{
    public class MediaExtraGenre
    {
        public Guid MediaId { get; set; }
        public Guid PhysicalMediaId { get; set; }
        public string Name { get; set; }
    }
}
