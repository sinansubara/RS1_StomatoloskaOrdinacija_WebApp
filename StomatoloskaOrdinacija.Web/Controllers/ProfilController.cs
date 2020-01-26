using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StomatoloskaOrdinacija.Data;
using StomatoloskaOrdinacija.Data.EntityModels;
using StomatoloskaOrdinacija.Web.Helper;
using StomatoloskaOrdinacija.Web.ViewModels.Profil;

namespace StomatoloskaOrdinacija.Web.Controllers
{
    public class ProfilController : Controller
    {
        private MyContext _context;

        public ProfilController(MyContext context)
        {
            _context = context;
        }

        [Autorizacija(true,true,true,true)]
        public IActionResult Pocetna()
        {
            KorisnickiNalog logiraniKorisnik = HttpContext.GetLogiraniKorisnik();
            if (logiraniKorisnik.Permisije == 0)
                TempData["Layout"] = "_Administrator";

            if (logiraniKorisnik.Permisije == 1)
                TempData["Layout"] = "_Stomatolog";

            if (logiraniKorisnik.Permisije == 2)
                TempData["Layout"] = "_MedicinskoOsoblje";

            if (logiraniKorisnik.Permisije == 3)
            {
                TempData["pacijentId"] = _context.Pacijents.Where(i => i.KorisnickiNalogId == HttpContext.GetLogiraniKorisnik().KorisnickiNalogId).FirstOrDefault().PacijentId;
                TempData["Layout"] = "_Pacijent";
            }

            //TempData[""] = _context..Count();
            //TempData[""] = _context..Count();
            //TempData[""] = _context..Count();

            return View();
        }


        [Autorizacija(true,true,true,true)]
        [ActionName("pregled-profila")]
        public IActionResult PregledProfila()
        {
            KorisnickiNalog logiraniKorisnik = HttpContext.GetLogiraniKorisnik();
            dynamic korisnik = null;


            //moze stvarat probleme sintaksa korisnik = ... treba debug
            if (logiraniKorisnik.Permisije == 0)
            {
                korisnik = _context.Administrators
                    .Include(i => i.KorisnickiNalog)
                    .ThenInclude(i => i.Grad)
                    .SingleOrDefault(i => i.KorisnickiNalog == HttpContext.GetLogiraniKorisnik());
                
                TempData["Layout"] = "_Administrator";
            }

            if (logiraniKorisnik.Permisije == 1)
            {
                korisnik = _context.Stomatologs
                    .Include(i => i.KorisnickiNalog)
                    .ThenInclude(i => i.Grad)
                    .SingleOrDefault(i => i.KorisnickiNalog == HttpContext.GetLogiraniKorisnik());

                TempData["Layout"] = "_Stomatolog";
            }
            if (logiraniKorisnik.Permisije == 2)
            {
                korisnik = _context.MedicinskoOsobljes
                    .Include(i => i.KorisnickiNalog)
                    .ThenInclude(i => i.Grad)
                    .SingleOrDefault(i => i.KorisnickiNalog == HttpContext.GetLogiraniKorisnik());
                
                TempData["Layout"] = "_MedicinskoOsoblje";
            }

            if (logiraniKorisnik.Permisije == 3)
            {
                korisnik = _context.Pacijents
                    .Include(i => i.KorisnickiNalog)
                    .ThenInclude(i => i.Grad)
                    .SingleOrDefault(i => i.KorisnickiNalog == HttpContext.GetLogiraniKorisnik());

                TempData["pacijentId"] = _context.Pacijents.Where(i => i.KorisnickiNalogId == HttpContext.GetLogiraniKorisnik().KorisnickiNalogId).FirstOrDefault().PacijentId;
                TempData["Layout"] = "_Pacijent";
            }
            var model = new UrediProfilViewModel
            {
                Ime = korisnik.Ime,
                Prezime = korisnik.Prezime,
                Email = korisnik.Email,
                JMBG = korisnik.JMBG,
                DatumRodjenja = korisnik.DatumRodjenja,
                Mobitel = korisnik.Mobitel,
                Adresa = korisnik.Adresa,
                GradID = korisnik.GradID,
                Spol = korisnik.Spol,
                Slika = korisnik.Slika,
                AlergijaNaLijek = korisnik.AlergijaNaLijek,
                Proteza = korisnik.Proteza,
                Terapija = korisnik.Terapija,
                Navlake = korisnik.Navlake,
                Aparatic = korisnik.Aparatic
            };

            return View("PregledProfila",model);
        }


        [HttpPost]
        [ActionName("pregled-profila")]
        public IActionResult PregledProfila(UrediProfilViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("pregled-profila");

            KorisnickiNalog logiraniKorisnik = HttpContext.GetLogiraniKorisnik();

            dynamic korisnik = null;

            if (logiraniKorisnik.Permisije == 0)
                korisnik = _context.Administrators.SingleOrDefault(i => i.KorisnickiNalogId == logiraniKorisnik.KorisnickiNalogId);

            if (logiraniKorisnik.Permisije == 1)
                korisnik = _context.Stomatologs.SingleOrDefault(i => i.KorisnickiNalogId == logiraniKorisnik.KorisnickiNalogId);

            if (logiraniKorisnik.Permisije == 2)
                korisnik = _context.MedicinskoOsobljes.SingleOrDefault(i => i.KorisnickiNalogId == logiraniKorisnik.KorisnickiNalogId);

            if (logiraniKorisnik.Permisije == 3)
            {
                TempData["korisnikId"] = _context.Pacijents.Where(i => i.KorisnickiNalogId == HttpContext.GetLogiraniKorisnik().KorisnickiNalogId).FirstOrDefault().PacijentId;
                korisnik = _context.Pacijents.SingleOrDefault(i => i.KorisnickiNalogId == logiraniKorisnik.KorisnickiNalogId);
            }

            if (korisnik.Ime != model.Ime)
            {
                korisnik.Ime = model.Ime;
                _context.SaveChanges();

                TempData["successMessage"] = "Ime uspješno promjenuto.";
            }

            if (korisnik.Prezime != model.Prezime)
            {
                korisnik.Prezime = model.Prezime;
                _context.SaveChanges();

                TempData["successMessage"] = "Prezime uspješno promjenuto.";
            }

            if (korisnik.Mobitel != model.Mobitel)
            {
                korisnik.Mobitel = model.Mobitel;
                _context.SaveChanges();

                TempData["successMessage"] = "Broj mobitela uspješno promjenut.";
            }

            if (korisnik.Adresa != model.Adresa)
            {
                korisnik.Adresa = model.Adresa;
                _context.SaveChanges();

                TempData["successMessage"] = "Adresa uspješno promjenuta.";
            }

            if (korisnik.GradID != model.GradID)
            {
                korisnik.GradID = model.GradID;
                _context.SaveChanges();

                TempData["successMessage"] = "Grad uspješno promjenut.";
            }

            if (korisnik.Slika != model.Slika)
            {
                korisnik.Slika = model.Slika;
                _context.SaveChanges();

                TempData["successMessage"] = "Slika uspješno promjenuta.";
            }
            if (korisnik.AlergijaNaLijek != model.AlergijaNaLijek)
            {
                korisnik.AlergijaNaLijek = model.AlergijaNaLijek;
                _context.SaveChanges();

                TempData["successMessage"] = "Promjena uspješno sačuvana.";
            }
            if (korisnik.Proteza != model.Proteza)
            {
                korisnik.Proteza = model.Proteza;
                _context.SaveChanges();

                TempData["successMessage"] = "Promjena uspješno sačuvana.";
            }
            if (korisnik.Terapija != model.Terapija)
            {
                korisnik.Terapija = model.Terapija;
                _context.SaveChanges();

                TempData["successMessage"] = "Slika uspješno promjenuta.";
            }
            if (korisnik.Navlake != model.Navlake)
            {
                korisnik.Navlake = model.Navlake;
                _context.SaveChanges();

                TempData["successMessage"] = "Slika uspješno promjenuta.";
            }
            if (korisnik.Aparatic != model.Aparatic)
            {
                korisnik.Aparatic = model.Aparatic;
                _context.SaveChanges();

                TempData["successMessage"] = "Slika uspješno promjenuta.";
            }

            return RedirectToAction("pregled-profila");
        }


        [ActionName("izmjena-lozinke")]
        [Autorizacija(true, true, true,true)]
        public IActionResult IzmjenaLozinke()
        {
            KorisnickiNalog logiraniKorisnik = HttpContext.GetLogiraniKorisnik();

            if (logiraniKorisnik.Permisije == 0)
                TempData["Layout"] = "_Administrator";

            if (logiraniKorisnik.Permisije == 1)
                TempData["Layout"] = "_Stomatolog";

            if (logiraniKorisnik.Permisije == 2)
                TempData["Layout"] = "_MedicinskoOsoblje";

            if (logiraniKorisnik.Permisije == 3)
            {
                TempData["pacijentId"] = _context.Pacijents.Where(i => i.KorisnickiNalogId == HttpContext.GetLogiraniKorisnik().KorisnickiNalogId).FirstOrDefault().PacijentId;
                TempData["Layout"] = "_Pacijent";
            }
            return View("IzmjenaLozinke");
        }


        [HttpPost]
        [ActionName("izmjena-lozinke")]
        public IActionResult PromijeniLozinku(PromjeniLozinkuViewModel model)
        {
            if (!ModelState.IsValid)
                return View("IzmjenaLozinke", model);

            KorisnickiNalog logiraniKorisnik = HttpContext.GetLogiraniKorisnik();

            if (PasswordSettings.GetHash(model.TrenutnaLozinka, Convert.FromBase64String(logiraniKorisnik.LozinkaSalt))
                != logiraniKorisnik.LozinkaHash)
            {
                TempData["errorMessage"] = "Netačno unesena trenutna lozinka.";
                return RedirectToAction("izmjena-lozinke");
            }

            if (PasswordSettings.GetHash(model.NovaLozinka, Convert.FromBase64String(logiraniKorisnik.LozinkaSalt))
                == logiraniKorisnik.LozinkaHash)
            {
                TempData["errorMessage"] = "Nova lozinka se treba razlikovati od trenutne.";
                return RedirectToAction("izmjena-lozinke");
            }

            logiraniKorisnik.LozinkaHash = PasswordSettings.GetHash(model.NovaLozinka, Convert.FromBase64String(logiraniKorisnik.LozinkaSalt));

            _context.SaveChanges();

            TempData["successMessage"] = "Uspješno ste promijenili lozinku.";
            return RedirectToAction("pregled-profila");
        }


        public IActionResult Odjava()
        {
            KorisnickiNalog logiraniKorisnik = HttpContext.GetLogiraniKorisnik();

            if (logiraniKorisnik == null)
                return RedirectToAction("Prijava", "Prijava");

            string trenutniToken = HttpContext.GetTrenutniToken();

            Token token = _context.Tokens.Where(x =>
                x.KorisnickiNalogId == logiraniKorisnik.KorisnickiNalogId && x.Vrijednost == trenutniToken).First();
                

            _context.Tokens.Remove(token);

            var tokeni = _context.Tokens.Where(x => x.KorisnickiNalogId == logiraniKorisnik.KorisnickiNalogId)
                .AsEnumerable().Where(x => (DateTime.Now - x.Kreirano).TotalHours >= 24).ToList();

            foreach (Token t in tokeni)
            {
                _context.Tokens.Remove(t);
            }

            _context.SaveChanges();

            Response.Cookies.Delete("logiraniKorisnik");

            return RedirectToAction("Prijava", "Prijava");
        }
    }
}