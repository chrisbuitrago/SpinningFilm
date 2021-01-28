using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.Models
{
    [Table("MediaGenre")]
    public class MediaGenre
    {
        [Key]
        public Guid MediaGenreId { get; set; }
        public string ImdbId { get; set; }
        public int GenreId { get; set; }
        public string MediaType { get; set; }
        public Genre Genre { get; set; }
        public MediaGenre() { }

        public MediaGenre(string imdbId, int genreId)
        {
            MediaGenreId = Guid.NewGuid();
            ImdbId = imdbId;
            GenreId = genreId;
        }

        public MediaGenre(string imdbId, Genre genre)
        {
            MediaGenreId = Guid.NewGuid();
            ImdbId = imdbId;
            GenreId = genre.GenreId;
            MediaType = genre.MediaType;
        }
    }
}

