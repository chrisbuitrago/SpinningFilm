using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpinningFilm.Models;

namespace SpinningFilm.ViewModels
{
    public class MediaInformationViewModel
    {
        public Media Media { get; set; }
        public Movie Movie { get; set; }
        public PhysicalMedia PhysicalMedia { get; set; }
        public IReadOnlyList<Genre> Genres { get; set; }
        public IReadOnlyList<MediaExtraGenre> MediaExtraGenres { get; set; }
        public IReadOnlyList<Watched> Watched { get; set; }
        public IReadOnlyList<Cast> Cast { get; set; }
        public IReadOnlyList<Crew> Crew { get; set; }

        public MediaInformationViewModel() { }

        public MediaInformationViewModel(Media media,
            Movie movie,
            PhysicalMedia physicalMedia,
            List<Genre> genres,
            List<Watched> watched,
            List<Cast> cast,
            List<Crew> crew)
        {
            Media = media;
            Movie = movie;
            PhysicalMedia = physicalMedia;
            Genres = genres;
            Watched = watched;
            Cast = cast;
            Crew = crew;
        }

        public MediaInformationViewModel(Media media,
        Movie movie,
        PhysicalMedia physicalMedia,
        IReadOnlyList<MediaExtraGenre> mediaExtraGenres,
        IReadOnlyList<Watched> watched,
        IReadOnlyList<Cast> cast,
        IReadOnlyList<Crew> crew)
        {
            Media = media;
            Movie = movie;
            PhysicalMedia = physicalMedia;
            MediaExtraGenres = mediaExtraGenres;
            Watched = watched;
            Cast = cast;
            Crew = crew;
        }
    }
}
