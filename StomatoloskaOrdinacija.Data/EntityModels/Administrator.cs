using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StomatoloskaOrdinacija.Data.EntityModels
{
    [Table("Administrator")]
    public class Administrator
    {
        [Key]
        public int AdministratorId { get; set; }

        [ForeignKey(nameof(KorisnickiNalog))]
        public int KorisnickiNalogId { get; set; }
        public KorisnickiNalog KorisnickiNalog { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.mm.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Datum zaposlenja")]
        public DateTime DatumZaposlenja { get; set; }

        [Required]
        [StringLength(200)]
        public string OpisPosla { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Broj žiro računa")]
        public string BrojZiroRacuna { get; set; }

        [Required]
        [Column(TypeName = "BIT")]
        public bool Aktivan { get; set; }

    }
}