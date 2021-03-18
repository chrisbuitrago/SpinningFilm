using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.Models
{
    [Table("Genre")]
    public class Genre
    {
        [Key]
        [JsonProperty(PropertyName = "id")]
        public int GenreId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public bool Extra { get; set; }
        public Genre()
        {
            Extra = true;
        }

        public List<Genre> MissingGenres(List<Genre> dbGenres, List<string> Genres)
        {
            List<Genre> missingGenres = new List<Genre>();
            foreach(var item in Genres)
            {
                bool exists = dbGenres.Any(g => g.Name == item);
                if (!exists)
                {
                    missingGenres.Add(new Genre { Name = item });
                }
            }

            return missingGenres;
        }
    }
}
