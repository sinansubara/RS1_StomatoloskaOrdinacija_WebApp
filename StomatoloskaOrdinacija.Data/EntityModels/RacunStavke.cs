using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StomatoloskaOrdinacija.Data.EntityModels
{
    [Table("RacunStavke")]
    public class RacunStavke
    {
        [Key]
        public int RacunStavkeId { get; set; }

        [ForeignKey(nameof(Materijal))]
        public int MaterijalId { get; set; }
        public Materijal Materijal { get; set; }

        [ForeignKey(nameof(Racun))]
        public int RacunId { get; set; }
        public Racun Racun { get; set; }

        [Required]
        [Column(TypeName = "DECIMAL(18,1)")]
        public decimal Kolicina { get; set; }

        [Required]
        [Column(TypeName = "DECIMAL(18,2)")]
        public decimal Cijena { get; set; }
    }
}