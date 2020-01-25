using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StomatoloskaOrdinacija.Data.EntityModels
{
    [Table("Materijal")]
    public class Materijal
    {
        [Key]
        public int MaterijalId { get; set; }

        [Required]
        [StringLength(100)]
        public string Naziv { get; set; }

        [Required]
        [StringLength(300)]
        public string Opis { get; set; }

        [Required]
        [StringLength(100)]
        public string Vrsta { get; set; }

        [Required]
        [StringLength(100)]
        public string Proizvodjac { get; set; }

        [Required]
        [Column(TypeName = "DECIMAL(18,1)")]
        public decimal Kolicina { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Mjerna jedinica")]
        public string MjernaJedinica { get; set; }
    }
}