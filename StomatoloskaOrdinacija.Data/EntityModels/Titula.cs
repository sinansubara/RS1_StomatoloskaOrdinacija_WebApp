using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StomatoloskaOrdinacija.Data.EntityModels
{
    [Table("Titule")]
    public class Titula
    {
        [Key]
        public int TitulaId { get; set; }

        [Required]
        [StringLength(50)]
        public string Naziv { get; set; }
    }
}