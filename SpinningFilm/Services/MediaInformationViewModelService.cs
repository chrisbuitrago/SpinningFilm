using SpinningFilm.Interfaces;
using SpinningFilm.Models;
using SpinningFilm.Specifications;
using SpinningFilm.ViewModels;
using SpinningFilm.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.Services
{
    public class MediaInformationViewModelService : IMediaInformationViewModelService
    {
        private readonly IMediaRepository<Media> _mediaRepository;
        private readonly IMediaRepository<Movie> _movieRepository;
        //private readonly IMediaRepository<DiscType> _discTypeRepository;
        private readonly IMediaRepository<Watched> _watchedRepository;
        private readonly IMediaRepository<MediaExtraGenre> _mediaExtraGenreRepository;
        //private readonly IMediaRepository<Genre> _genreRepository;
        //private readonly IMediaRepository<MediaGenre> _mediaGenreRepository;
        //private readonly IMediaRepository<ExtraGenre> _extraGenreRepository;
        private readonly IMediaRepository<PhysicalMedia> _physicalMediaRepository;
        private readonly IMediaRepository<PhysicalMediaMovie> _physicalMediaMovieRepository;
        private readonly IMediaRepository<Cast> _castRepository;
        private readonly IMediaRepository<Crew> _crewRepository;
        public MediaInformationViewModelService(IMediaRepository<Media> mediaRepository,
            IMediaRepository<Movie> movieRepository,
            //IMediaRepository<DiscType> discTypeRepository,
            IMediaRepository<Watched> watchedRepository,
            IMediaRepository<MediaExtraGenre> mediaExtraGenreRepository,
            //IMediaRepository<Genre> genreRepository,
            //IMediaRepository<MediaGenre> mediaGenreRepository,
            //IMediaRepository<ExtraGenre> extraGenreRepository,
            IMediaRepository<PhysicalMedia> physicalMediaRepository,
            IMediaRepository<PhysicalMediaMovie> physicalMediaMovieRepository,
            IMediaRepository<Cast> castRepository,
            IMediaRepository<Crew> crewRepository)
        {
            _mediaRepository = mediaRepository;
            _movieRepository = movieRepository;
            //_discTypeRepository = discTypeRepository;
            _watchedRepository = watchedRepository;
            _mediaExtraGenreRepository = mediaExtraGenreRepository;
            //_genreRepository = genreRepository;
            //_mediaGenreRepository = mediaGenreRepository;
            //_extraGenreRepository = extraGenreRepository;
            _physicalMediaRepository = physicalMediaRepository;
            _physicalMediaMovieRepository = physicalMediaMovieRepository;
            _castRepository = castRepository;
            _crewRepository = crewRepository;
        }

        public async Task<MediaInformationViewModel> CreateViewModel(Guid mediaId, Guid appUserId)
        {
            Media media = await _mediaRepository.GetByIdAsync(mediaId);
            Movie movie = await _movieRepository.SingleOrDefaultAsync(new MovieSpecification(mediaId));
            PhysicalMedia physicalMedia = await _physicalMediaRepository.SingleOrDefaultAsync(new PhysicalMediaSpecification(appUserId, mediaId));//_context.PhysicalMedias.SingleOrDefault(p => p.MediaId == mediaId && p.AppUserId == User.Identity.GetNameIdGuid());
            var mediaExtraGenres = await _mediaExtraGenreRepository.ListAsync(new MediaExtraGenreSpecification(mediaId, physicalMedia.PhysicalMediaId));
            //List<MediaGenre> mediaGenres = _context.MediaGenres.Where(g => g.MediaId == mediaId).ToList();
            //List<ExtraGenre> extraGenres = _context.ExtraGenres.Where(g => g.PhysicalMediaId == physicalMedia.PhysicalMediaId).ToList();
            //List<Genre> genres = _context.Genres.Where(g => mediaGenres.Select(m => m.GenreId).Contains(g.GenreId) || extraGenres.Select(m => m.GenreId).Contains(g.GenreId)).ToList();
            var watched = await _watchedRepository.ListAsync(new WatchedSpecification(physicalMedia.PhysicalMediaId));//_context.Watched.Where(w => w.PhysicalMediaId == physicalMedia.PhysicalMediaId).ToList();
            var cast = await _castRepository.ListAsync(new CastSpecification(mediaId, SpinningFilmHelper.MaxCastOrder));//_context.Casts.Where(c => c.MediaId == mediaId && c.Order < 3).OrderBy(c => c.Order).ToList();
            var crew = await _crewRepository.ListAsync(new CrewSpecification(mediaId, SpinningFilmHelper.Director));//_context.Crews.Where(c => c.MediaId == mediaId && c.Job == "Director").ToList();

            return new MediaInformationViewModel(media, movie, physicalMedia, mediaExtraGenres, watched, cast, crew);
        }

        public async Task<MediaInformationViewModel> CreateViewModelFromPhysicalMedia(PhysicalMedia physicalMedia)
        {
            Media media = await _mediaRepository.GetByIdAsync(physicalMedia.MediaId);
            Movie movie = await _movieRepository.SingleOrDefaultAsync(new MovieSpecification(physicalMedia.MediaId));
            var mediaExtraGenres = await _mediaExtraGenreRepository.ListAsync(new MediaExtraGenreSpecification(physicalMedia.MediaId, physicalMedia.PhysicalMediaId));
            var watched = await _watchedRepository.ListAsync(new WatchedSpecification(physicalMedia.PhysicalMediaId));
            var cast = await _castRepository.ListAsync(new CastSpecification(physicalMedia.MediaId, SpinningFilmHelper.MaxCastOrder));
            var crew = await _crewRepository.ListAsync(new CrewSpecification(physicalMedia.MediaId, SpinningFilmHelper.Director));

            return new MediaInformationViewModel(media, movie, physicalMedia, mediaExtraGenres, watched, cast, crew);
        }
    }
}
