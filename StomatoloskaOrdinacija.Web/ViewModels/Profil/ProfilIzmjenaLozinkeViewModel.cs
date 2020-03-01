using System;
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
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "Lozinka mora sadržavati, veliko i malo slovo, broj i znak, minimalno 8 karaktera")]
        public string NovaLozinka { get; set; }

        [Required]
        public string PotvrdaNoveLozinke { get; set; }
    }
}
