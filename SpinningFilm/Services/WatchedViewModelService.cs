using SpinningFilm.Interfaces;
using SpinningFilm.Models;
using SpinningFilm.Specifications;
using SpinningFilm.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.Services
{
    public class WatchedViewModelService : IWatchedViewModelService
    {
        private readonly IMediaRepository<PhysicalMedia> _physicalMediaRepository;
        private readonly IMediaRepository<Watched> _watchedRepository;

        public WatchedViewModelService(IMediaRepository<PhysicalMedia> physicalMediaRepository,
            IMediaRepository<Watched> watchedRepository)
        {
            _physicalMediaRepository = physicalMediaRepository;
            _watchedRepository = watchedRepository;
        }
        public async Task<WatchedViewModel> AddWatched(PhysicalMedia physicalMedia, DateTime dateWatched)
        {
            physicalMedia.Watched = true;
            await _physicalMediaRepository.UpdateAsync(physicalMedia);

            Watched watched = new Watched
            {
                WatchedId = Guid.NewGuid(),
                PhysicalMediaId = physicalMedia.PhysicalMediaId,
                Date = dateWatched
            };

            await _watchedRepository.AddAsync(watched);

            var watchedList = await _watchedRepository.ListAsync(new WatchedSpecification(physicalMedia.PhysicalMediaId));

            return new WatchedViewModel { Count = watchedList.Count, LastWatched = watchedList.Max(w => w.Date).ToString("MM/dd/yyyy") };
        }
    }
}
