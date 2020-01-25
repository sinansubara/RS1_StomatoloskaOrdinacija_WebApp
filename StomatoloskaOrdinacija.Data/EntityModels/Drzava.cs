using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace StomatoloskaOrdinacija.Data.EntityModels
{
    [Table("Drzava")]
    public class Drzava
    {
        [Key]
        public int DrzavaId { get; set; }

        [Required]
        [StringLength(100)]
        public string Naziv { get; set; }
    }
}