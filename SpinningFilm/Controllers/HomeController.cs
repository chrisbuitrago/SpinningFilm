using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SpinningFilm.Models;
using SpinningFilm.Helpers;
using SpinningFilm.Services;
using SpinningFilm.Data;
using Microsoft.AspNetCore.Authorization;
using SpinningFilm.ApiModels;
using Newtonsoft.Json;
using SpinningFilm.Interfaces;

namespace SpinningFilm.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SpinningFilmContext _context;
        private IApiService _apiService;

        public HomeController(ILogger<HomeController> logger, SpinningFilmContext context, IApiService apiService)
        {
            _logger = logger;
            _context = context;
            _apiService = apiService;
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> DiscType()
        {
            string key = _apiService.GetTmdbKey();
            string url = string.Format("https://api.themoviedb.org/3/genre/tv/list?api_key={0}", key);
            string responseBody = await _apiService.HttpGet(url);

            TmdbMovieResult result = JsonConvert.DeserializeObject<TmdbMovieResult>(responseBody);

            foreach (var item in result.Genres)
            {
                item.MediaType = "tv";
                _context.Add(item);
            }

            _context.SaveChanges();

            List<DiscType> discTypes = _context.DiscTypes.ToList();
            return View(discTypes);
        }

        public IActionResult DiscTypeAdd()
        {
            int id;
            try
            {
                id = _context.DiscTypes.Max(d => d.DiscTypeId) + 1;
            }
            catch
            {
                id = 1;
            }
            DiscType discType = new DiscType();
            discType.DiscTypeId = id;
            return View(discType);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult DiscTypeAdd(DiscType discType)
        {
            _context.Add(discType);
            _context.SaveChanges();

            return RedirectToAction("DiscType");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
