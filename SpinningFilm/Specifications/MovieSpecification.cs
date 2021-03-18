using SpinningFilm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.Specifications
{
    public class MovieSpecification : BaseSpecification<Movie>
    {
        public MovieSpecification(string userId)
            : base(r => r.MediaId == Guid.Parse(userId))
        {
        }

        public MovieSpecification(Guid mediaId)
            : base(r => r.MediaId == mediaId)
        {
        }

        //public MovieSpecification(MediaFilter filter)
        //    : base(r => r.MediaUserId == filter.UserId
        //        && (filter.FilteredDiscTypeId.Count == 0 || filter.FilteredDiscTypeId.Contains(r.DiscTypeId))
        //        && (string.IsNullOrEmpty(filter.SearchTerm) || r.Title.ToLower().Contains(filter.SearchTerm.ToLower()))
        //        && ((!filter.DigitalCopy && !filter.NoDigitalCopy) || (filter.DigitalCopy && filter.NoDigitalCopy) || r.DigitalCopy == filter.DigitalCopy)
        //        && ((!filter.Watched && !filter.NotWatched) || (filter.Watched && filter.NotWatched) || r.Watched == filter.Watched))
        //{
        //    if (filter.OrderBy == 1)
        //    {
        //        ApplyOrderBy(r => r.ReleaseDate);
        //        AddThenBy(r => r.Title);
        //    }
        //    else
        //    {
        //        ApplyOrderBy(r => r.Title);
        //        AddThenBy(r => r.ReleaseDate);
        //    }
        //}
    }
}
