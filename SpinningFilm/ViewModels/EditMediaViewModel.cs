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
        public Guid PhysicalMediaId { get; set; }
        public string Poster { get; set; }
        public int DiscTypeId { get; set; }
        public bool DigitalCopy { get; set; }
        public IReadOnlyList<ExtraGenre> ThisDiscExtraGenres { get; set; }
        public IReadOnlyList<Genre> ExtraGenres { get; set; }
        public List<int> ExtraGenreIds { get; set; } = new List<int>();
        public IReadOnlyList<DiscType> DiscTypes { get; set; } = new List<DiscType>();

        public EditMediaViewModel() { }

        public EditMediaViewModel(Media media, PhysicalMedia physicalMedia, IReadOnlyList<DiscType> discTypes, IReadOnlyList<Genre> extraGenres, IReadOnlyList<ExtraGenre> thisDiscExtraGenres)
        {
            PhysicalMediaId = physicalMedia.PhysicalMediaId;
            Poster = media.PosterSmall;
            DiscTypeId = physicalMedia.DiscTypeId;
            DigitalCopy = physicalMedia.DigitalCopy;
            DiscTypes = discTypes;
            ExtraGenres = extraGenres;
            ThisDiscExtraGenres = thisDiscExtraGenres;
            ExtraGenreIds = thisDiscExtraGenres.Select(g => g.GenreId).ToList();
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
