using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StomatoloskaOrdinacija.Data.EntityModels
{
    [Table("Korisnici")]
    public class Korisnik
    {
        [Key]
        public int KorisnikId { get; set; }

        [StringLength(100)]
        [Required]
        public string Ime { get; set; }

        [StringLength(100)]
        [Required]
        public string Prezime { get; set; }

        [StringLength(150)]
        [Required]
        public string Email { get; set; }

        [StringLength(13,MinimumLength = 13,ErrorMessage = "JMBG mora da ima 13 brojeva!")]
        [Required]
        public string JMBG { get; set; }

        [StringLength(30)]
        [Required]
        public string Mobitel { get; set; }

        [StringLength(200)]
        [Required]
        public string Adresa { get; set; }

        [StringLength(100)]
        [Required]
        public string KorisnickoIme { get; set; }

        [StringLength(100)]
        [Required]
        public string Lozinka { get; set; }

        [Display(Name = "Ime i prezime")]
        public string PrezimeIme
        {
            get
            {
                return Prezime + ", " + Ime;
            }
        }
    }
}
