using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.Models
{
    [Table("ExtraGenre")]
    public class ExtraGenre
    {
        [Key]
        public Guid ExtraGenreId { get; set; }
        public Guid PhysicalMediaId { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public ExtraGenre() { }

        public ExtraGenre(Guid physicalMediaId, int genreId)
        {
            ExtraGenreId = Guid.NewGuid();
            PhysicalMediaId = physicalMediaId;
            GenreId = genreId;
        }

        public ExtraGenre(Guid physicalMediaId, Genre genre)
        {
            ExtraGenreId = Guid.NewGuid();
            PhysicalMediaId = physicalMediaId;
            GenreId = genre.GenreId;
        }
    }
}
