using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpinningFilm.Models;

namespace SpinningFilm.ApiModels
{
    public class TmdbCreditsResult
    {
        public List<Cast> Cast { get; set; }
        public List<Crew> Crew { get; set; }
    }
}
