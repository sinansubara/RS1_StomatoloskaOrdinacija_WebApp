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
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.mm.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Datum { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Vrijeme { get; set; }

        [Required]
        [StringLength(200)]
        public string Razlog { get; set; }

        [Required]
        [Column(TypeName = "BIT")]
        public bool Hitan { get; set; }

        [Required]
        [Column(TypeName = "BIT")]
        public bool Odobren { get; set; }
    }
}