using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.Models
{
    [Table("MediaType")]
    public class MediaType
    {
        [Key]
        public Guid MediaTypeId { get; set; }
        public string Name { get; set; }
    }
}
