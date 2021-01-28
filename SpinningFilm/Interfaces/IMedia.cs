using Microsoft.AspNetCore.Mvc.Rendering;
using SpinningFilm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.Interfaces
{
    public interface IMedia
    {
        public string ImdbId { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
    }
}
