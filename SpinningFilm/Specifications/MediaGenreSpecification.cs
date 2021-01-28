using SpinningFilm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.Specifications
{
    public class MediaGenreSpecification : BaseSpecification<MediaGenre>
    {
        public MediaGenreSpecification(string mediaType)
            : base(mg => mg.MediaType == mediaType)
        {

        }
    }
}
