﻿using SpinningFilm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.Specifications
{
    public class GenreSpecification : BaseSpecification<Genre>
    {
        public GenreSpecification(bool extra)
            : base(g => g.Extra == extra)
        {

        }
    }
}
