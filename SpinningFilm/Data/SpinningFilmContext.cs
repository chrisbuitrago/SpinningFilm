using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpinningFilm.Models;

namespace SpinningFilm.Data
{
    public class SpinningFilmContext : DbContext
    {
        public SpinningFilmContext(DbContextOptions<SpinningFilmContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<SeriesSeason> SeriesSeasons { get; set; }
        public DbSet<DiscType> DiscTypes { get; set; }
        public DbSet<Watched> Watched { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MediaGenre> MediaGenres { get; set; }
        public DbSet<MediaUser> MediaUsers { get; set; }
        public DbSet<Crew> Crews { get; set; }
        public DbSet<Cast> Casts { get; set; }

    }
}
