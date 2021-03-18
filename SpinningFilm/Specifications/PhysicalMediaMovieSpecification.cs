using SpinningFilm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.Specifications
{
    public class PhysicalMediaMovieSpecification : BaseSpecification<PhysicalMediaMovie>
    {
        public PhysicalMediaMovieSpecification(Guid userId)
            : base(p => p.AppUserId == userId)
        {
            ApplyOrderBy(p => p.Title);
        }

        public PhysicalMediaMovieSpecification(MediaFilter filter)
            : base(r => r.AppUserId == filter.UserId
                && (filter.FilteredDiscTypeId.Count == 0 || filter.FilteredDiscTypeId.Contains(r.DiscTypeId))
                && (string.IsNullOrEmpty(filter.SearchTerm) || r.Title.ToLower().Contains(filter.SearchTerm.ToLower()))
                && ((!filter.DigitalCopy && !filter.NoDigitalCopy) || (filter.DigitalCopy && filter.NoDigitalCopy) || r.DigitalCopy == filter.DigitalCopy)
                && ((!filter.Watched && !filter.NotWatched) || (filter.Watched && filter.NotWatched) || r.Watched == filter.Watched))
        {
            if (filter.OrderBy == 1)
            {
                ApplyOrderBy(r => r.ReleaseDate);
                AddThenBy(r => r.Title);
            }
            else
            {
                ApplyOrderBy(r => r.Title);
                AddThenBy(r => r.ReleaseDate);
            }
        }
    }
}
