using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StomatoloskaOrdinacija.Data.EntityModels
{
    [Table("IzvrsenaUsluga")]
    public class IzvrsenaUsluga
    {
        [Key]
        public int IzvrsenaUslugaId { get; set; }

        [ForeignKey(nameof(Pregled))]
        public int PregledId { get; set; }
        public Pregled Pregled { get; set; }

        [ForeignKey(nameof(Usluga))]
        public int UslugaId { get; set; }
        public Usluga Usluga { get; set; }

        [Required]
        [Column(TypeName = "DECIMAL(18,2)")]
        public decimal Cijena { get; set; }
    }
}