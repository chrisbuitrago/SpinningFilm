using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.Models
{
    [Table("Crew")]
    public class Crew
    {
        [Key]
        public Guid CrewId { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string CrewName { get; set; }
        public string Department { get; set; }
        public string Job { get; set; }
        public string ImdbId { get; set; }

        public Crew()
        {
            CrewId = Guid.NewGuid();
        }
    }
}
