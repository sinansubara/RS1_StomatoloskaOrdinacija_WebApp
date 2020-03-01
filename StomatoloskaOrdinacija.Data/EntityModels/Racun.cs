using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StomatoloskaOrdinacija.Data.EntityModels
{
    [Table("Racun")]
    public class Racun
    {
        [Key]
        public int RacunId { get; set; }

        [ForeignKey(nameof(IzvrsenaUsluga))]
        public int IzvrsenaUslugaId { get; set; }
        public IzvrsenaUsluga IzvrsenaUsluga { get; set; }

        [ForeignKey(nameof(MedicinskoOsoblje))]
        public int MedicinskoOsobljeId { get; set; }
        public MedicinskoOsoblje MedicinskoOsoblje { get; set; }

        [ForeignKey(nameof(Pacijent))]
        public int PacijentId { get; set; }
        public Pacijent Pacijent { get; set; }


        [Required]
        [Column(TypeName = "SMALLDATETIME")]
        [Display(Name = "Datum izdavanja racuna")]
        public DateTime DatumIzdavanjaRacuna { get; set; }

        [Required]
        [Column(TypeName = "BIT")]
        public bool Uplaceno { get; set; }
    }
}