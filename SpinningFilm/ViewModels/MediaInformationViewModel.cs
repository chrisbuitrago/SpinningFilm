using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpinningFilm.Models;

namespace SpinningFilm.ViewModels
{
    public class MediaInformationViewModel
    {
        public Movie Media { get; set; }
        public List<MediaGenre> MediaGenres { get; set; }
        public List<Watched> Watched { get; set; }
        public List<Cast> Cast { get; set; }
        public List<Crew> Crew { get; set; }

        public MediaInformationViewModel() { }
        public MediaInformationViewModel(Movie media, 
                                         List<MediaGenre> mediaGenres, 
                                         List<Genre> genres, 
                                         List<Watched> watched, 
                                         List<Cast> cast, 
                                         List<Crew> crew)
        {
            Media = media;
            MediaGenres = mediaGenres;
            Watched = watched;
            Cast = cast;
            Crew = crew;
        }
    }
}
