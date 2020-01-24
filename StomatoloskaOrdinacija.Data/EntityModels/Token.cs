using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StomatoloskaOrdinacija.Data.EntityModels
{
    [Table("Tokeni")]
    public class Token
    {
        [Key]
        public int TokenId { get; set; }

        [Required]
        [StringLength(50)]
        public string Vrijednost { get; set; }

        [ForeignKey(nameof(KorisnickiNalog))]
        public int KorisnickiNalogId { get; set; }
        public KorisnickiNalog KorisnickiNalog { get; set; }

        [Required]
        [Column(TypeName = "SMALLDATETIME")]
        public DateTime Kreirano { get; set; }
    }

}