using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StomatoloskaOrdinacija.Data.EntityModels
{
    [Table("Termini")]
    public class Termin
    {
        [Key]
        public int TerminId { get; set; }

        [ForeignKey(nameof(Pacijent))]
        public int PacijentId { get; set; }
        public Pacijent Pacijent { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DatumVrijeme { get; set; }

        [Required]
        [StringLength(200)]
        public string Razlog { get; set; }

        [Required]
        [Column(TypeName = "BIT")]
        public bool Hitan { get; set; }

        [Required]
        [Column(TypeName = "BIT")]
        public bool Odobren { get; set; }

        [Required]
        [Column(TypeName = "BIT")]
        public bool NaCekanju { get; set; }
    }
}