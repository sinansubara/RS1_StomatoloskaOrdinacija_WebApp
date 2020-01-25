using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StomatoloskaOrdinacija.Data.EntityModels
{
    [Table("Dijagnoza")]
    public class Dijagnoza
    { 
        [Key]
        public int DijagnozaId { get; set; }

        [Required]
        [StringLength(200)]
        public string Naziv { get; set; }
    }
}