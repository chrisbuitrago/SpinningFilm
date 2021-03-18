using SpinningFilm.Models;
using SpinningFilm.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.Interfaces
{
    public interface IEditMediaViewModelService
    {
        Task<EditMediaViewModel> CreateViewModel(PhysicalMedia physicalMedia);
    }
}
