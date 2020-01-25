using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StomatoloskaOrdinacija.Data.EntityModels
{
    public class PromjenaLozinke
    {
        [Key]
        public int PromjenaLozinkeID { get; set; }

        [Required]
        [StringLength(30)]
        public string Vrijednost { get; set; }

        [ForeignKey(nameof(KorisnickiNalog))]
        public int KorisnickiNalogID { get; set; }
        public KorisnickiNalog KorisnickiNalog { get; set; }

        [Required]
        [Column(TypeName = "SMALLDATETIME")]
        [Display(Name = "Datum promjene")]
        public DateTime DatumPromjene { get; set; }
    }

}
