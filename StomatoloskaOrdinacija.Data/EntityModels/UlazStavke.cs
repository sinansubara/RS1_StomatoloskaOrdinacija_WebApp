using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StomatoloskaOrdinacija.Data.EntityModels
{
    [Table("UlazStavke")]
    public class UlazStavke
    {
        [Key]
        public int UlazStavkeId { get; set; }

        [ForeignKey(nameof(Materijal))]
        public int MaterijalId { get; set; }
        public Materijal Materijal { get; set; }

        [ForeignKey(nameof(UlazUSkladiste))]
        public int UlazUSkladisteId { get; set; }
        public UlazUSkladiste UlazUSkladiste { get; set; }

        [Required]
        [Column(TypeName = "DECIMAL(18,1)")]
        public decimal Kolicina { get; set; }

        [Required]
        [Column(TypeName = "DECIMAL(18,2)")]
        public decimal Cijena { get; set; }
    }
}