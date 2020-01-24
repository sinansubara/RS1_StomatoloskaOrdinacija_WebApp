using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StomatoloskaOrdinacija.Data.EntityModels
{
    [Table("Usluga")]
    public class Usluga
    {
        [Key]
        public int UslugaId { get; set; }

        [Required]
        [StringLength(150)]
        public string Naziv { get; set; }
    }
}