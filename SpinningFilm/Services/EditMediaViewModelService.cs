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
    public class EditMediaViewModelService : IEditMediaViewModelService
    {
        private readonly IMediaRepository<Media> _mediaRepository;
        private readonly IMediaRepository<DiscType> _discTypeRepository;
        private readonly IMediaRepository<ExtraGenre> _extraGenreRepository;
        private readonly IMediaRepository<Genre> _genreRepository;

        public EditMediaViewModelService(IMediaRepository<Media> mediaRepository,
            IMediaRepository<DiscType> discTypeRepository,
            IMediaRepository<ExtraGenre> extraGenreRepository,
            IMediaRepository<Genre> genreRepository)
        {
            _mediaRepository = mediaRepository;
            _discTypeRepository = discTypeRepository;
            _extraGenreRepository = extraGenreRepository;
            _genreRepository = genreRepository;
        }

        public async Task<EditMediaViewModel> CreateViewModel(PhysicalMedia physicalMedia)
        {
            Media media = await _mediaRepository.GetByIdAsync(physicalMedia.MediaId);
            var discTypes = await _discTypeRepository.ListAllAsync();
            var extraGenres = await _extraGenreRepository.ListAsync(new ExtraGenreSpecification(physicalMedia.PhysicalMediaId));
            var genres = await _genreRepository.ListAsync(new GenreSpecification(true));//_context.Genres.Where(g => !g.Default).ToList();

            EditMediaViewModel editMedia = new EditMediaViewModel(media, physicalMedia, discTypes, genres, extraGenres);

            return editMedia;
        }
    }
}
