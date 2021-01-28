using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using SpinningFilm.ViewModels;

namespace SpinningFilm.Models
{
    [Table("Watched")]
    public class Watched
    {
        [Key]
        public Guid WatchedId { get; set; }
        public string ImdbId { get; set; }
        public DateTime Date { get; set; }
        public Watched()
        {
            WatchedId = Guid.NewGuid();
        }
        public Watched(WatchedViewModel watched)
        {
            WatchedId = Guid.NewGuid();
            ImdbId = watched.ImdbId;
            Date = watched.Date;
        }
    }
}
