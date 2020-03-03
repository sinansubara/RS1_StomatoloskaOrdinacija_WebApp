using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StomatoloskaOrdinacija.Data;
using StomatoloskaOrdinacija.Data.EntityModels;
using StomatoloskaOrdinacija.Web.Helper;
using StomatoloskaOrdinacija.Web.ViewModels.Korisnik;
using StomatoloskaOrdinacija.Web.ViewModels.Prijava;
using StomatoloskaOrdinacija.Web.ViewModels.Profil;

namespace StomatoloskaOrdinacija.Web.Controllers
{
    public class KorisnikController : Controller
    {
        private MyContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public KorisnikController(MyContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [Autorizacija(true,true,true,false)]
        public IActionResult Index()
        {
            return RedirectToAction("uredi-pacijent");
        }
        
        [Autorizacija(true,true,true,false)]
        public IActionResult ListaPacijenata()
        {
            var lista = _context.Pacijents.Select(i => new KorisnikPrikazPacijentiViewModel
            {
                PacijentId = i.PacijentId,
                Ime = _context.KorisnickiNalogs.Where(x=>x.KorisnickiNalogId == i.KorisnickiNalogId).Select(x=>x.Ime).FirstOrDefault(),
                Prezime = _context.KorisnickiNalogs.Where(x=>x.KorisnickiNalogId == i.KorisnickiNalogId).Select(x=>x.Prezime).FirstOrDefault(),
                Adresa = _context.KorisnickiNalogs.Where(x=>x.KorisnickiNalogId == i.KorisnickiNalogId).Select(x=>x.Adresa).FirstOrDefault(),
                Spol = _context.KorisnickiNalogs.Where(x=>x.KorisnickiNalogId == i.KorisnickiNalogId).Select(x=>x.Spol).FirstOrDefault(),
                DatumRodjenja = _context.KorisnickiNalogs.Where(x=>x.KorisnickiNalogId == i.KorisnickiNalogId).Select(x=>x.DatumRodjenja).FirstOrDefault(),
                Grad = _context.Grads.Where(x=>x.GradId == _context.KorisnickiNalogs.Where(y=>y.KorisnickiNalogId == i.KorisnickiNalogId).Select(y=>y.GradId).FirstOrDefault()).Select(x=>x.Naziv).FirstOrDefault()
            }).ToList();

            return Json(new {data = lista});
        }
        [Autorizacija(true,true,true,false)]
        public IActionResult ListaOsoblja()
        {
            var lista = _context.MedicinskoOsobljes.Select(i => new KorisnikPrikazOsobljeViewModel
            {
                MedicinskoOsobljeId = i.MedicinskoOsoboljeId,
                Ime = _context.KorisnickiNalogs.Where(x=>x.KorisnickiNalogId == i.KorisnickiNalogId).Select(x=>x.Ime).FirstOrDefault(),
                Prezime = _context.KorisnickiNalogs.Where(x=>x.KorisnickiNalogId == i.KorisnickiNalogId).Select(x=>x.Prezime).FirstOrDefault(),
                Adresa = _context.KorisnickiNalogs.Where(x=>x.KorisnickiNalogId == i.KorisnickiNalogId).Select(x=>x.Adresa).FirstOrDefault(),
                Spol = _context.KorisnickiNalogs.Where(x=>x.KorisnickiNalogId == i.KorisnickiNalogId).Select(x=>x.Spol).FirstOrDefault(),
                DatumZaposlenja = _context.MedicinskoOsobljes.Where(x=>x.KorisnickiNalogId == i.KorisnickiNalogId).Select(x=>x.DatumZaposlenja).FirstOrDefault(),
                Aktivan = _context.MedicinskoOsobljes.Where(x=>x.KorisnickiNalogId == i.KorisnickiNalogId).Select(x=>x.Aktivan).FirstOrDefault()?"Da":"Ne",
                Grad = _context.Grads.Where(x=>x.GradId == _context.KorisnickiNalogs.Where(y=>y.KorisnickiNalogId == i.KorisnickiNalogId).Select(y=>y.GradId).FirstOrDefault()).Select(x=>x.Naziv).FirstOrDefault()
            }).ToList();

            return Json(new {data = lista});
        }
        [Autorizacija(true,true,true,false)]
        public IActionResult ListaStomatologa()
        {
            var lista = _context.Stomatologs.Select(i => new KorisnikPrikazStomatologaViewModel
            {
                StomatologId = i.StomatologId,
                Ime = _context.KorisnickiNalogs.Where(x=>x.KorisnickiNalogId == i.KorisnickiNalogId).Select(x=>x.Ime).FirstOrDefault(),
                Prezime = _context.KorisnickiNalogs.Where(x=>x.KorisnickiNalogId == i.KorisnickiNalogId).Select(x=>x.Prezime).FirstOrDefault(),
                Adresa = _context.KorisnickiNalogs.Where(x=>x.KorisnickiNalogId == i.KorisnickiNalogId).Select(x=>x.Adresa).FirstOrDefault(),
                Spol = _context.KorisnickiNalogs.Where(x=>x.KorisnickiNalogId == i.KorisnickiNalogId).Select(x=>x.Spol).FirstOrDefault(),
                DatumZaposlenja = _context.Stomatologs.Where(x=>x.KorisnickiNalogId == i.KorisnickiNalogId).Select(x=>x.DatumZaposlenja).FirstOrDefault(),
                Aktivan = _context.Stomatologs.Where(x=>x.KorisnickiNalogId == i.KorisnickiNalogId).Select(x=>x.Aktivan).FirstOrDefault()?"Da":"Ne",
                Grad = _context.Grads.Where(x=>x.GradId == _context.KorisnickiNalogs.Where(y=>y.KorisnickiNalogId == i.KorisnickiNalogId).Select(y=>y.GradId).FirstOrDefault()).Select(x=>x.Naziv).FirstOrDefault()
            }).ToList();

            return Json(new {data = lista});
        }
        
        [Autorizacija(true,true,true,false)]
        [ActionName("uredi-pacijent")]
        public IActionResult UrediPacijent(int id = -1)
        {
            KorisnickiNalog logiraniKorisnik = HttpContext.GetLogiraniKorisnik();
            if (logiraniKorisnik.Permisije == 0)
                TempData["Layout"] = "_Administrator";

            if (logiraniKorisnik.Permisije == 1)
                TempData["Layout"] = "_Stomatolog";

            if (logiraniKorisnik.Permisije == 2)
                TempData["Layout"] = "_MedicinskoOsoblje";

            if (id != -1)
            {
                dynamic korisnik = _context.Pacijents
                    .Include(i => i.KorisnickiNalog)
                    .ThenInclude(i => i.Grad)
                    .SingleOrDefault(i => i.PacijentId == id);


                var model = new KorisnikPrikazViewModel 
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
                    Slika = korisnik.KorisnickiNalog.Slika,
                    AlergijaNaLijek = korisnik.AlergijaNaLijek,
                    Aparatic = korisnik.Aparatic,
                    Navlake = korisnik.Navlake,
                    Proteza = korisnik.Proteza,
                    Terapija = korisnik.Terapija,
                    VrstaAcc = "Pacijent",
                    KorisnikId = id
                };
                return View("UrediKorisnike", model);
            }
            return View("UrediPacijent");
        }

        [Autorizacija(true, true, true, false)]
        [ActionName("dodaj-pacijenta")]
        public IActionResult DodajPacijenta()
        {
            var model = new KorisnikDodajPacijentaViewModel
            {
                Gradovi = _context.Grads.Select
                    (i => new SelectListItem { Text = i.Naziv, Value = i.GradId.ToString() }).ToList()
            };

            KorisnickiNalog logiraniKorisnik = HttpContext.GetLogiraniKorisnik();
            if (logiraniKorisnik.Permisije == 0)
                TempData["Layout"] = "_Administrator";

            if (logiraniKorisnik.Permisije == 1)
                TempData["Layout"] = "_Stomatolog";

            if (logiraniKorisnik.Permisije == 2)
                TempData["Layout"] = "_MedicinskoOsoblje";

            return View("DodajPacijenta", model);
        }

        [Autorizacija(true, true, true, false)]
        [ActionName("dodaj-pacijenta")]
        [HttpPost]
        public IActionResult DodajPacijenta(KorisnikDodajPacijentaViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("dodaj-pacijenta");

            if (_context.KorisnickiNalogs.Any(i => i.Email == model.Email))
            {
                TempData["errorMessage"] = "Email adresa se koristi.";
                return RedirectToAction("dodaj-pacijenta");
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

            TempData["successMessage"] = "Uspješno ste dodali novog pacijenta.";

            return RedirectToAction("uredi-pacijent");
        }

        [Autorizacija(true, false, false, false)]
        [ActionName("uredi-stomatolog")]
        public IActionResult UrediStomatolog(int id = -1)
        {
            if (id != -1)
            {
                dynamic korisnik = _context.Stomatologs.Include(i => i.Titula)
                    .Include(i => i.KorisnickiNalog)
                    .ThenInclude(i => i.Grad)
                    .SingleOrDefault(i => i.StomatologId == id);

                TempData["Layout"] = "_Administrator";


                var model = new KorisnikPrikazViewModel
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
                    Slika = korisnik.KorisnickiNalog.Slika,
                    DatumZaposlenjaString = korisnik.DatumZaposlenja.ToString("dd.MM.yyyy"),
                    BrojZiroRacuna = korisnik.BrojZiroRacuna,
                    Aktivan = korisnik.Aktivan,
                    TitulaID = korisnik.TitulaID,
                    Titula = korisnik.Titula.Naziv,
                    VrstaAcc = "Stomatolog",
                    Titule = _context.Titulas.Select
                        (i => new SelectListItem { Text = i.Naziv, Value = i.TitulaId.ToString() }).ToList(),
                    KorisnikId = id
                };
                return View("UrediKorisnike", model);
            }
            return View("UrediStomatolog");
        }

        [Autorizacija(true, false, false, false)]
        [ActionName("dodaj-stomatolog")]
        public IActionResult DodajStomatolog()
        {
            var model = new KorisnikDodajStomatologViewModel
            {
                Gradovi = _context.Grads.Select
                    (i => new SelectListItem { Text = i.Naziv, Value = i.GradId.ToString() }).ToList(),
                Titule = _context.Titulas.Select
                    (i => new SelectListItem { Text = i.Naziv, Value = i.TitulaId.ToString() }).ToList()
            };
            return View("DodajStomatolog", model);
        }

        //[Autorizacija(true,false,false,false)]
        //[ActionName("dodaj-stomatolog")]
        //[HttpPost]
        //public IActionResult DodajStomatolog(KorisnikDodajStomatologViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //        return RedirectToAction("dodaj-stomatolog");

        //    if (_context.KorisnickiNalogs.Any(i => i.Email == model.Email))
        //    {
        //        TempData["errorMessage"] = "Email adresa se koristi.";
        //        return RedirectToAction("dodaj-stomatolog");
        //    }

        //    byte[] lozinkaSalt = PasswordSettings.GetSalt();
        //    string lozinkaHash = PasswordSettings.GetHash(model.Lozinka, lozinkaSalt);


        //    string uniqueFileName = UploadedFile(model);

        //    KorisnickiNalog korisnickiNalog = new KorisnickiNalog
        //    {
        //        Ime = model.Ime,
        //        Prezime = model.Prezime,
        //        Email = model.Email,
        //        LozinkaHash = lozinkaHash,
        //        LozinkaSalt = Convert.ToBase64String(lozinkaSalt),
        //        Permisije = 1,
        //        Kreirano = DateTime.Now,
        //        Mobitel = model.Mobitel,
        //        GradId = model.GradID,
        //        Adresa = model.Adresa,
        //        JMBG = model.JMBG,
        //        DatumRodjenja = model.DatumRodjenja,
        //        Spol = model.Spol,
        //        Slika = uniqueFileName
        //    };
        //    Stomatolog stomatolog = new Stomatolog
        //    {
        //        KorisnickiNalog = korisnickiNalog,
        //        TitulaID = model.TitulaID,
        //        DatumZaposlenja = model.DatumZaposlenja,
        //        BrojZiroRacuna = model.BrojZiroRacuna,
        //        Aktivan = model.Aktivan
        //    };

        //    _context.KorisnickiNalogs.Add(korisnickiNalog);
        //    _context.Stomatologs.Add(stomatolog);

        //    _context.SaveChanges();

        //    TempData["successMessage"] = "Uspješno ste dodali novog stomatologa.";
        //    return RedirectToAction("uredi-stomatolog");
        //}

        //[Autorizacija(true,false,false,false)]
        //[ActionName("uredi-osoblje")]
        //public IActionResult UrediOsoblje(int id = -1)
        //{
        //    if (id != -1)
        //    {
        //        dynamic korisnik = _context.MedicinskoOsobljes.Include(i=>i.Titula)
        //            .Include(i => i.KorisnickiNalog)
        //            .ThenInclude(i => i.Grad)
        //            .SingleOrDefault(i => i.MedicinskoOsoboljeId == id);

        //        TempData["Layout"] = "_Administrator";

        //        var model = new KorisnikPrikazViewModel 
        //        {
        //            Ime = korisnik.KorisnickiNalog.Ime,
        //            Prezime = korisnik.KorisnickiNalog.Prezime,
        //            Email = korisnik.KorisnickiNalog.Email,
        //            JMBG = korisnik.KorisnickiNalog.JMBG,
        //            DatumRodjenjaString = korisnik.KorisnickiNalog.DatumRodjenja.ToString("dd.MM.yyyy"),
        //            Mobitel = korisnik.KorisnickiNalog.Mobitel,
        //            Adresa = korisnik.KorisnickiNalog.Adresa,
        //            GradID = korisnik.KorisnickiNalog.GradId,
        //            Grad = korisnik.KorisnickiNalog.Grad.Naziv,
        //            Gradovi = _context.Grads.Select
        //                (i => new SelectListItem { Text = i.Naziv, Value = i.GradId.ToString() }).ToList(),
        //            Spol = korisnik.KorisnickiNalog.Spol,
        //            Slika = korisnik.KorisnickiNalog.Slika,
        //            DatumZaposlenjaString = korisnik.DatumZaposlenja.ToString("dd.MM.yyyy"),
        //            BrojZiroRacuna = korisnik.BrojZiroRacuna,
        //            OpisPosla = korisnik.OpisPosla,
        //            Aktivan = korisnik.Aktivan,
        //            TitulaID = korisnik.TitulaID,
        //            Titula = korisnik.Titula.Naziv,
        //            VrstaAcc = "Medicinsko osoblje",
        //            Titule = _context.Titulas.Select
        //                (i => new SelectListItem {Text = i.Naziv, Value = i.TitulaId.ToString()}).ToList(),
        //            KorisnikId = id
        //        };
        //        return View("UrediKorisnike", model);
        //    }
        //    return View("UrediOsoblje");
        //}

        //[Autorizacija(true,false,false,false)]
        //[ActionName("dodaj-osoblje")]
        //public IActionResult DodajOsoblje()
        //{
        //    var model = new KorisnikDodajOsobljeViewModel
        //    {
        //        Gradovi = _context.Grads.Select
        //            (i => new SelectListItem { Text = i.Naziv, Value = i.GradId.ToString() }).ToList(),
        //        Titule = _context.Titulas.Select
        //            (i=>new SelectListItem{Text = i.Naziv,Value = i.TitulaId.ToString()}).ToList()
        //    };
        //    return View("DodajOsoblje", model);
        //}

        //[Autorizacija(true,false,false,false)]
        //[ActionName("dodaj-osoblje")]
        //[HttpPost]
        //public IActionResult DodajOsoblje(KorisnikDodajOsobljeViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //        return RedirectToAction("dodaj-osoblje");

        //    if (_context.KorisnickiNalogs.Any(i => i.Email == model.Email))
        //    {
        //        TempData["errorMessage"] = "Email adresa se koristi.";
        //        return RedirectToAction("dodaj-osoblje");
        //    }

        //    byte[] lozinkaSalt = PasswordSettings.GetSalt();
        //    string lozinkaHash = PasswordSettings.GetHash(model.Lozinka, lozinkaSalt);


        //    string uniqueFileName = UploadedFile(model);

        //    KorisnickiNalog korisnickiNalog = new KorisnickiNalog
        //    {
        //        Ime = model.Ime,
        //        Prezime = model.Prezime,
        //        Email = model.Email,
        //        LozinkaHash = lozinkaHash,
        //        LozinkaSalt = Convert.ToBase64String(lozinkaSalt),
        //        Permisije = 2,
        //        Kreirano = DateTime.Now,
        //        Mobitel = model.Mobitel,
        //        GradId = model.GradID,
        //        Adresa = model.Adresa,
        //        JMBG = model.JMBG,
        //        DatumRodjenja = model.DatumRodjenja,
        //        Spol = model.Spol,
        //        Slika = uniqueFileName
        //    };
        //    MedicinskoOsoblje osoblje = new MedicinskoOsoblje
        //    {
        //        KorisnickiNalog = korisnickiNalog,
        //        TitulaID = model.TitulaID,
        //        DatumZaposlenja = model.DatumZaposlenja,
        //        BrojZiroRacuna = model.BrojZiroRacuna,
        //        Aktivan = model.Aktivan,
        //        OpisPosla = model.OpisPosla
        //    };

        //    _context.KorisnickiNalogs.Add(korisnickiNalog);
        //    _context.MedicinskoOsobljes.Add(osoblje);

        //    _context.SaveChanges();

        //    TempData["successMessage"] = "Uspješno ste dodali novog uposlenika.";
        //    return RedirectToAction("uredi-osoblje");
        //}

        [HttpPost]
        [ActionName("snimi-izmjene")]
        [DisableRequestSizeLimit]
        public IActionResult SnimiIzmjene(KorisnikPrikazViewModel model)
        {
            
            KorisnickiNalog logiraniKorisnik = HttpContext.GetLogiraniKorisnik();
            if (logiraniKorisnik.Permisije == 0)
                TempData["Layout"] = "_Administrator";

            if (logiraniKorisnik.Permisije == 1)
                TempData["Layout"] = "_Stomatolog";

            if (logiraniKorisnik.Permisije == 2)
                TempData["Layout"] = "_MedicinskoOsoblje";


            if (!ModelState.IsValid)
                return RedirectToAction("uredi-pacijent", model.KorisnikId);

            var vrsta = model.VrstaAcc;

            dynamic korisnik = null;

            if (vrsta == "Stomatolog")
            {
                var korisnickiId = _context.Stomatologs.Where(i => i.StomatologId == model.KorisnikId).Select(i => i.KorisnickiNalogId).SingleOrDefault();
                korisnik = _context.Stomatologs
                    .Include(i => i.KorisnickiNalog)
                    .ThenInclude(i => i.Grad)
                    .SingleOrDefault(i => i.KorisnickiNalogId == korisnickiId);
            }
            if (vrsta == "Medicinsko osoblje")
            {
                var korisnickiId = _context.MedicinskoOsobljes.Where(i => i.MedicinskoOsoboljeId == model.KorisnikId).Select(i => i.KorisnickiNalogId).SingleOrDefault();

                korisnik = _context.MedicinskoOsobljes
                    .Include(i => i.KorisnickiNalog)
                    .ThenInclude(i => i.Grad)
                    .SingleOrDefault(i => i.KorisnickiNalogId == korisnickiId);
            }
            if (vrsta == "Pacijent")
            {
                var korisnickiId = _context.Pacijents.Where(i => i.PacijentId == model.KorisnikId).Select(i => i.KorisnickiNalogId).SingleOrDefault();

                korisnik = _context.Pacijents
                    .Include(i => i.KorisnickiNalog)
                    .ThenInclude(i => i.Grad)
                    .SingleOrDefault(i => i.KorisnickiNalogId == korisnickiId);
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

                if (System.IO.File.Exists(_imageToBeDeleted))
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

            if (vrsta == "Pacijent")
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
                if (vrsta == "Administrator" || vrsta == "Medicinsko osoblje")
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

                if (vrsta == "Stomatolog" || vrsta == "Medicinsko osoblje")
                {
                    if (korisnik.TitulaID != model.TitulaID)
                    {
                        korisnik.TitulaID = model.TitulaID;
                        _context.SaveChanges();

                        TempData["successMessage"] = "Titula uspješno promjenuta.";
                    }
                }

            }

            if (vrsta == "Stomatolog")
                return RedirectToAction("uredi-stomatolog");

            if (vrsta == "Medicinsko osoblje")
                return RedirectToAction("uredi-osoblje");

            return RedirectToAction("uredi-pacijent");
        }
        private string UploadedFile(KorisnikPrikazViewModel model)  
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
        private string UploadedFile(KorisnikDodajPacijentaViewModel model)  
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

        private string UploadedFile(KorisnikDodajStomatologViewModel model)  
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
        private string UploadedFile(KorisnikDodajOsobljeViewModel model)  
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

        [Autorizacija(true, false, false, false)]
        [ActionName("izbrisi-pacijenta")]
        public IActionResult IzbrisiPacijenta(int id)
        {
            var pacijent = _context.Pacijents.FirstOrDefault(i => i.PacijentId == id);
            if (pacijent != null)
            {
                var korisnikId = pacijent.KorisnickiNalogId;
                var nalogzabrisanje = _context.KorisnickiNalogs.FirstOrDefault(i => i.KorisnickiNalogId == korisnikId);
                _context.Remove(pacijent);
                _context.Remove(nalogzabrisanje);
                _context.SaveChanges();
            }
            return RedirectToAction("uredi-pacijent");
        }

        //[Autorizacija(true, false, false, false)]
        //[ActionName("izbrisi-osoblje")]
        //public IActionResult IzbrisiOsoblje(int id)
        //{
        //    var osoblje = _context.MedicinskoOsobljes.FirstOrDefault(i => i.MedicinskoOsoboljeId == id);
        //    if (osoblje != null)
        //    {
        //        var korisnikId = osoblje.KorisnickiNalogId;
        //        var nalogzabrisanje = _context.KorisnickiNalogs.FirstOrDefault(i => i.KorisnickiNalogId == korisnikId);
        //        _context.Remove(osoblje);
        //        _context.Remove(nalogzabrisanje);
        //        _context.SaveChanges();
        //    }
        //    return RedirectToAction("uredi-osoblje");
        }

        //[Autorizacija(true, false, false, false)]
        //[ActionName("izbrisi-stomatolog")]
        //public IActionResult IzbrisiStomatolog(int id)
        //{
        //    var stomatolog = _context.Stomatologs.FirstOrDefault(i => i.StomatologId == id);
        //    if (stomatolog != null)
        //    {
        //        var korisnikId = stomatolog.KorisnickiNalogId;
        //        var nalogzabrisanje = _context.KorisnickiNalogs.FirstOrDefault(i => i.KorisnickiNalogId == korisnikId);
        //        _context.Remove(stomatolog);
        //        _context.Remove(nalogzabrisanje);
        //        _context.SaveChanges();
        //    }
        //    return RedirectToAction("uredi-stomatolog");
        //}
        public bool IsOsobljeOrStomatolog(KorisnikDodajOsobljeViewModel model)  
        {  
            KorisnickiNalog logiraniKorisnik = HttpContext.GetLogiraniKorisnik();
            
            if (logiraniKorisnik.Permisije == 1 || logiraniKorisnik.Permisije == 2)
                return true;
            return false;
        }
    }
}