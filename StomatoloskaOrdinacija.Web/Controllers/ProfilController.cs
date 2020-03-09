using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProfilController(MyContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
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

            var model = new ProfilPocetnaViewModel
            {
                Ime = logiraniKorisnik.Ime,
                Prezime = logiraniKorisnik.Prezime
            };
            var imepre = model.Ime + " " + model.Prezime;

            HttpContext.Response.SetCookieJson("imeprezime",imepre);
            
            return View(model);
        }


        [Autorizacija(true,true,true,true)]
        [ActionName("pregled-profila")]
        public IActionResult PregledProfila()
        {
            KorisnickiNalog logiraniKorisnik = HttpContext.GetLogiraniKorisnik();
            dynamic korisnik = null;


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
                korisnik = _context.Stomatologs.Include(i=>i.Titula)
                    .Include(i => i.KorisnickiNalog)
                    .ThenInclude(i => i.Grad)
                    .SingleOrDefault(i => i.KorisnickiNalog == HttpContext.GetLogiraniKorisnik());

                TempData["Layout"] = "_Stomatolog";
            }
            if (logiraniKorisnik.Permisije == 2)
            {
                korisnik = _context.MedicinskoOsobljes.Include(i=>i.Titula)
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

            

            var model = new UrediProfilViewModel //za sve usere
            {
                Ime = korisnik.KorisnickiNalog.Ime,
                Prezime = korisnik.KorisnickiNalog.Prezime,
                Email = korisnik.KorisnickiNalog.Email,
                JMBG = korisnik.KorisnickiNalog.JMBG,
                DatumRodjenjaString = korisnik.KorisnickiNalog.DatumRodjenja.ToString("dd.MM.yyyy"),
                Mobitel = korisnik.KorisnickiNalog.Mobitel,
                Adresa = korisnik.KorisnickiNalog.Adresa,
                GradID = korisnik.KorisnickiNalog.GradId,
                Grad = korisnik.KorisnickiNalog.Grad.Naziv,
                Gradovi = _context.Grads.Select
                    (i => new SelectListItem { Text = i.Naziv, Value = i.GradId.ToString() }).ToList(),
                Spol = korisnik.KorisnickiNalog.Spol,
                Slika = korisnik.KorisnickiNalog.Slika
            };
            if (logiraniKorisnik.Permisije == 0)//admin
            {
                model.DatumZaposlenjaString = korisnik.DatumZaposlenja.ToString("dd.MM.yyyy");
                model.BrojZiroRacuna = korisnik.BrojZiroRacuna;
                model.OpisPosla = korisnik.OpisPosla;
                model.Aktivan = korisnik.Aktivan;
                model.VrstaAcc = "Administrator";
            }
            if (logiraniKorisnik.Permisije == 1)//stomatolog
            {
                model.DatumZaposlenjaString = korisnik.DatumZaposlenja.ToString("dd.MM.yyyy");
                model.BrojZiroRacuna = korisnik.BrojZiroRacuna;
                model.Aktivan = korisnik.Aktivan;
                model.TitulaID = korisnik.TitulaID;
                model.Titula = korisnik.Titula.Naziv;
                model.VrstaAcc = "Stomatolog";
                model.Titule = _context.Titulas.Select
                    (i => new SelectListItem {Text = i.Naziv, Value = i.TitulaId.ToString()}).ToList();
            }
            if (logiraniKorisnik.Permisije == 2)//med.osoblje
            {
                model.DatumZaposlenjaString = korisnik.DatumZaposlenja.ToString("dd.MM.yyyy");
                model.BrojZiroRacuna = korisnik.BrojZiroRacuna;
                model.OpisPosla = korisnik.OpisPosla;
                model.Aktivan = korisnik.Aktivan;
                model.TitulaID = korisnik.TitulaID;
                model.Titula = korisnik.Titula.Naziv;
                model.VrstaAcc = "Medicinsko osoblje";
                model.Titule = _context.Titulas.Select
                    (i => new SelectListItem {Text = i.Naziv, Value = i.TitulaId.ToString()}).ToList();
            }
            if (logiraniKorisnik.Permisije == 3)//pacijent
            {
                model.AlergijaNaLijek = korisnik.AlergijaNaLijek;
                model.Aparatic = korisnik.Aparatic;
                model.Navlake = korisnik.Navlake;
                model.Proteza = korisnik.Proteza;
                model.Terapija = korisnik.Terapija;
                model.VrstaAcc = "Pacijent";
            }
            return View("PregledProfila", model);
        }

        [HttpPost]
        [ActionName("snimi-izmjene")]
        [DisableRequestSizeLimit]
        public IActionResult SnimiIzmjene(UrediProfilViewModel model)
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
                korisnik = _context.Pacijents.SingleOrDefault(i => i.KorisnickiNalogId == logiraniKorisnik.KorisnickiNalogId);
            }

            if (korisnik.KorisnickiNalog.Ime != model.Ime)
            {
                korisnik.KorisnickiNalog.Ime = model.Ime;
                _context.SaveChanges();

                TempData["successMessage"] = "Ime uspješno promjenuto.";
            }

            if (korisnik.KorisnickiNalog.Prezime != model.Prezime)
            {
                korisnik.KorisnickiNalog.Prezime = model.Prezime;
                _context.SaveChanges();

                TempData["successMessage"] = "Prezime uspješno promjenuto.";
            }

            if (korisnik.KorisnickiNalog.Mobitel != model.Mobitel)
            {
                korisnik.KorisnickiNalog.Mobitel = model.Mobitel;
                _context.SaveChanges();

                TempData["successMessage"] = "Broj mobitela uspješno promjenut.";
            }

            if (korisnik.KorisnickiNalog.Adresa != model.Adresa)
            {
                korisnik.KorisnickiNalog.Adresa = model.Adresa;
                _context.SaveChanges();

                TempData["successMessage"] = "Adresa uspješno promjenuta.";
            }

            if (korisnik.KorisnickiNalog.GradId != model.GradID)
            {
                korisnik.KorisnickiNalog.GradId = model.GradID;
                _context.SaveChanges();

                TempData["successMessage"] = "Grad uspješno promjenut.";
            }
            string uniqueFileName = UploadedFile(model);
            if (uniqueFileName != null)
            {
                string _imageToBeDeleted = Path.Combine(_webHostEnvironment.WebRootPath, "images", korisnik.KorisnickiNalog.Slika);

                if(System.IO.File.Exists(_imageToBeDeleted))
                {
                    if (korisnik.KorisnickiNalog.Slika != "blank-profile.jpg")
                    {
                        System.IO.File.Delete(_imageToBeDeleted);
                    }
                }

                korisnik.KorisnickiNalog.Slika = uniqueFileName;
                _context.SaveChanges();

                TempData["successMessage"] = "Slika uspješno promjenuta.";
            }

            if (logiraniKorisnik.Permisije == 3)
            {
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
            }
            else
            {
                if (logiraniKorisnik.Permisije == 0 || logiraniKorisnik.Permisije == 2)
                {
                    if (korisnik.OpisPosla != model.OpisPosla)
                    {
                        korisnik.OpisPosla = model.OpisPosla;
                        _context.SaveChanges();

                        TempData["successMessage"] = "Opis posla uspješno promjenut.";
                    }
                }
                if (korisnik.BrojZiroRacuna != model.BrojZiroRacuna)
                {
                    korisnik.BrojZiroRacuna = model.BrojZiroRacuna;
                    _context.SaveChanges();

                    TempData["successMessage"] = "Broj žiro računa uspješno promjenut.";
                }
                if (korisnik.Aktivan != model.Aktivan)
                {
                    korisnik.Aktivan = model.Aktivan;
                    _context.SaveChanges();

                    TempData["successMessage"] = "Aktivnost korisnika uspješno promjenuta";
                }

                if (logiraniKorisnik.Permisije == 1 || logiraniKorisnik.Permisije == 2)
                {
                    if (korisnik.TitulaID != model.TitulaID)
                    {
                        korisnik.TitulaID = model.TitulaID;
                        _context.SaveChanges();

                        TempData["successMessage"] = "Titula uspješno promjenuta.";
                    }
                }

            }
            

            return RedirectToAction("pregled-profila");
        }
        private string UploadedFile(UrediProfilViewModel model)  
        {  
            string uniqueFileName = null;  
  
            if (model.NovaSlika != null) 
            {  
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");  
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.NovaSlika.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);  
                using (var fileStream = new FileStream(filePath, FileMode.Create))  
                {  
                    model.NovaSlika.CopyTo(fileStream);  
                }  
            }  
            return uniqueFileName;  
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
                TempData["Layout"] = "_Pacijent";
            }
            return View("IzmjenaLozinke");
        }


        [HttpPost]
        [ActionName("izmjena-lozinke")]
        public IActionResult PromijeniLozinku(ProfilIzmjenaLozinkeViewModel model)
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
                TempData["Layout"] = "_Pacijent";
            }

            if (!ModelState.IsValid)
                return View("IzmjenaLozinke", model);


            if (PasswordSettings.GetHash(model.StaraLozinka, Convert.FromBase64String(logiraniKorisnik.LozinkaSalt))
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

            if (model.NovaLozinka != model.PotvrdaNoveLozinke)
            {
                TempData["errorMessage"] = "Niste potvrdili vašu novu lozinku.";
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
