using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpinningFilm.Models;
using SpinningFilm.Helpers;
using SpinningFilm.Services;
using SpinningFilm.Data;
using SpinningFilm.Interfaces;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using SpinningFilm.ViewModels;
using SpinningFilm.ApiModels;
using SpinningFilm.Extensions;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace SpinningFilm.Controllers
{
    [Authorize]     
    public class SearchController : Controller
    {
        private readonly SpinningFilmContext _context;
        private IApiService _apiService;

        public SearchController(SpinningFilmContext context, IApiService apiService)
        {
            _context = context;
            _apiService = apiService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string term)
        {
            string responseBody = await _apiService.SearchAsync(term);
            TmdbResult result = JsonConvert.DeserializeObject<TmdbResult>(responseBody);

            result.Results = result.Results.Where(r => r.Type == SpinningFilmHelper.MovieType || r.Type == SpinningFilmHelper.SeriesType).OrderByDescending(r => r.Popularity).ToList();

            return View(result);
        }

        public IActionResult AddMediaModal(string tmdbId, string type, string poster, string title)
        {
            if(type == SpinningFilmHelper.MovieType)
            {
                return RedirectToAction("AddMovieModal", new { tmdbId, type, poster, title });
            }
            else if(type == SpinningFilmHelper.SeriesType)
            {
                return RedirectToAction("AddSeriesModal", new { tmdbId, type });
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public IActionResult AddMovieModal(AddMovieViewModel movie)
        {
            movie.Poster = TmdbSettings.PosterSmall + movie.Poster;
            movie.DiscTypes = _context.DiscTypes.ToList();
            movie.ExtraGenres = _context.Genres.Where(g => !g.Extra).OrderBy(g => g.Name).ToList();

            return View(movie);
        }

        public async Task<IActionResult> AddSeriesModal(AddSeriesViewModel addSeries)
        {
            string responseBody = await _apiService.GetWithExternalIdsAsync(addSeries.TmdbId, SpinningFilmHelper.SeriesType);
            TmdbTVResult result = JsonConvert.DeserializeObject<TmdbTVResult>(responseBody);
            addSeries.ImdbId = result.ExternalIds.ImdbId;
            addSeries.Title = result.Name;
            addSeries.DiscTypes = _context.DiscTypes.ToList();
            addSeries.Poster = TmdbSettings.PosterSmall + result.Poster;
            addSeries.NumberOfSeasons = result.NumberOfSeasons;

            Media media = _context.Medias.SingleOrDefault(m => m.ImdbId == addSeries.ImdbId) ?? new Media();

            Guid userId = User.Identity.GetNameIdGuid();
            for (int i = 0; i < addSeries.NumberOfSeasons; i++)
            {
                addSeries.PhysicalMedias.Add(new PhysicalMedia(media.MediaId, userId));
            }

            return View(addSeries);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSeries(AddSeriesViewModel addSeries)
        {
            Media media = _context.Medias.SingleOrDefault(m => m.ImdbId == addSeries.ImdbId);
            if (media == null)
            {
                MediaType mediaType = _context.MediaTypes.SingleOrDefault(m => m.Name == SpinningFilmHelper.SeriesType);

                string responseBody = await _apiService.GetWithCreditsAsync(addSeries.TmdbId, SpinningFilmHelper.SeriesType);
                TmdbTVResult tvResult = JsonConvert.DeserializeObject<TmdbTVResult>(responseBody);

                responseBody = await _apiService.GetOmdbResult(addSeries.ImdbId);
                RatingResult ratingResult = JsonConvert.DeserializeObject<RatingResult>(responseBody);

                media = new Media(tvResult, mediaType, ratingResult, addSeries.ImdbId);
                _context.Add(media);

                Series series = new Series(tvResult, media.MediaId);
                _context.Add(series);

                List<Genre> dbGenres = _context.Genres.ToList();
                foreach (var item in tvResult.Genres)
                {
                    var genre = dbGenres.SingleOrDefault(g => g.GenreId == item.GenreId) ?? _context.Genres.Add(item).Entity;
                    MediaGenre mediaGenre = new MediaGenre(media.MediaId, genre);
                    _context.Add(mediaGenre);
                }

                tvResult.Credits.Cast.ForEach(c => c.MediaId = media.MediaId);
                _context.Casts.AddRange(tvResult.Credits.Cast);

                tvResult.Credits.Crew.ForEach(c => c.MediaId = media.MediaId);
                _context.Crews.AddRange(tvResult.Credits.Crew);

                _context.SaveChanges();
            }

            if(!_context.PhysicalMedias.Any(m => m.AppUserId == User.Identity.GetNameIdGuid() && m.MediaId == media.MediaId))
            {
                foreach (var number in addSeries.SeasonNumbers)
                {
                    var physicalMedia = addSeries.PhysicalMedias[number-1];
                    physicalMedia.MediaId = media.MediaId;
                    physicalMedia.AppUserId = User.Identity.GetNameIdGuid();
                    _context.PhysicalMedias.Add(physicalMedia);

                    PhysicalSeason physicalSeason = new PhysicalSeason(physicalMedia.PhysicalMediaId, number);
                    _context.PhysicalSeasons.Add(physicalSeason);
                }

                _context.SaveChanges();

                return View("AddMedia", media);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMovie(AddMovieViewModel addMedia)
        {
            Media media = _context.Medias.SingleOrDefault(m => m.TmdbId == addMedia.TmdbId);
            List<Genre> dbGenres = _context.Genres.ToList();
            if (media == null)
            {
                MediaType mediaType = _context.MediaTypes.SingleOrDefault(m => m.Name == SpinningFilmHelper.MovieType);

                string responseBody = await _apiService.GetWithCreditsAsync(addMedia.TmdbId, SpinningFilmHelper.MovieType);
                TmdbMovieResult movieResult = JsonConvert.DeserializeObject<TmdbMovieResult>(responseBody);

                responseBody = await _apiService.GetOmdbResult(movieResult.ImdbId);
                RatingResult ratingResult = JsonConvert.DeserializeObject<RatingResult>(responseBody);

                media = new Media(movieResult, mediaType, ratingResult, movieResult.ImdbId);
                _context.Add(media);

                Movie movie = new Movie(movieResult, media.MediaId);
                _context.Add(movie);

                foreach (var item in movieResult.Genres)
                {
                    var genre = dbGenres.SingleOrDefault(g => g.GenreId == item.GenreId) ?? _context.Genres.Add(item).Entity;
                    MediaGenre mediaGenre = new MediaGenre(media.MediaId, genre);
                    _context.Add(mediaGenre);
                }

                movieResult.Credits.Cast.ForEach(c => c.MediaId = media.MediaId);
                _context.Casts.AddRange(movieResult.Credits.Cast);

                movieResult.Credits.Crew.ForEach(c => c.MediaId = media.MediaId);
                _context.Crews.AddRange(movieResult.Credits.Crew); 

                _context.SaveChanges();
            }

            Guid userId = User.Identity.GetNameIdGuid();
            if (!_context.PhysicalMedias.Any(m => m.AppUserId == userId && m.MediaId == media.MediaId))
            {                
                PhysicalMedia physicalMedia = new PhysicalMedia(media.MediaId, userId, (bool)addMedia.DigitalCopy, addMedia.DiscType);
                _context.PhysicalMedias.Add(physicalMedia);

                foreach (var genreId in addMedia.ExtraGenreIds)
                {
                    var genre = dbGenres.SingleOrDefault(g => g.GenreId == genreId);
                    ExtraGenre extraGenre = new ExtraGenre(physicalMedia.PhysicalMediaId, genre);
                    _context.Add(extraGenre);
                }

                _context.SaveChanges();

                return View("AddMedia", media);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
