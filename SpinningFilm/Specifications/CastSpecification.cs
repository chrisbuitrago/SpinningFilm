using SpinningFilm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.Specifications
{
    public class CastSpecification : BaseSpecification<Cast>
    {
        public CastSpecification(Guid mediaId)
            : base(c => c.MediaId == mediaId)
        {

        }

        public CastSpecification(Guid mediaId, int order)
            : base(c => c.MediaId == mediaId && c.Order < order)
        {
            ApplyOrderBy(r => r.Order);
        }
    }
}
