using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StomatoloskaOrdinacija.Data;
using StomatoloskaOrdinacija.Data.EntityModels;
using StomatoloskaOrdinacija.Web.Helper;
using StomatoloskaOrdinacija.Web.ViewModels.Termin;

namespace StomatoloskaOrdinacija.Web.Controllers
{
    public class TerminController : Controller
    {
        private MyContext _context;

        public TerminController(MyContext context)
        {
            _context = context;
        }

        [Autorizacija(false,false,false,true)]
        public IActionResult ListaSvihTermina(int id)
        {
            var lista = _context.Termins.Where(i=>i.PacijentId == id)
                .Select(i => new TerminPregledViewModel
                {
                    TerminId = i.TerminId,
                    PacijentId = i.PacijentId,
                    Pacijent = i.Pacijent.KorisnickiNalog.Ime+ " "+i.Pacijent.KorisnickiNalog.Prezime,
                    Datum = i.Datum,
                    Vrijeme = i.Vrijeme,
                    Hitan = i.Hitan?"Da":"Ne",
                    NaCekanju = i.NaCekanju?"Da":"Ne",
                    Odobren = i.Odobren?"Da":"Ne",
                    Razlog = i.Razlog
                }).ToList();
            return Json(new { data = lista });
        }

        [Autorizacija(false,false,false,true)]
        public IActionResult ListaOdobrenih(int id)
        {
            var lista = _context.Termins.Where(i=>i.Odobren && i.PacijentId == id)
                .Select(i => new TerminPregledViewModel
            {
                TerminId = i.TerminId,
                PacijentId = i.PacijentId,
                Pacijent = i.Pacijent.KorisnickiNalog.Ime+ " "+i.Pacijent.KorisnickiNalog.Prezime,
                Datum = i.Datum,
                Vrijeme = i.Vrijeme,
                Hitan = i.Hitan?"Da":"Ne",
                NaCekanju = i.NaCekanju?"Da":"Ne",
                Odobren = i.Odobren?"Da":"Ne",
                Razlog = i.Razlog
            }).ToList();
            return Json(new { data = lista });
        }
        [Autorizacija(false,false,false,true)]
        public IActionResult ListaOdbijenih(int id)
        {
            var lista = _context.Termins.Where(i=>!i.Odobren && !i.NaCekanju && i.PacijentId == id)
                .Select(i => new TerminPregledViewModel
            {
                TerminId = i.TerminId,
                PacijentId = i.PacijentId,
                Pacijent = i.Pacijent.KorisnickiNalog.Ime+ " "+i.Pacijent.KorisnickiNalog.Prezime,
                Datum = i.Datum,
                Vrijeme = i.Vrijeme,
                Hitan = i.Hitan?"Da":"Ne",
                NaCekanju = i.NaCekanju?"Da":"Ne",
                Odobren = i.Odobren?"Da":"Ne",
                Razlog = i.Razlog
            }).ToList();
            return Json(new { data = lista });
        }
        [Autorizacija(false,false,false,true)]
        public IActionResult ListaNaCekanju(int id)
        {
            var lista = _context.Termins.Where(i=>i.NaCekanju && i.PacijentId == id)
                .Select(i => new TerminPregledViewModel
            {
                TerminId = i.TerminId,
                PacijentId = i.PacijentId,
                Pacijent = i.Pacijent.KorisnickiNalog.Ime+ " "+i.Pacijent.KorisnickiNalog.Prezime,
                Datum = i.Datum,
                Vrijeme = i.Vrijeme,
                Hitan = i.Hitan?"Da":"Ne",
                NaCekanju = i.NaCekanju?"Da":"Ne",
                Odobren = i.Odobren?"Da":"Ne",
                Razlog = i.Razlog
            }).ToList();
            return Json(new { data = lista });
        }

        [Autorizacija(false,false,false,true)]
        public IActionResult Index()
        {
            KorisnickiNalog logiraniKorisnik = HttpContext.GetLogiraniKorisnik();
            if (logiraniKorisnik.Permisije == 0)
                TempData["Layout"] = "_Administrator";

            if (logiraniKorisnik.Permisije == 1)
                TempData["Layout"] = "_Stomatolog";

            if (logiraniKorisnik.Permisije == 2)
                TempData["Layout"] = "_MedicinskoOsoblje";

            if (logiraniKorisnik.Permisije == 3)
                TempData["Layout"] = "_Pacijent";

            var model = new TerminPregledViewModel
            {
                PacijentId = _context.Pacijents.Where(i => i.KorisnickiNalogId == logiraniKorisnik.KorisnickiNalogId)
                    .Select(i => i.PacijentId).FirstOrDefault()
            };

            return View(model);
        }
    }
}