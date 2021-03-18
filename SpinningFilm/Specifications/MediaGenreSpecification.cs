using SpinningFilm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.Specifications
{
    public class MediaGenreSpecification : BaseSpecification<MediaGenre>
    {
        public MediaGenreSpecification(List<int> filteredGenreIds)
            : base(m => filteredGenreIds.Contains(m.GenreId))
        {

        }
    }
}
