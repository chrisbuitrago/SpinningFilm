using SpinningFilm.Helpers.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.Interfaces
{
    public interface IIncludeQuery
    {
        Dictionary<IIncludeQuery, string> PathMap { get; }
        IncludeVisitor Visitor { get; }
        HashSet<string> Paths { get; }
    }

    public interface IIncludeQuery<TEntity, out TPreviousProperty> : IIncludeQuery
    {
    }
}
