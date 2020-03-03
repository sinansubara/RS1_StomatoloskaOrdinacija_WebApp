using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace StomatoloskaOrdinacija.Web.ViewModels.Korisnik
{
    public class KorisnikPrikazViewModel
    {
        public int KorisnikId { get; set; }

        [StringLength(100), MinLength(2)]
        public string Ime { get; set; }

        [StringLength(100),MinLength(3)]
        public string Prezime { get; set; }

        
        public string Email { get; set; }

       
        public string JMBG { get; set; }

        public string VrstaAcc { get; set; }
        
        public DateTime DatumRodjenja { get; set; }
        public string DatumRodjenjaString { get; set; }

        [StringLength(30)]
        public string Mobitel { get; set; }

        [StringLength(200)]
        public string Adresa { get; set; }

        public List<SelectListItem> Gradovi { get; set; }

        public int GradID { get; set; }
        public string Grad { get; set; }
        
        public string Spol { get; set; }


        public string Slika { get; set; }
        public IFormFile NovaSlika { get; set; }
        //pacijent
        public bool AlergijaNaLijek { get; set; }

        public bool Proteza { get; set; }

        public bool Terapija { get; set; }

        public bool Navlake { get; set; }

        public bool Aparatic { get; set; }
        //admin, stomatolog, osoblje
        public DateTime DatumZaposlenja { get; set; }
        public string DatumZaposlenjaString { get; set; }

        public string OpisPosla { get; set; }

        [Display(Name = "Broj žiro računa")]
        public string BrojZiroRacuna { get; set; }

        public bool Aktivan { get; set; }

        public int TitulaID { get; set; }
        public string Titula { get; set; }
        public List<SelectListItem> Titule { get; set; }
    }
}
