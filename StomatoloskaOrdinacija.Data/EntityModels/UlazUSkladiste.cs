using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StomatoloskaOrdinacija.Data.EntityModels
{
    [Table("UlazUSkladiste")]
    public class UlazUSkladiste
    {
        [Key]
        public int UlazUSkladisteID { get; set; }

        [ForeignKey(nameof(MedicinskoOsoblje))]
        public int MedicinskoOsobljeId { get; set; }
        public MedicinskoOsoblje MedicinskoOsoblje { get; set; }


        [Required]
        [StringLength(50)]
        public string BrojFakture { get; set; }

        [Required]
        [Column(TypeName = "SMALLDATETIME")]
        public DateTime Datum { get; set; }

        [Required]
        [Column(TypeName = "DECIMAL(18,2)")]
        public decimal IznosRacuna { get; set; }

        [Required]
        [Column(TypeName = "DECIMAL(18,2)")]
        public decimal PDV { get; set; }
    }
}