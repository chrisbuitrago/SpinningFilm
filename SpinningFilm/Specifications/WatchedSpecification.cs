using SpinningFilm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.Specifications
{
    public class WatchedSpecification : BaseSpecification<Watched>
    {
        public WatchedSpecification(Guid physicalMediaId)
            : base(w => w.PhysicalMediaId == physicalMediaId)
        {

        }
    }
}
