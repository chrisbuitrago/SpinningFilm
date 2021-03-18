using SpinningFilm.Models;
using SpinningFilm.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.Interfaces
{
    public interface IPhysicalMediaService
    {
        Task<PhysicalMedia> Get(Guid physicalMediaId);
        Task Update(PhysicalMedia physicalMedia);
        Task Delete(PhysicalMedia physicalMedia);
    }
}
