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
        public Guid MediaId { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public MediaGenre() { }

        public MediaGenre(Guid mediaId, int genreId)
        {
            MediaGenreId = Guid.NewGuid();
            MediaId = mediaId;
            GenreId = genreId;
        }

        public MediaGenre(Guid mediaId, Genre genre)
        {
            MediaGenreId = Guid.NewGuid();
            MediaId = mediaId;
            GenreId = genre.GenreId;
        }
    }
}

