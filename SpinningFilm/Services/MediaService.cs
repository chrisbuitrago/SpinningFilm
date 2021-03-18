using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpinningFilm.Interfaces;
using SpinningFilm.Models;

namespace SpinningFilm.Services
{
    public class MediaService
    {
        private readonly IMediaRepository<Movie> _movieRepository;
        private readonly IMediaRepository<DiscType> _discTypeRepository;
        private readonly IMediaRepository<Watched> _watchedRepository;
        private readonly IMediaRepository<Genre> _genreRepository;
        private readonly IMediaRepository<MediaGenre> _mediaGenreRepository;
        private readonly IMediaRepository<ExtraGenre> _extraGenreRepository;
        private readonly IMediaRepository<PhysicalMediaMovie> _physicalMediaRepository;
        public MediaService(IMediaRepository<Movie> movieRepository,
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


    }
}
