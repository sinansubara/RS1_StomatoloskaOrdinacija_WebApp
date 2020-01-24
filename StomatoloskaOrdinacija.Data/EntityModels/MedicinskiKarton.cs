using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StomatoloskaOrdinacija.Data.EntityModels
{
    [Table("MedicinskiKarton")]
    public class MedicinskiKarton
    {
        [Key] 
        public int MedicinskiKartonId { get; set; }

        [ForeignKey(nameof(IzvrsenaUsluga))]
        public int IzvrsenaUslugaId { get; set; }
        public IzvrsenaUsluga IzvrsenaUsluga { get; set; }

        [ForeignKey(nameof(Terapija))]
        public int TerapijaId { get; set; }
        public Terapija Terapija { get; set; }

        [ForeignKey(nameof(Pacijent))]
        public int PacijentId { get; set; }
        public Pacijent Pacijent { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.mm.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Datum { get; set; }

        [Required]
        [StringLength(200)]
        public string Napomena { get; set; }
    }
}