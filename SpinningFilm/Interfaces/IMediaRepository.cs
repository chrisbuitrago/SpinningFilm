using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.Interfaces
{
    public interface IMediaRepository<T> : IBaseRepository<T> where T : class
    {
    }
}
