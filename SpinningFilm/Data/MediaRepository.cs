using SpinningFilm.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.Data
{
    public class MediaRepository<T> : BaseRepository<T>, IMediaRepository<T> where T : class
    {
        public MediaRepository(SpinningFilmContext context) : base(context) { }
    }
}
