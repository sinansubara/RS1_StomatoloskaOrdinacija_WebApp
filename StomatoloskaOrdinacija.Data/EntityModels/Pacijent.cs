using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StomatoloskaOrdinacija.Data.EntityModels
{
    [Table("Pacijenti")]
    public class Pacijent
    {
        [Key]
        public int PacijentId { get; set; }

        [ForeignKey(nameof(Korisnik))]
        public int KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.mm.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Datum rođenja")]
        public DateTime DatumRodjenja { get; set; }
    }
}