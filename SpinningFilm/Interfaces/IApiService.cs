using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.Interfaces
{
    public interface IApiService
    {
        string GetTmdbKey();
        string GetOmdbKey();
        Task<string> GetOmdbResult(string imdbId);
        Task<string> SearchAsync(string term);
        Task<string> GetWithExternalIdsAsync(string tmdbId, string type);
        Task<string> GetWithCreditsAsync(string tmdbId, string type);
        Task<string> HttpGet(string url);
    }
}