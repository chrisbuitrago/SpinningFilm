using SpinningFilm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.Specifications
{
    public class ExtraGenreSpecification : BaseSpecification<ExtraGenre>
    {
        public ExtraGenreSpecification(List<int> filteredGenreIds)
            : base(e => filteredGenreIds.Contains(e.GenreId))
        {

        }

        public ExtraGenreSpecification(Guid physicalMediaId)
            : base(e => e.PhysicalMediaId == physicalMediaId)
        {

        }
    }
}
