using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace StomatoloskaOrdinacija.Web.ViewModels.Prijava
{
    public class RegistracijaViewModel
    {
        [StringLength(100)]
        [Required]
        public string Ime { get; set; }

        [StringLength(100)]
        [Required]
        public string Prezime { get; set; }

        [Required]
        [StringLength(320)]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$")]
        public string Email { get; set; }

        [StringLength(13,MinimumLength = 13,ErrorMessage = "JMBG mora da ima 13 brojeva!")]
        [Required]
        public string JMBG { get; set; }

        [Required]
        public DateTime DatumRodjenja { get; set; }

        [StringLength(30)]
        [Required]
        public string Mobitel { get; set; }

        [StringLength(200)]
        [Required]
        public string Adresa { get; set; }

        public List<SelectListItem> Gradovi { get; set; }

        [Required]
        public int GradID { get; set; }

        [Required]
        public string Spol { get; set; }

        
        public IFormFile Slika { get; set; }

        [Required]
        public bool AlergijaNaLijek { get; set; }

        [Required]
        public bool Proteza { get; set; }

        [Required]
        public bool Terapija { get; set; }

        [Required]
        public bool Navlake { get; set; }

        [Required]
        public bool Aparatic { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$")]
        public string Lozinka { get; set; }

        [Required]
        public string PotvrdaLozinke { get; set; }
    }
}
