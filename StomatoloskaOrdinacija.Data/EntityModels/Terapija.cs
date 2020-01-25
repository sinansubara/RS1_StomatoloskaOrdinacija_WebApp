using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StomatoloskaOrdinacija.Data.EntityModels
{
    [Table("Terapija")]
    public class Terapija
    {
        [Key]
        public int TerapijaId { get; set; }

        [ForeignKey(nameof(Lijek))]
        public int LijekId { get; set; }
        public Lijek Lijek { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Vrsta terapije")]
        public string VrstaTerapije { get; set; }

        [Required]
        [Column(TypeName = "TINYINT")]
        public int Kolicina { get; set; }
    }
}