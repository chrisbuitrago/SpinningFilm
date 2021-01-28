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
        private IMediaRepository<Movie> _movieRepository;
        private IMediaRepository<DiscType> _discTypeRepository;
        private IMediaRepository<Watched> _watchedRepository;
        private IMediaRepository<Genre> _genreRepository;
        private IMediaRepository<MediaGenre> _mediaGenreRepository;
        public MediaListViewModelService(IMediaRepository<Movie> movieRepository,
            IMediaRepository<DiscType> discTypeRepository,
            IMediaRepository<Watched> watchedRepository,
            IMediaRepository<Genre> genreRepository,
            IMediaRepository<MediaGenre> mediaGenreRepository)
        {
              _movieRepository = movieRepository;
              _discTypeRepository = discTypeRepository;
              _watchedRepository = watchedRepository;
              _genreRepository = genreRepository;
              _mediaGenreRepository = mediaGenreRepository;
        }

        public async Task<MediaListViewModel> CreateViewModel(MediaFilter filter)
        {
            var media = await _movieRepository.ListAsync(new MovieSpecification(filter));
            var discTypes = await _discTypeRepository.ListAllAsync();
            var watched = await _watchedRepository.ListAllAsync();
            var genres = await _genreRepository.ListAsync(new GenreSpecification(filter.MediaType));

            var viewModel = new MediaListViewModel
            {
                MediaType = filter.MediaType,
                Filter = filter,
                Genres = genres.ToList(),
                DiscTypes = discTypes.ToList(),
                Media = media.ToList()
            };

            foreach (var item in viewModel.Media)
            {
                item.DiscType = discTypes.SingleOrDefault(d => d.DiscTypeId == item.DiscTypeId);
                item.WatchedList = watched.Where(w => w.ImdbId == item.ImdbId).ToList();
            }

            if (filter.FilteredGenreId != null && filter.FilteredGenreId.Count > 0)
            {
                var mediaGenres = await _mediaGenreRepository.ListAsync(new MediaGenreSpecification(filter.MediaType));

                viewModel.Media = viewModel.Media.Where(m => (mediaGenres.Where(mg => filter.FilteredGenreId.Contains(mg.GenreId)).Select(mg => mg.ImdbId))
                                                             .Contains(m.ImdbId)).ToList();
            }

            return viewModel;
        }
    }
}
