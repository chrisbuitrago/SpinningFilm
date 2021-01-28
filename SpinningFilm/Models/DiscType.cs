using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.Models
{
    [Table("DiscType")]
    public class DiscType
    {
        [Key]
        public int DiscTypeId { get; set; }
        [Required]
        public string Description { get; set; }
        [NotMapped]
        public bool Filtered { get; set; }

        [NotMapped]
        public string DiscTypeLogoLink
        {
            get
            {
                if (DiscTypeId == 1)
                {
                    return "/img/blu-ray.svg";
                }
                else if (DiscTypeId == 2)
                {
                    return "/img/4k-uhd2.svg";
                }
                else if(DiscTypeId == 3)
                {
                    return "/img/dvd.svg";
                }
                else
                {
                    return "";
                }
            }
        }
    }
}
