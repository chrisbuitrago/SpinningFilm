using SpinningFilm.Interfaces;
using SpinningFilm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.Services
{
    public class MovieService
    {
        private readonly IMediaRepository<Movie> _movieRepository;

        public MovieService(IMediaRepository<Movie> movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task DeleteMovie()
        {

        }
    }
}
