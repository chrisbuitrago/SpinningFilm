using SpinningFilm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.Specifications
{
    public class MediaExtraGenreSpecification : BaseSpecification<MediaExtraGenre>
    {
        public MediaExtraGenreSpecification(Guid mediaId, Guid physicalMediaId)
            : base(m => m.MediaId == mediaId && m.PhysicalMediaId == physicalMediaId)
        {

        }
    }
}
