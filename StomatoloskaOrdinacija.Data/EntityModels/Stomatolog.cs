using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StomatoloskaOrdinacija.Data.EntityModels
{
    [Table("Stomatolozi")]
    public class Stomatolog
    {
        [Key]
        public int StomatologId { get; set; }

        [ForeignKey(nameof(KorisnickiNalog))]
        public int KorisnickiNalogId { get; set; }
        public KorisnickiNalog KorisnickiNalog { get; set; }

        [ForeignKey(nameof(Titula))]
        public int TitulaID { get; set; }
        public Titula Titula { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.mm.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Datum zaposlenja")]
        public DateTime DatumZaposlenja { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Broj žiro računa")]
        public string BrojZiroRacuna { get; set; }

        [Required]
        [Column(TypeName = "BIT")]
        public bool Aktivan { get; set; }
    }
}