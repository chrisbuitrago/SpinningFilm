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
    public class MediaListViewModelService : IMediaListViewModelService
    {
        private readonly IMediaRepository<Movie> _movieRepository;
        private readonly IMediaRepository<DiscType> _discTypeRepository;
        private readonly IMediaRepository<Watched> _watchedRepository;
        private readonly IMediaRepository<Genre> _genreRepository;
        private readonly IMediaRepository<MediaGenre> _mediaGenreRepository;
        private readonly IMediaRepository<ExtraGenre> _extraGenreRepository;
        private readonly IMediaRepository<PhysicalMediaMovie> _physicalMediaRepository;
        public MediaListViewModelService(IMediaRepository<Movie> movieRepository,
            IMediaRepository<DiscType> discTypeRepository,
            IMediaRepository<Watched> watchedRepository,
            IMediaRepository<Genre> genreRepository,
            IMediaRepository<MediaGenre> mediaGenreRepository,
            IMediaRepository<ExtraGenre> extraGenreRepository,
            IMediaRepository<PhysicalMediaMovie> physicalMediaRepository)
        {
            _movieRepository = movieRepository;
            _discTypeRepository = discTypeRepository;
            _watchedRepository = watchedRepository;
            _genreRepository = genreRepository;
            _mediaGenreRepository = mediaGenreRepository;
            _physicalMediaRepository = physicalMediaRepository;
            _extraGenreRepository = extraGenreRepository;
        }

        public async Task<MediaListViewModel> CreateViewModel(MediaFilter filter)
        {
            var media = await _physicalMediaRepository.ListAsync(new PhysicalMediaMovieSpecification(filter));
            var discTypes = await _discTypeRepository.ListAllAsync();
            var watched = await _watchedRepository.ListAllAsync();
            var genres = await _genreRepository.ListAllAsync();

            var viewModel = new MediaListViewModel
            {
                MediaType = filter.MediaType,
                Filter = filter,
                Genres = genres.ToList(),
                DiscTypes = discTypes.ToList(),
                PhysicalMedia = media.ToList()
            };

            //foreach (var item in viewModel.Media)
            //{
            //    item.DiscType = discTypes.SingleOrDefault(d => d.DiscTypeId == item.DiscTypeId);
            //    item.WatchedList = watched.Where(w => w.ImdbId == item.ImdbId).ToList();
            //}

            if (filter.FilteredGenreId != null && filter.FilteredGenreId.Count > 0)
            {
                var mediaGenres = await _mediaGenreRepository.ListAsync(new MediaGenreSpecification(filter.FilteredGenreId));
                var extraGenres = await _extraGenreRepository.ListAsync(new ExtraGenreSpecification(filter.FilteredGenreId));

                viewModel.PhysicalMedia = viewModel.PhysicalMedia.Where(m => (mediaGenres.Select(mg => mg.MediaId)).Contains(m.MediaId) ||
                                                                             (extraGenres.Select(mg => mg.PhysicalMediaId)).Contains(m.PhysicalMediaId)).ToList();
            }

            return viewModel;
        }
    }
}
