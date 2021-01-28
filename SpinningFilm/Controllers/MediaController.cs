using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpinningFilm.Data;
using SpinningFilm.Services;
using SpinningFilm.Models;
using SpinningFilm.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using SpinningFilm.Extensions;
using SpinningFilm.Interfaces;

namespace SpinningFilm.Controllers
{
    [Authorize]
    public class MediaController : Controller
    {
        private readonly SpinningFilmContext _context;
        private IApiService _apiService;
        private IMediaListViewModelService _mediaListViewModelService;

        public MediaController(SpinningFilmContext context, IApiService apiService, IMediaListViewModelService mediaListViewModelService)
        {
            _context = context;
            _apiService = apiService;
            _mediaListViewModelService = mediaListViewModelService;
        }
        
        public async Task<IActionResult> Index(MediaFilter filter)
        {
            filter.UserId = User.Identity.NameId();
            MediaListViewModel mediaList = await _mediaListViewModelService.CreateViewModel(filter);

            return View(mediaList);
        }

        public async Task<IActionResult> MediaList(MediaFilter filter)
        {
            filter.UserId = User.Identity.NameId();
            MediaListViewModel mediaList = await _mediaListViewModelService.CreateViewModel(filter);

            return View(mediaList);
        }

        public IActionResult MediaDelete(string imdbId, string type)
        {
            Movie media = _context.Movies.SingleOrDefault(m => m.MediaUserId == User.Identity.NameId() && m.ImdbId == imdbId);
            _context.Remove(media);
            List<Watched> watched = _context.Watched.Where(m => m.ImdbId == imdbId).ToList();
            foreach (var item in watched)
            {
                _context.Remove(item);
            }
            List<MediaGenre> mediaGenres = _context.MediaGenres.Where(m => m.ImdbId == imdbId).ToList();
            foreach (var item in mediaGenres)
            {
                _context.Remove(item);
            }
            List<Cast> cast = _context.Casts.Where(c => c.ImdbId == imdbId).ToList();
            foreach(var item in cast)
            {
                _context.Remove(item);
            }
            List<Crew> crew = _context.Crews.Where(c => c.ImdbId == imdbId).ToList();
            foreach (var item in crew)
            {
                _context.Remove(item);
            }
            _context.SaveChanges();
            return RedirectToAction("Index", new { type });
        }

        public IActionResult WatchedModal(WatchedViewModel watched)
        {
            return View(watched);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult WatchedAdd(WatchedViewModel watched)
        {
            Movie media = _context.Movies.SingleOrDefault(m => m.MediaUserId == User.Identity.NameId() && m.ImdbId == watched.ImdbId);
            media.Watched = true;
            _context.Update(media);
            _context.Add(new Watched(watched));
            _context.SaveChanges();
            List<Watched> watchedList = _context.Watched.Where(w => w.ImdbId == watched.ImdbId).ToList();
            watched.Count = watchedList.Count();
            watched.LastWatched = watchedList.Max(w => w.Date).ToString("MM/dd/yyyy");
            JsonResult result = Json(watched);
            return result;
        }

        public IActionResult InfoModal(string imdbId)
        {
            Movie media = _context.Movies.SingleOrDefault(m => m.MediaUserId == User.Identity.NameId() && m.ImdbId == imdbId);
            List<MediaGenre> mediaGenres = _context.MediaGenres.Where(g => g.ImdbId == imdbId).ToList();
            List<Genre> genres = _context.Genres.Where(g => g.MediaType == media.Type).ToList();
            List<Watched> watched = _context.Watched.Where(w => w.ImdbId == imdbId).ToList();
            List<Cast> cast = _context.Casts.Where(c => c.ImdbId == imdbId && c.Order < 3).OrderBy(c => c.Order).ToList();
            List<Crew> crew = _context.Crews.Where(c => c.ImdbId == imdbId && c.Job == "Director").ToList();

            MediaInformationViewModel mediaVM = new MediaInformationViewModel(media, mediaGenres, genres, watched, cast, crew);
            return View(mediaVM);
        }

        public IActionResult EditModal(string imdbId)
        {
            Movie media = _context.Movies.SingleOrDefault(m => m.MediaUserId == User.Identity.NameId() && m.ImdbId == imdbId);
            List<DiscType> discTypes = _context.DiscTypes.ToList();
            List<MediaGenre> mediaGenres = _context.MediaGenres.Where(m => m.ImdbId == imdbId).ToList();
            List<Genre> genres = _context.Genres.Where(g => g.MediaType == media.Type).ToList();
            EditMediaViewModel editMedia = new EditMediaViewModel(media, discTypes, mediaGenres, genres);

            return View(editMedia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditMedia(string imdbId, List<string> extraGenres, int discType, bool digitalCopy)
        {
            Movie media = _context.Movies.SingleOrDefault(m => m.MediaUserId == User.Identity.NameId() && m.ImdbId == imdbId);
            media.DiscTypeId = discType;            
            media.DigitalCopy = digitalCopy;

            List<Genre> genres = _context.Genres.Where(g => g.MediaType == media.Type).ToList();
            List<MediaGenre> mediaGenres = _context.MediaGenres.Where(m => m.ImdbId == imdbId).ToList();
            foreach (var mediaGenre in mediaGenres)
            {
                foreach(var genre in genres)
                {
                    if(mediaGenre.GenreId == genre.GenreId && !genre.Default)
                    {
                        _context.Remove(mediaGenre);
                    }
                }
            }
            foreach(var item in extraGenres)
            {
                var genre = genres.SingleOrDefault(g => g.Name == item);
                _context.Add(new MediaGenre(media.ImdbId, genre));
            }
            _context.SaveChanges();
            return RedirectToAction("Index", new { type = media.Type });
        }
    }
}