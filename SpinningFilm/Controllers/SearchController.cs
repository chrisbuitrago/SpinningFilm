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

            result.Results = result.Results.Where(r => r.Type == TmdbSettings.MovieType || r.Type == TmdbSettings.SeriesType).OrderByDescending(r => r.Popularity).ToList();

            return View(result);
        }

        public IActionResult AddMediaModal(string tmdbId, string type, string poster, string title)
        {
            if(type == TmdbSettings.MovieType)
            {
                return RedirectToAction("AddMovieModal", new { tmdbId, type, poster, title });
            }
            else if(type == TmdbSettings.SeriesType)
            {
                return RedirectToAction("AddSeriesModal", new { tmdbId, type });
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public IActionResult AddMovieModal(AddMediaModalViewModel movie)
        {
            movie.Poster = TmdbSettings.PosterSmall + movie.Poster;
            movie.DiscTypes = _context.DiscTypes.ToList();

            return View(movie);
        }

        public async Task<IActionResult> AddSeriesModal(AddSeriesViewModel addSeries)
        {
            string responseBody = await _apiService.GetWithExternalIdsAsync(addSeries.TmdbId, TmdbSettings.SeriesType);
            TmdbTVResult result = JsonConvert.DeserializeObject<TmdbTVResult>(responseBody);
            addSeries.ImdbId = result.ExternalIds.ImdbId;
            addSeries.Title = result.Name;
            addSeries.DiscTypes = _context.DiscTypes.ToList();
            addSeries.FirstAired = result.FirstAired;
            addSeries.Poster = TmdbSettings.PosterSmall + result.Poster;
            addSeries.NumberOfSeasons = result.NumberOfSeasons;

            for (int i = 0; i < addSeries.NumberOfSeasons; i++)
            {
                addSeries.Seasons.Add(new SeriesSeason(result.ExternalIds.ImdbId, i + 1));
            }

            return View(addSeries);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSeries(AddSeriesViewModel addSeries)
        {
            if (!_context.Series.Any(m => m.ImdbId == addSeries.ImdbId))
            {
                string responseBody = await _apiService.GetWithCreditsAsync(addSeries.TmdbId, TmdbSettings.SeriesType);
                TmdbTVResult tvResult = JsonConvert.DeserializeObject<TmdbTVResult>(responseBody);

                List<MediaGenre> mediaGenres = _context.MediaGenres.Where(m => m.ImdbId == addSeries.ImdbId).ToList();
                foreach (var item in mediaGenres)
                {
                    _context.Remove(item);
                }

                List<Genre> dbGenres = _context.Genres.Where(g => g.MediaType == addSeries.Type).ToList();
                foreach (var item in addSeries.ExtraGenres)
                {
                    var genre = dbGenres.SingleOrDefault(g => g.Name == item);
                    _context.Add(new MediaGenre(addSeries.ImdbId, genre));
                }

                foreach (var item in tvResult.Genres)
                {
                    var genre = dbGenres.SingleOrDefault(g => g.GenreId == item.GenreId);
                    MediaGenre mediaGenre = new MediaGenre(addSeries.ImdbId, genre);
                    _context.Add(mediaGenre);
                }

                foreach (var item in tvResult.Credits.Cast)
                {
                    item.ImdbId = addSeries.ImdbId;
                    _context.Add(item);
                }

                foreach (var item in tvResult.Credits.Crew)
                {
                    item.ImdbId = addSeries.ImdbId;
                    _context.Add(item);
                }

                foreach (var number in addSeries.SeasonNumbers)
                {
                    var seriesSeason = addSeries.Seasons.SingleOrDefault(s => s.SeasonNumber == number);
                    seriesSeason.ImdbId = addSeries.ImdbId;
                    _context.SeriesSeasons.Add(seriesSeason);
                }

                responseBody = await _apiService.GetOmdbResult(addSeries.ImdbId);
                RatingResult ratingResult = JsonConvert.DeserializeObject<RatingResult>(responseBody);

                string mediaUserId = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;
                Series series = new Series(mediaUserId, addSeries.ImdbId, tvResult, ratingResult);
                _context.Add(series);
                _context.SaveChanges();
                return View("AddMedia", series);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMovie(AddMediaModalViewModel addMediaVM)
        {
            if (!_context.Movies.Any(m => m.TmdbId == addMediaVM.TmdbId))
            {
                string responseBody = await _apiService.GetWithCreditsAsync(addMediaVM.TmdbId, TmdbSettings.MovieType);
                TmdbMovieResult movieResult = JsonConvert.DeserializeObject<TmdbMovieResult>(responseBody);

                List<MediaGenre> mediaGenres = _context.MediaGenres.Where(m => m.ImdbId == movieResult.ImdbId).ToList();
                foreach (var item in mediaGenres)
                {
                    _context.Remove(item);
                }

                List<Genre> dbGenres = _context.Genres.Where(g => g.MediaType == addMediaVM.Type).ToList();
                foreach (var item in addMediaVM.ExtraGenres)
                {
                    var genre = dbGenres.SingleOrDefault(g => g.Name == item);
                    _context.Add(new MediaGenre(movieResult.ImdbId, genre));
                }

                foreach (var item in movieResult.Genres)
                {
                    var genre = dbGenres.SingleOrDefault(g => g.GenreId == item.GenreId);
                    MediaGenre mediaGenre = new MediaGenre(movieResult.ImdbId, genre);
                    _context.Add(mediaGenre);
                }

                foreach(var item in movieResult.Credits.Cast)
                {
                    item.ImdbId = movieResult.ImdbId;
                    _context.Add(item);
                }

                foreach (var item in movieResult.Credits.Crew)
                {
                    item.ImdbId = movieResult.ImdbId;
                    _context.Add(item);
                }

                responseBody = await _apiService.GetOmdbResult(movieResult.ImdbId);
                RatingResult ratingResult = JsonConvert.DeserializeObject<RatingResult>(responseBody);

                string mediaUserId = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;
                Movie media = new Movie(mediaUserId, movieResult, ratingResult, addMediaVM.Type, addMediaVM.DiscType, (bool)addMediaVM.DigitalCopy);
                _context.Add(media);
                _context.SaveChanges();
                return View("AddMedia", media);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
