using SpinningFilm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.Specifications
{
    public class PhysicalMediaSpecification : BaseSpecification<PhysicalMedia>
    {
        //public PhysicalMediaSpecification(Guid appUserId, Guid physicalMediaId)
        //    : base(p => p.AppUserId == appUserId && p.PhysicalMediaId == physicalMediaId)
        //{
        //}
        public PhysicalMediaSpecification(Guid appUserId, Guid mediaId)
            : base(p => p.AppUserId == appUserId && p.MediaId == mediaId)
        {
        }

    }
}
