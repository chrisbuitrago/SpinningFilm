using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.Models
{
    [Table("MediaUser")]
    public class MediaUser
    {
        [Key, StringLength(36)]
        public string Id { get; set; }
        [StringLength(200)]
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string LastName { get; set; }
        public bool EmailConfirmed { get; set; }
        [StringLength(200)]
        public string NormalizeEmail { get; set; }
        public string SecurityStamp { get; set; }
    }
}
