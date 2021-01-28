using Microsoft.AspNetCore.Mvc.Rendering;
using SpinningFilm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.ViewModels
{
    public class EditMediaViewModel
    {
        public string ImdbId { get; set; }
        public string Poster { get; set; }
        public int DiscTypeId { get; set; }
        public bool DigitalCopy { get; set; }
        public List<string> ExtraGenres { get; set; } = new List<string>();
        public List<DiscType> DiscTypes { get; set; } = new List<DiscType>();

        public EditMediaViewModel() { }

        public EditMediaViewModel(Movie media, List<DiscType> discTypes, List<MediaGenre> mediaGenres, List<Genre> genres)
        {
            ImdbId = media.ImdbId;
            Poster = media.PosterSmall;
            DiscTypeId = media.DiscTypeId;
            DigitalCopy = media.DigitalCopy;
            DiscTypes = discTypes;

            foreach (var mediaGenre in mediaGenres)
            {
                foreach (var genre in genres)
                {
                    if (mediaGenre.GenreId == genre.GenreId && !genre.Default)
                    {
                        ExtraGenres.Add(genre.Name);
                    }
                }
            }
        }

        public List<SelectListItem> DiscTypeList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in DiscTypes)
            {
                list.Add(new SelectListItem { Text = item.Description, Value = item.DiscTypeId.ToString(), Selected = DiscTypeId == item.DiscTypeId ? true : false });
            }

            return list;
        }
    }
}
