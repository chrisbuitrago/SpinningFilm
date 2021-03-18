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
using SpinningFilm.Helpers;
using SpinningFilm.Auth;

namespace SpinningFilm.Controllers
{
    [Authorize]
    public class MediaController : Controller
    {
        private readonly SpinningFilmContext _context;
        private readonly IApiService _apiService;
        private readonly IMediaListViewModelService _mediaListViewModelService;
        private readonly IPhysicalMediaService _physicalMediaService;
        private readonly IMediaInformationViewModelService _mediaInformationViewModelService;
        private readonly IWatchedViewModelService _watchedViewModelService;
        private readonly IEditMediaViewModelService _editMediaViewModelService;
        private readonly IAuthorizationService _authorizationService;

        public MediaController(SpinningFilmContext context, IApiService apiService, IMediaListViewModelService mediaListViewModelService,
            IPhysicalMediaService physicalMediaService,
            IMediaInformationViewModelService mediaInformationViewModelService,
            IWatchedViewModelService watchedViewModelService,
            IEditMediaViewModelService editMediaViewModelService,
            IAuthorizationService authorizationService)
        {
            _context = context;
            _apiService = apiService;
            _mediaListViewModelService = mediaListViewModelService;
            _physicalMediaService = physicalMediaService;
            _mediaInformationViewModelService = mediaInformationViewModelService;
            _watchedViewModelService = watchedViewModelService;
            _editMediaViewModelService = editMediaViewModelService;
            _authorizationService = authorizationService;
        }
        
        public async Task<IActionResult> Index(MediaFilter filter)
        {
            filter.UserId = User.Identity.GetNameIdGuid();
            MediaListViewModel mediaList = await _mediaListViewModelService.CreateViewModel(filter);

            return View(mediaList);
        }

        public async Task<IActionResult> MediaList(MediaFilter filter)
        {
            filter.UserId = User.Identity.GetNameIdGuid();
            MediaListViewModel mediaList = await _mediaListViewModelService.CreateViewModel(filter);

            return View(mediaList);
        }

        public async Task<IActionResult> MediaDelete(Guid physicalMediaId)
        {
            PhysicalMedia physicalMedia = await _physicalMediaService.Get(physicalMediaId);

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, physicalMedia, new SameUserRequirement());

            if (!authorizationResult.Succeeded)
            {
                return new ForbidResult();
            }

            await _physicalMediaService.Delete(physicalMedia);
            return RedirectToAction("Index", new { type = SpinningFilmHelper.MovieType });
        }

        public IActionResult WatchedModal(WatchedViewModel watched)
        {
            return View(watched);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> WatchedAdd(WatchedViewModel watched)
        {
            PhysicalMedia physicalMedia = await _physicalMediaService.Get(watched.PhysicalMediaId);

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, physicalMedia, new SameUserRequirement());

            if (!authorizationResult.Succeeded)
            {
                return new ForbidResult();
            }

            var result = await _watchedViewModelService.AddWatched(physicalMedia, watched.Date);
            return Json(result);
        }

        public async Task<IActionResult> InfoModal(Guid physicalMediaId)
        {
            PhysicalMedia physicalMedia = await _physicalMediaService.Get(physicalMediaId);

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, physicalMedia, new SameUserRequirement());

            if (!authorizationResult.Succeeded)
            {
                return new ForbidResult();
            }

            MediaInformationViewModel mediaVM = await _mediaInformationViewModelService.CreateViewModelFromPhysicalMedia(physicalMedia);
            return View(mediaVM);
        }

        public async Task<IActionResult> EditModal(Guid physicalMediaId)
        {
            PhysicalMedia physicalMedia = await _physicalMediaService.Get(physicalMediaId);

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, physicalMedia, new SameUserRequirement());

            if (!authorizationResult.Succeeded)
            {
                return new ForbidResult();
            }

            EditMediaViewModel editMedia = await _editMediaViewModelService.CreateViewModel(physicalMedia);

            return View(editMedia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMedia(EditMediaViewModel editMedia)
        {
            PhysicalMedia physicalMedia = await _physicalMediaService.Get(editMedia.PhysicalMediaId);

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, physicalMedia, new SameUserRequirement());

            if (!authorizationResult.Succeeded)
            {
                return new ForbidResult();
            }

            //PhysicalMedia physicalMedia = _context.PhysicalMedias.SingleOrDefault(p => p.MediaId == editMedia.PhysicalMediaId && p.AppUserId == User.Identity.GetNameIdGuid());
            physicalMedia.DiscTypeId = editMedia.DiscTypeId;
            physicalMedia.DigitalCopy = editMedia.DigitalCopy;
            await _physicalMediaService.Update(physicalMedia);

            List<Genre> genres = _context.Genres.Where(g => g.Extra).ToList();
            List<MediaGenre> mediaGenres = _context.MediaGenres.Where(m => m.MediaId == editMedia.PhysicalMediaId && genres.Select(g => g.GenreId).Contains(m.GenreId)).ToList();

            if (mediaGenres != null)
            {
                _context.RemoveRange(mediaGenres);
            }

            foreach(var extraGenreId in editMedia.ExtraGenreIds)
            {
                var genre = genres.SingleOrDefault(g => g.GenreId == extraGenreId);
                _context.Add(new ExtraGenre(physicalMedia.PhysicalMediaId, genre));
            }

            _context.SaveChanges();

            return RedirectToAction("Index", new { type = SpinningFilmHelper.MovieType });
        }
    }
}