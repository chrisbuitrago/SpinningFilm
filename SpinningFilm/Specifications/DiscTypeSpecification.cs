using SpinningFilm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.Specifications
{
    public class DiscTypeSpecification : BaseSpecification<DiscType>
    {
        public DiscTypeSpecification(int id)
            : base(r => r.DiscTypeId == id)
        {
            ApplyOrderBy(r => r.Description);
        }
    }
}
