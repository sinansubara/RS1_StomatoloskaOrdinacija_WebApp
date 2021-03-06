﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StomatoloskaOrdinacija.Web.ViewModels.Korisnik
{
    public class KorisnikPrikazPacijentiViewModel
    {
        public int PacijentId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Adresa { get; set; }
        public string Spol { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Grad { get; set; }
        public string Kontakt { get; set; }
        public string Jmbg { get; set; }
        public string Lokacija { get; set; }
    }
}
