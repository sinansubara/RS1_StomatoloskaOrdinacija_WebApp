﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace StomatoloskaOrdinacija.Web.ViewModels.Korisnik
{
    public class KorisnikDodajStomatologViewModel
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
        [Range(1, int.MaxValue, ErrorMessage = "Molimo vas da odaberete grad")]
        public int GradID { get; set; }

        [Required]
        public string Spol { get; set; }

        
        public IFormFile Slika { get; set; }



        [Required]
        public DateTime DatumZaposlenja { get; set; }

        [Required]
        public bool Aktivan { get; set; }

        [Required]
        [StringLength(20)]
        public string BrojZiroRacuna { get; set; }

        public List<SelectListItem> Titule { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Molimo vas da odaberete titulu")]
        public int TitulaID { get; set; }

        
    }
}
