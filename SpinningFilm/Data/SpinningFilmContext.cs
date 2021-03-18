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

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Crew> Crews { get; set; }
        public DbSet<Cast> Casts { get; set; }
        public DbSet<DiscType> DiscTypes { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<MediaGenre> MediaGenres { get; set; }
        public DbSet<ExtraGenre> ExtraGenres { get; set; }
        public DbSet<MediaExtraGenre> MediaExtraGenres { get; set; }
        public DbSet<MediaType> MediaTypes { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<PhysicalMedia> PhysicalMedias { get; set; }
        public DbSet<PhysicalSeason> PhysicalSeasons { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<Watched> Watched { get; set; }
        public DbSet<PhysicalMediaMovie> PhysicalMediaMovies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<PhysicalMediaMovie>(entity =>
            {
                entity.ToView("PhysicalMediaMovie");
                entity.HasNoKey();
            });

            modelBuilder.Entity<MediaExtraGenre>(entity =>
            {
                entity.ToView("MediaExtraGenre");
                entity.HasNoKey();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
