using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using SpinningFilm.ApiModels;
using SpinningFilm.Helpers;
using SpinningFilm.Interfaces;

namespace SpinningFilm.Models
{
    [Table("Movie")]
    public class Movie
    {
        [Key]
        [Required]
        public Guid MovieId { get; set; }
        public Guid MediaId { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Runtime { get; set; }
        [NotMapped]
        public string Genre { get; set; }
        [NotMapped]
        public DiscType DiscType { get; set; }
        [NotMapped]
        public List<Watched> WatchedList { get; set; } = new List<Watched>();        

        [NotMapped]
        public string LastWatched
        {
            get
            {
                if (WatchedList != null && WatchedList.Count > 0)
                {
                    return WatchedList.Max(w => w.Date).ToString("MM/dd/yyyy");
                }
                else
                {
                    return "";
                }
            }
        }

        public Movie() { }

        public Movie(TmdbMovieResult movieResult, Guid mediaId)
        {
            MovieId = Guid.NewGuid();
            MediaId = mediaId;
            ReleaseDate = movieResult.ReleaseDate;
            Runtime = movieResult.Runtime;
        }
    }
}
