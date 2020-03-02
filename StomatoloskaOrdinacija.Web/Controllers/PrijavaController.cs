using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.WebEncoders.Testing;
using StomatoloskaOrdinacija.Data;
using StomatoloskaOrdinacija.Data.EntityModels;
using StomatoloskaOrdinacija.Web.Helper;
using StomatoloskaOrdinacija.Web.ViewModels.Prijava;

namespace StomatoloskaOrdinacija.Web.Controllers
{
    public class PrijavaController : Controller
    {
        private MyContext _context;
        private IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PrijavaController(MyContext context, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Prijava()
        {
            if (HttpContext.GetLogiraniKorisnik() != null)
                return RedirectToAction("Pocetna", "Profil");

            return View();
        }

        [HttpPost]
        public IActionResult Prijava(PrijavaViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            
            var korisnickiNalog = _context.KorisnickiNalogs
                .Where(i => i.Email == model.Email)
                .AsEnumerable()
                .Where(i => i.LozinkaHash ==
                            PasswordSettings.GetHash(model.Lozinka, Convert.FromBase64String(i.LozinkaSalt)));
            
            if (korisnickiNalog == null || !korisnickiNalog.Any())
            {
                TempData["errorMessage"] = "Niste unijeli ispravne podatke za prijavu.";
                return View(model);
            }

            
            if ((korisnickiNalog.First().Permisije == 0 &&
                 _context.Administrators
                     .Where(i => i.KorisnickiNalogId == korisnickiNalog.First().KorisnickiNalogId).First().Aktivan) ||
                (korisnickiNalog.First().Permisije == 1 &&
                 _context.Stomatologs
                     .Where(i => i.KorisnickiNalogId == korisnickiNalog.First().KorisnickiNalogId).First().Aktivan) ||
                (korisnickiNalog.First().Permisije == 2 &&
                 _context.MedicinskoOsobljes
                     .Where(i => i.KorisnickiNalogId == korisnickiNalog.First().KorisnickiNalogId).First().Aktivan) ||
                korisnickiNalog.First().Permisije == 3)
            {
                HttpContext.SetLogiraniKorisnik(korisnickiNalog.First(), true);

                return RedirectToAction("Pocetna", "Profil");
            }

            TempData["errorMessage"] = "Niste unijeli ispravne podatke za prijavu.";
            return View(model);
        }

        public IActionResult Registracija()
        {
            if (HttpContext.GetLogiraniKorisnik() != null)
                return RedirectToAction("Pocetna", "Profil");

            var model = new RegistracijaViewModel
            {
                Gradovi = _context.Grads.Select
                    (i => new SelectListItem { Text = i.Naziv, Value = i.GradId.ToString() }).ToList()
            };

            return View(model);
        }


        [HttpPost]
        public IActionResult Registracija(RegistracijaViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Registracija");

            if (_context.KorisnickiNalogs.Any(i => i.Email == model.Email))
            {
                TempData["errorMessage"] = "Email adresa se koristi.";
                return RedirectToAction("Registracija");
            }

            byte[] lozinkaSalt = PasswordSettings.GetSalt();
            string lozinkaHash = PasswordSettings.GetHash(model.Lozinka, lozinkaSalt);


            string uniqueFileName = UploadedFile(model); 

            KorisnickiNalog korisnickiNalog = new KorisnickiNalog
            {
                Ime = model.Ime,
                Prezime = model.Prezime,
                Email = model.Email,
                LozinkaHash = lozinkaHash,
                LozinkaSalt = Convert.ToBase64String(lozinkaSalt),
                Permisije = 3,
                Kreirano = DateTime.Now,
                Mobitel = model.Mobitel,
                GradId = model.GradID,
                Adresa = model.Adresa,
                JMBG = model.JMBG,
                DatumRodjenja = model.DatumRodjenja,
                Spol = model.Spol,
                Slika = uniqueFileName
            };
            Pacijent pacijent = new Pacijent
            {
                KorisnickiNalog = korisnickiNalog,
                AlergijaNaLijek = model.AlergijaNaLijek,
                Aparatic = model.Aparatic,
                Navlake = model.Navlake,
                Proteza = model.Proteza,
                Terapija = model.Terapija
            };

            _context.KorisnickiNalogs.Add(korisnickiNalog);
            _context.Pacijents.Add(pacijent);

            _context.SaveChanges();

            TempData["successMessage"] = "Uspješno ste se registrovali.";
            return RedirectToAction("Prijava");
        }
        private string UploadedFile(RegistracijaViewModel model)  
        {  
            string uniqueFileName = "blank-profile.jpg";  
  
            if (model.Slika != null)  
            {  
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");  
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Slika.FileName;  
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);  
                using (var fileStream = new FileStream(filePath, FileMode.Create))  
                {  
                    model.Slika.CopyTo(fileStream);  
                }  
            }  
            return uniqueFileName;  
        }

        [ActionName("zaboravljena-lozinka")]
        public IActionResult ZaboravljenaLozinka()
        {
            if (HttpContext.GetLogiraniKorisnik() != null)
                return RedirectToAction("Pocetna", "Profil");

            return View("ZaboravljenaLozinka");
        }


        [HttpPost]
        [ActionName("zaboravljena-lozinka")]
        public IActionResult ZaboravljenaLozinka(ZaboravljenaLozinkaViewModel model)
        {
            if (!ModelState.IsValid)
                return View("ZaboravljenaLozinka", model);

            KorisnickiNalog korisnickiNalog = _context.KorisnickiNalogs.SingleOrDefault(i => i.Email == model.Email);

            if (korisnickiNalog == null)
            {
                TempData["errorMessage"] = "Email adresa se ne koristi.";
                return View("ZaboravljenaLozinka", model);
            }

            PromjenaLozinke promjenaLozinke = _context.PromjenaLozinkes.SingleOrDefault
                (i => i.KorisnickiNalogID == korisnickiNalog.KorisnickiNalogId);

            if (promjenaLozinke != null)
            {
                if ((DateTime.Now - promjenaLozinke.DatumPromjene).TotalHours < 24)
                {
                    TempData["errorMessage"] = "Email za promjenu lozinke je već poslan.";
                    return View("ZaboravljenaLozinka", model);
                }
                else
                {
                    _context.PromjenaLozinkes.Remove(promjenaLozinke);
                    _context.SaveChanges();
                }
            }

            string primalacPoruke = "";

            if (korisnickiNalog.Permisije == 0)
            {
                Administrator administrator = _context.Administrators.SingleOrDefault(i => i.KorisnickiNalogId == korisnickiNalog.KorisnickiNalogId);
                primalacPoruke = administrator.KorisnickiNalog.Ime + " " + administrator.KorisnickiNalog.Prezime;
            }

            if (korisnickiNalog.Permisije == 1)
            {
                Stomatolog stomatolog = _context.Stomatologs.SingleOrDefault(i => i.KorisnickiNalogId == korisnickiNalog.KorisnickiNalogId);
                primalacPoruke = stomatolog.KorisnickiNalog.Ime + " " + stomatolog.KorisnickiNalog.Prezime;
            }

            if (korisnickiNalog.Permisije == 2)
            {
                MedicinskoOsoblje medicinskoOsoblje = _context.MedicinskoOsobljes.SingleOrDefault(i => i.KorisnickiNalogId == korisnickiNalog.KorisnickiNalogId);
                primalacPoruke = medicinskoOsoblje.KorisnickiNalog.Ime + " " + medicinskoOsoblje.KorisnickiNalog.Prezime;
            }
            if (korisnickiNalog.Permisije == 3)
            {
                Pacijent pacijent = _context.Pacijents.SingleOrDefault(i => i.KorisnickiNalogId == korisnickiNalog.KorisnickiNalogId);
                primalacPoruke = pacijent.KorisnickiNalog.Ime + " " + pacijent.KorisnickiNalog.Prezime;
            }

            string vrijednost = RandomString.GetString(30);
            string link = 
                $"{ this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/prijava/promjena-lozinke?vrijednost=" + vrijednost;

            string poruka = "Posjetite sljedeći link kako biste promijenili lozinku: \n" + link +
                "\nUkoliko ne promijenite lozinku putem datog link-a u roku od 24 sata, link će " +
                "postati nevažeći.";

            EmailSettings.SendEmail(_configuration, primalacPoruke, korisnickiNalog.Email, "Promjena lozinke", poruka);

            PromjenaLozinke zahtjevZaPromjenomLozinke = new PromjenaLozinke
            {
                Vrijednost = vrijednost,
                KorisnickiNalogID = korisnickiNalog.KorisnickiNalogId,
                DatumPromjene = DateTime.Now
            };

            _context.PromjenaLozinkes.Add(zahtjevZaPromjenomLozinke);
            _context.SaveChanges();

            TempData["successMessage"] = "Email za promjenu lozinke uspješno poslan.";
            return RedirectToAction("zaboravljena-lozinka");
        }

        [ActionName("promjena-lozinke")]
        public IActionResult PromjenaLozinke(string vrijednost)
        {
            if (HttpContext.GetLogiraniKorisnik() != null)
                return RedirectToAction("Pocetna", "Profil");

            if (_context.PromjenaLozinkes.SingleOrDefault(i => i.Vrijednost == vrijednost) == null)
                return RedirectToAction("Prijava");

            TempData["vrijednost"] = vrijednost;
            return View("PromjenaLozinke");
        }

        [HttpPost]
        [ActionName("promjena-lozinke")]
        public IActionResult PromjenaLozinke(PromjenaLozinkeViewModel model)
        {
            if (!ModelState.IsValid)
                return View("PromjenaLozinke", model);

            string vrijednost = (string)TempData["vrijednost"];

            PromjenaLozinke promjenaLozinke = _context.PromjenaLozinkes.SingleOrDefault(i => i.Vrijednost == vrijednost);

            KorisnickiNalog korisnickiNalog = _context.KorisnickiNalogs.SingleOrDefault
                (i => i.KorisnickiNalogId == promjenaLozinke.KorisnickiNalogID);

            korisnickiNalog.LozinkaHash = PasswordSettings.GetHash(model.NovaLozinka, Convert.FromBase64String(korisnickiNalog.LozinkaSalt));

            _context.PromjenaLozinkes.Remove(promjenaLozinke);
            _context.SaveChanges();

            TempData["successMessage"] = "Uspješno ste promijenili lozinku.";
            return RedirectToAction("Prijava");
        }
    }
}