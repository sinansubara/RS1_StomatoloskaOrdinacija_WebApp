using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StomatoloskaOrdinacija.Data;
using StomatoloskaOrdinacija.Data.EntityModels;
using StomatoloskaOrdinacija.Web.Helper;
using StomatoloskaOrdinacija.Web.ViewModels.Administracija;
using StomatoloskaOrdinacija.Web.ViewModels.Korisnik;

namespace StomatoloskaOrdinacija.Web.Controllers
{
    public class AdministracijaController : Controller
    {
        private MyContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private IConfiguration _configuration;
        public AdministracijaController(MyContext context, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return Redirect("/Profil/Pocetna");
        }

        [Autorizacija(true,false,false,false)]
        public IActionResult ListaTitula()
        {
            var lista = _context.Titulas.Select(i => new AdministracijaPrikazTitulaViewModel
            {
                TitulaId = i.TitulaId,
                Titula = i.Naziv
            }).ToList();

            return Json(new {data = lista});
        }

        [Autorizacija(true,false,false,false)]
        [ActionName("uredi-titulu")]
        public IActionResult UrediTitulu(string imetitule = "")
        {
            if (imetitule != "")
            {
                var novatitula = new Titula
                {
                    Naziv = imetitule
                };
                _context.Titulas.Add(novatitula);
                _context.SaveChanges();
                TempData["successMessage"] = "Titula uspješno dodana.";
            }
            return View("UrediTitulu");
        }
        [Autorizacija(true,false,false,false)]
        [ActionName("dodaj-titulu")]
        public IActionResult DodajTitulu()
        {
            return PartialView("DodajTitulu");
        }
        [Autorizacija(true,false,false,false)]
        [ActionName("izbrisi-titulu")]
        public IActionResult IzbrisiTitulu(int id)
        {
            Titula titulazabrisanje = _context.Titulas.Find(id);
            _context.Titulas.Remove(titulazabrisanje);
            _context.SaveChanges();
            TempData["successMessage"] = "Titula uspješno izbrisana.";
            return RedirectToAction("uredi-titulu");
        }

        [Autorizacija(true,false,false,false)]
        public IActionResult ListaDrzava()
        {
            var lista = _context.Drzavas.Select(i => new AdministracijaPrikazDrzavaViewModel
            {
                DrzavaId = i.DrzavaId,
                Drzava = i.Naziv
            }).ToList();

            return Json(new {data = lista});
        }

        [Autorizacija(true,false,false,false)]
        [ActionName("uredi-drzavu")]
        public IActionResult UrediDrzavu(string imedrzave = "")
        {
            if (imedrzave != "")
            {
                var novadrzava = new Drzava
                {
                    Naziv = imedrzave
                };
                _context.Drzavas.Add(novadrzava);
                _context.SaveChanges();
                TempData["successMessage"] = "Država uspješno dodana.";
            }
            return View("UrediDrzavu");
        }
        [Autorizacija(true,false,false,false)]
        [ActionName("dodaj-drzavu")]
        public IActionResult DodajDrzavu()
        {
            return PartialView("DodajDrzavu");
        }
        [Autorizacija(true,false,false,false)]
        [ActionName("izbrisi-drzavu")]
        public IActionResult IzbrisiDrzavu(int id)
        {
            Drzava drzavazabrisanje = _context.Drzavas.Find(id);
            Grad drzavaSeKoristi = _context.Grads.Where(i => i.DrzavaId == id).FirstOrDefault();
            if (drzavaSeKoristi == null)
            {
                _context.Drzavas.Remove(drzavazabrisanje);
                _context.SaveChanges();
                TempData["successMessage"] = "Država uspješno izbrisana.";
                return RedirectToAction("uredi-drzavu");
            }
            TempData["errorMessage"] = "Država se koristi.";
            return RedirectToAction("uredi-drzavu");
        }

        [Autorizacija(true,false,false,false)]
        public IActionResult ListaGradova()
        {
            var lista = _context.Grads.Select(i => new AdministracijaPrikazGradViewModel
            {
                GradId = i.GradId,
                Grad = i.Naziv,
                Drzava = i.Drzava.Naziv,
                PostanskiBroj = i.PostanskiBroj
            }).ToList();

            return Json(new {data = lista});
        }

        [Autorizacija(true,false,false,false)]
        [ActionName("uredi-grad")]
        public IActionResult UrediGrad(int drzavaid, string imegrada = "", string postanskibroj="")
        {
            if (imegrada != "")
            {
                if (imegrada != null && postanskibroj != null)
                {
                    if (imegrada.Length > 2 && imegrada.Length<100 && postanskibroj.Length>2 && postanskibroj.Length<20)
                    {
                        var novigrad = new Grad
                        {
                            Naziv = imegrada,
                            DrzavaId = drzavaid,
                            PostanskiBroj = postanskibroj
                        };
                        _context.Grads.Add(novigrad);
                        _context.SaveChanges();
                        TempData["successMessage"] = "Grad uspješno dodan.";
                    }
                }
            }
            return View("UrediGrad");
        }
        [Autorizacija(true,false,false,false)]
        [ActionName("dodaj-grad")]
        public IActionResult DodajGrad()
        {
            var model = new AdministracijaDodajGrad
            {
                Drzave = _context.Drzavas.Select(x => new SelectListItem
                {
                    Text = x.Naziv,
                    Value = x.DrzavaId.ToString()
                }).ToList()
            };
            return PartialView("DodajGrad", model);
        }
        [Autorizacija(true,false,false,false)]
        [ActionName("izbrisi-grad")]
        public IActionResult IzbrisiGrad(int id)
        {
            Grad gradzabrisanje = _context.Grads.Find(id);
            var gradSeKoristi = _context.KorisnickiNalogs.FirstOrDefault(x => x.GradId == id);
            if (gradSeKoristi == null)
            {
                _context.Grads.Remove(gradzabrisanje);
                _context.SaveChanges();
                TempData["successMessage"] = "Grad uspješno izbrisan.";
                return RedirectToAction("uredi-grad");
            }
            TempData["errorMessage"] = "Grad se koristi.";
            return RedirectToAction("uredi-grad");
        }

        [Autorizacija(true,false,false,false)]
        public IActionResult ListaUsluga()
        {
            var lista = _context.Uslugas.Select(i => new AdministracijaPrikazUslugaViewModel
            {
                UslugaId = i.UslugaId,
                Usluga = i.Naziv
            }).ToList();

            return Json(new {data = lista});
        }

        [Autorizacija(true,false,false,false)]
        [ActionName("uredi-uslugu")]
        public IActionResult UrediUslugu(string imeusluge = "")
        {
            if (imeusluge != "")
            {
                var novausluga = new Usluga
                {
                    Naziv = imeusluge
                };
                _context.Uslugas.Add(novausluga);
                _context.SaveChanges();
                TempData["successMessage"] = "Usluga uspješno dodana.";
            }
            return View("UrediUslugu");
        }
        [Autorizacija(true,false,false,false)]
        [ActionName("dodaj-uslugu")]
        public IActionResult DodajUslugu()
        {
            return PartialView("DodajUslugu");
        }
        [Autorizacija(true,false,false,false)]
        [ActionName("izbrisi-uslugu")]
        public IActionResult IzbrisiUslugu(int id)
        {
            Usluga uslugazabrisanje = _context.Uslugas.Find(id);
            _context.Uslugas.Remove(uslugazabrisanje);
            _context.SaveChanges();
            TempData["successMessage"] = "Usluga uspješno izbrisana.";
            return RedirectToAction("uredi-uslugu");
        }
    }
}