using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace StomatoloskaOrdinacija.Web.ViewModels.Profil
{
    public class UrediProfilViewModel
    {
        [StringLength(100), MinLength(2)]
        [Required]
        public string Ime { get; set; }

        [StringLength(100),MinLength(3)]
        [Required]
        public string Prezime { get; set; }

        
        public string Email { get; set; }

       
        public string JMBG { get; set; }

        
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

    }
}
