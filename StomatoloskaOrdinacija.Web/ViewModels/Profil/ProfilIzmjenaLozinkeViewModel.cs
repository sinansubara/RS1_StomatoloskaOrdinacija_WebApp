﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StomatoloskaOrdinacija.Web.ViewModels.Profil
{
    public class ProfilIzmjenaLozinkeViewModel
    {
        [Required]
        public string StaraLozinka { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "Lozinka mora da posjeduje kombinaciju velikih i malih slova, brojeva i znakova, minimalno 8")]
        public string NovaLozinka { get; set; }

        [Required]
        public string PotvrdaNoveLozinke { get; set; }
    }
}
