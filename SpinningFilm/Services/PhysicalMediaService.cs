using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SpinningFilm.Extensions;
using SpinningFilm.Interfaces;
using SpinningFilm.Models;
using SpinningFilm.Specifications;
using SpinningFilm.ViewModels;

namespace SpinningFilm.Services
{
    public class PhysicalMediaService : IPhysicalMediaService
    {
        private readonly IMediaRepository<PhysicalMedia> _physicalMediaRepository;
        private readonly IMediaRepository<ExtraGenre> _extraGenresRepository;
        private readonly IMediaRepository<Watched> _watchedRepository;
        private readonly IMediaRepository<PhysicalSeason> _physicalSeasonRepository;

        public PhysicalMediaService(IMediaRepository<PhysicalMedia> physicalMediaRepository,
            IMediaRepository<ExtraGenre> extraGenresRepository,
            IMediaRepository<Watched> watchedRepository,
            IMediaRepository<PhysicalSeason> physicalSeasonRepository)
        {
            _physicalMediaRepository = physicalMediaRepository;
            _extraGenresRepository = extraGenresRepository;
            _watchedRepository = watchedRepository;
            _physicalSeasonRepository = physicalSeasonRepository;
        }

        public async Task<PhysicalMedia> Get(Guid physicalMediaId)
        {
            var physicalMedia = await _physicalMediaRepository.GetByIdAsync(physicalMediaId);

            return physicalMedia;
        }

        public async Task Update(PhysicalMedia physicalMedia)
        {
            await _physicalMediaRepository.UpdateAsync(physicalMedia);
        }

        public async Task Delete(PhysicalMedia physicalMedia)
        {
            await _physicalMediaRepository.DeleteAsync(physicalMedia);

            var extraGenres = await _extraGenresRepository.ListAsync(new ExtraGenreSpecification(physicalMedia.PhysicalMediaId));
            await _extraGenresRepository.DeleteRangeAsync(extraGenres);

            var watched = await _watchedRepository.ListAsync(new WatchedSpecification(physicalMedia.PhysicalMediaId));
            await _watchedRepository.DeleteRangeAsync(watched);

            var physicalSeason = await _physicalSeasonRepository.ListAsync(new PhysicalSeasonSpecification(physicalMedia.PhysicalMediaId));
            await _physicalSeasonRepository.DeleteRangeAsync(physicalSeason);
        }
    }
}
