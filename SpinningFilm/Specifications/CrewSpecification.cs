using SpinningFilm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.Specifications
{
    public class CrewSpecification : BaseSpecification<Crew>
    {
        public CrewSpecification(Guid mediaId)
            : base(c => c.MediaId == mediaId)
        {

        }

        public CrewSpecification(Guid mediaId, string job)
            : base(c => c.MediaId == mediaId && c.Job == job)
        {

        }
    }
}
