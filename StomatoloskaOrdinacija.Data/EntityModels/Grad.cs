using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StomatoloskaOrdinacija.Data.EntityModels
{
    [Table("Grad")]
    public class Grad
    {
        [Key]
        public int GradId { get; set; }

        [ForeignKey(nameof(Drzava))]
        public int DrzavaId { get; set; }
        public Drzava Drzava { get; set; }

        [Required]
        [StringLength(100)]
        public string Naziv { get; set; }

        [Required]
        [StringLength(20)] 
        public string PostanskiBroj { get; set; }
    }
}