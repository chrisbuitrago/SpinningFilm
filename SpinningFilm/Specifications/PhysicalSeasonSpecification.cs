using SpinningFilm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.Specifications
{
    public class PhysicalSeasonSpecification : BaseSpecification<PhysicalSeason>
    {
        public PhysicalSeasonSpecification(Guid physicalMediaId)
            : base(p => p.PhysicalMediaId == physicalMediaId)
        {

        }
    }
}
