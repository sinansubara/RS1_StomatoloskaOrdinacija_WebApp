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

        [ForeignKey(nameof(Korisnik))]
        public int KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }

        [StringLength(100)]
        [Required]
        public string Titula { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.mm.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Datum zaposlenja")]
        public DateTime DatumZaposlenja { get; set; }
    }
}