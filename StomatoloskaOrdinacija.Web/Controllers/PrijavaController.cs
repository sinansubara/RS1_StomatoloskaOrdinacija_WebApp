using System;
using System.Collections.Generic;
using System.Globalization;
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
using IpData;
using Newtonsoft.Json.Linq;
using Nexmo.Api;
using RestSharp;

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
            if (!_context.Administrators.Any())
            {
                byte[] lozinkaSalt = PasswordSettings.GetSalt();
                string lozinkaHash = PasswordSettings.GetHash("Admin24!", lozinkaSalt);
                if (!_context.Grads.Any())
                {
                    if (!_context.Drzavas.Any())
                    {
                        var novaDrzava = new Drzava
                        {
                            Naziv = "Bosna i Hercegovina"
                        };
                        _context.Add(novaDrzava);
                        _context.SaveChanges();
                    }

                    var noviGrad = new Grad
                    {
                        DrzavaId = 1,
                        Naziv = "Jablanica",
                        PostanskiBroj = "88420"
                    };
                    _context.Add(noviGrad);
                    _context.SaveChanges();
                }
                var AdminKorisnik = new KorisnickiNalog
                {
                    Ime = "Dino",
                    Prezime = "Nanić",
                    Email = "stomatoloska.ordinacija24@gmail.com",
                    LozinkaHash = lozinkaHash,
                    LozinkaSalt = Convert.ToBase64String(lozinkaSalt),
                    Permisije = 0,
                    Kreirano = DateTime.Now,
                    JMBG = "0101990150023",
                    DatumRodjenja = new DateTime(1990, 1, 1),
                    Mobitel = "38762516238",
                    Adresa = "San BB",
                    GradId = 1,
                    Spol = "Muško",
                    Slika = "blank-profile.jpg"
                };
                _context.Add(AdminKorisnik);
                _context.SaveChanges();
                var NoviAdministrator = new Administrator
                {
                    KorisnickiNalog = _context.KorisnickiNalogs.SingleOrDefault(i => i.Email == AdminKorisnik.Email),
                    DatumZaposlenja = DateTime.Now,
                    OpisPosla = "Administracija stranice",
                    BrojZiroRacuna = "4343000022225555",
                    Aktivan = true
                };
                _context.Add(NoviAdministrator);
                _context.SaveChanges();

                var novaTitula = new Titula
                {
                    Naziv = "dr."
                };
                _context.Add(novaTitula);
                _context.SaveChanges();
            }
                

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
                HttpContext.SetLogiraniKorisnik(korisnickiNalog.First(), true); //setuje logiranog korisnika

                var prijavaLokacijaMail = GetLoginLocation(model.Email, "mail"); //dobavlja informacije o lokaciji prijave
                var prijavaLokacijaMobitel = GetLoginLocation(model.Email, "mobitel"); //dobavlja informacije o lokaciji prijave

                var trenutnoVrijeme = DateTime.Now.ToString(new CultureInfo("de-DE"));//trenutno vrijeme prebacuje na njemacki format datum 19.03.2020 15:35:43
                var primalacPoruke = korisnickiNalog.First().Ime + " " + korisnickiNalog.First().Prezime; //ime i prezime za email
                var primalacEmail = korisnickiNalog.First().Email; //primalac email-a
                var prijavaEmailPoruka = "Poštovani " + primalacPoruke +
                                        ",\nDetektovana je prijava na vaš račun" +
                                        "\n-----------------------------------------------\n\n" + prijavaLokacijaMail +
                                        "\nDatum i vrijeme: " + trenutnoVrijeme +
                                        "\n!!!AKO OVO NISTE BILI VI, MOLIMO VAS DA PROMJENITE VAŠU LOZINKU!!!" +
                                        "\nIli nas kontaktirajte na naš mail: stomatoloska.ordinacija24@gmail.com"; //generisanje email poruke

                var primalacPorukeTelefon = korisnickiNalog.First().Mobitel;




                //VAZNO!!! UKLONI KOMENTARE DA BI PRORADILO SLANJE PORUKA

                var client = new Client(creds: new Nexmo.Api.Request.Credentials
                {
                    ApiKey = _configuration.GetValue<string>("NexmoSmsGateway:ApiKey"),
                    ApiSecret = _configuration.GetValue<string>("NexmoSmsGateway:ApiSecret")
                });
                var results = client.SMS.Send(request: new SMS.SMSRequest
                {
                    from = "Ordinacija",
                    to = primalacPorukeTelefon,
                    text = prijavaLokacijaMobitel
                });


                EmailSettings.SendEmail(_configuration, primalacPoruke, primalacEmail, "Nova prijava detektovana", prijavaEmailPoruka);//šalje email

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
            {
                model.Gradovi = _context.Grads.Select
                    (i => new SelectListItem {Text = i.Naziv, Value = i.GradId.ToString()}).ToList();
                return View("Registracija", model);
            }


            if (_context.KorisnickiNalogs.Any(i => i.Email == model.Email))
            {
                TempData["errorMessage"] = "Email adresa se koristi.";
                model.Gradovi = _context.Grads.Select
                    (i => new SelectListItem {Text = i.Naziv, Value = i.GradId.ToString()}).ToList();
                return View("Registracija", model);
            }

            byte[] lozinkaSalt = PasswordSettings.GetSalt();
            string lozinkaHash = PasswordSettings.GetHash(model.Lozinka, lozinkaSalt);

            //implementiraj izmjenu lozinke ako je 0 na pocetku, prebaci u 387
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
                primalacPoruke = korisnickiNalog.Ime + " " + korisnickiNalog.Prezime;
            

            if (korisnickiNalog.Permisije == 1)
                primalacPoruke = korisnickiNalog.Ime + " " + korisnickiNalog.Prezime;
            

            if (korisnickiNalog.Permisije == 2)
                primalacPoruke = korisnickiNalog.Ime + " " + korisnickiNalog.Prezime;
            
            if (korisnickiNalog.Permisije == 3)
                primalacPoruke = korisnickiNalog.Ime + " " + korisnickiNalog.Prezime;
            

            string vrijednost = RandomString.GetString(30);
            string link = 
                $"{ this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/prijava/promjena-lozinke?vrijednost=" + vrijednost;

            string poruka = "Kako bi promjenili lozinku, morate kliknut na sljedeći link: \n" + link +
                "\nLink za resetiranje lozinke, će biti aktivan samo 24 sata, a poslije toga će postati nevažeći.";

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

            var model = new PromjenaLozinkeViewModel
            {
                GenerisanaVrijednost = vrijednost
            };
            return View("PromjenaLozinke", model);
        }

        [HttpPost]
        [ActionName("promjena-lozinke")]
        public IActionResult PromjenaLozinke(PromjenaLozinkeViewModel model)
        {
            if (!ModelState.IsValid)
                return View("PromjenaLozinke", model);

            string vrijednost = model.GenerisanaVrijednost;

            PromjenaLozinke promjenaLozinke = _context.PromjenaLozinkes.SingleOrDefault(i => i.Vrijednost == vrijednost);

            KorisnickiNalog korisnickiNalog = _context.KorisnickiNalogs.SingleOrDefault
                (i => i.KorisnickiNalogId == promjenaLozinke.KorisnickiNalogID);

            if (korisnickiNalog != null)
                korisnickiNalog.LozinkaHash = PasswordSettings.GetHash(model.NovaLozinka,
                    Convert.FromBase64String(korisnickiNalog.LozinkaSalt));

            _context.PromjenaLozinkes.Remove(promjenaLozinke);
            _context.SaveChanges();

            TempData["successMessage"] = "Uspješno ste promijenili lozinku.";
            return RedirectToAction("Prijava");
        }

        private string GetLoginLocation(string email, string vrsta)
        {
            var clientip = HttpContext.Connection.RemoteIpAddress.ToString();
            var IP = "https://api.ipdata.co/" + 
                     clientip + 
                "?api-key=2d07d672cba0c9d7650f5512f2639784597f7e441cfd992718bee66f";
                     

            var client = new RestClient(IP);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            var obj = JObject.Parse(response.Content);

            var location = "Objekat ipdata je prazan!";
            if (obj["ip"] == null)
            {
                location = "Prijavili ste se sa privatne IP adrese!";
                return location;
            }
            if (obj != null)
            {
                location = "Pogresno oznacena vrsta slanja poruke!";
                if (vrsta == "mail")
                {
                    location = "Korisnicki nalog: " + email + "\n" +
                               "IP: " + obj["ip"] + "\n" +
                               "Grad: " + obj["city"] + "\n" +
                               "Regija: " + obj["region"] + "\n" +
                               "Drzava: " + obj["country_name"] + " [" +
                               obj["region_code"] + "/" + obj["country_code"] + "]\n" +
                               "Kontinent: " + obj["continent_name"] + " [" + obj["continent_code"] + "]\n" +
                               "Koordinate: " + obj["latitude"] + ", " + obj["longitude"] + "\n" +
                               "***ASN***" + "\n" +
                               "\tASN Broj: " + obj["asn"]["asn"] + "\n" +
                               "\tIme: " + obj["asn"]["name"] + "\n" +
                               "\tDomena: " + obj["asn"]["domain"] + "\n" +
                               "\tTip: " + obj["asn"]["type"] + "\n";
                }
                if (vrsta == "mobitel")
                {
                    location = "Prijava: " + email + "\n"
                               + obj["ip"] + "\n"
                               + obj["city"] + ", " + obj["region_code"]
                               + "\n[" + obj["latitude"] + ", " + obj["longitude"] + "\n"
                               + obj["asn"]["domain"];
                }
                
            }
            

            return location;
        }
    }
}