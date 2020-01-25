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

        [ForeignKey(nameof(KorisnickiNalog))]
        public int KorisnickiNalogId { get; set; }
        public KorisnickiNalog KorisnickiNalog { get; set; }

        [Required]
        [Column(TypeName = "BIT")]
        [Display(Name = "Alergija na lijek")]
        public bool AlergijaNaLijek { get; set; }

        [Required]
        [Column(TypeName = "BIT")]
        public bool Proteza { get; set; }

        [Required]
        [Column(TypeName = "BIT")]
        public bool Terapija { get; set; }

        [Required]
        [Column(TypeName = "BIT")]
        public bool Navlake { get; set; }

        [Required]
        [Column(TypeName = "BIT")]
        public bool Aparatic { get; set; }

    }
}