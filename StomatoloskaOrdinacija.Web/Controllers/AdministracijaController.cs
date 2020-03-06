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
            return RedirectToAction("uredi-titulu");
        }

    }
}