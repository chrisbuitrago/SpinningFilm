using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.Models
{
    [Table("Cast")]
    public class Cast
    {   
        [Key]
        public Guid CastId { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string CastName { get; set; }
        [JsonProperty(PropertyName = "character")]
        public string CharacterName { get; set; }
        public int Order { get; set; }
        public Guid MediaId { get; set; }
        public Cast()
        {
            CastId = Guid.NewGuid();
        }

    }
}
