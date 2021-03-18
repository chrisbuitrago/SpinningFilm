using SpinningFilm.Models;
using SpinningFilm.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.Interfaces
{
    public interface IMediaInformationViewModelService
    {
        Task<MediaInformationViewModel> CreateViewModel(Guid mediaId, Guid appUserId);
        Task<MediaInformationViewModel> CreateViewModelFromPhysicalMedia(PhysicalMedia physicalMedia);
    }
}
