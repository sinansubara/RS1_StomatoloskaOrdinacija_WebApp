﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StomatoloskaOrdinacija.Data;
using StomatoloskaOrdinacija.Data.EntityModels;
using StomatoloskaOrdinacija.Web.Helper;
using StomatoloskaOrdinacija.Web.ViewModels.Korisnik;
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
                    Datum = i.DatumVrijeme,
                    Vrijeme = i.DatumVrijeme,
                    Hitan = i.Hitan?"Da":"Ne",
                    NaCekanju = i.NaCekanju?"Da":"Ne",
                    Odobren = i.Odobren?"Da":"Ne",
                    Razlog = i.Razlog
                }).ToList();
            return Json(new { data = lista });
        }

        //[Autorizacija(false,false,false,true)]
        //public IActionResult ListaOdobrenih(int id)
        //{
        //    var lista = _context.Termins.Where(i=>i.Odobren && i.PacijentId == id)
        //        .Select(i => new TerminPregledViewModel
        //    {
        //        TerminId = i.TerminId,
        //        PacijentId = i.PacijentId,
        //        Pacijent = i.Pacijent.KorisnickiNalog.Ime+ " "+i.Pacijent.KorisnickiNalog.Prezime,
        //        Datum = i.DatumVrijeme,
        //        Vrijeme = i.DatumVrijeme,
        //        Hitan = i.Hitan?"Da":"Ne",
        //        NaCekanju = i.NaCekanju?"Da":"Ne",
        //        Odobren = i.Odobren?"Da":"Ne",
        //        Razlog = i.Razlog
        //    }).ToList();
        //    return Json(new { data = lista });
        //}
        //[Autorizacija(false,false,false,true)]
        //public IActionResult ListaOdbijenih(int id)
        //{
        //    var lista = _context.Termins.Where(i=>!i.Odobren && !i.NaCekanju && i.PacijentId == id)
        //        .Select(i => new TerminPregledViewModel
        //    {
        //        TerminId = i.TerminId,
        //        PacijentId = i.PacijentId,
        //        Pacijent = i.Pacijent.KorisnickiNalog.Ime+ " "+i.Pacijent.KorisnickiNalog.Prezime,
        //        Datum = i.DatumVrijeme,
        //        Vrijeme = i.DatumVrijeme,
        //        Hitan = i.Hitan?"Da":"Ne",
        //        NaCekanju = i.NaCekanju?"Da":"Ne",
        //        Odobren = i.Odobren?"Da":"Ne",
        //        Razlog = i.Razlog
        //    }).ToList();
        //    return Json(new { data = lista });
        //}
        //[Autorizacija(false,false,false,true)]
        //public IActionResult ListaNaCekanju(int id)
        //{
        //    var lista = _context.Termins.Where(i=>i.NaCekanju && i.PacijentId == id)
        //        .Select(i => new TerminPregledViewModel
        //    {
        //        TerminId = i.TerminId,
        //        PacijentId = i.PacijentId,
        //        Pacijent = i.Pacijent.KorisnickiNalog.Ime+ " "+i.Pacijent.KorisnickiNalog.Prezime,
        //        Datum = i.DatumVrijeme,
        //        Vrijeme = i.DatumVrijeme,
        //        Hitan = i.Hitan?"Da":"Ne",
        //        NaCekanju = i.NaCekanju?"Da":"Ne",
        //        Odobren = i.Odobren?"Da":"Ne",
        //        Razlog = i.Razlog
        //    }).ToList();
        //    return Json(new { data = lista });
        //}

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


        [Autorizacija(false,false,false,true)]
        [ActionName("dodaj-termin")]
        public IActionResult DodajTermin()
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

            var trenutnoVrijeme = DateTime.Now;
            var temp = new DateTime(trenutnoVrijeme.Year, trenutnoVrijeme.Month, trenutnoVrijeme.Day, trenutnoVrijeme.Hour, trenutnoVrijeme.Minute, trenutnoVrijeme.Second);
            var zaokruziminute = ":00";

            if (temp.Minute >= 0 && temp.Minute <= 25)
                zaokruziminute = ":30";
            else if (temp.Minute > 25)
                temp = new DateTime(trenutnoVrijeme.Year, trenutnoVrijeme.Month, trenutnoVrijeme.Day, trenutnoVrijeme.Hour + 1, trenutnoVrijeme.Minute, trenutnoVrijeme.Second);

            

            var model = new TerminDodajUrediViewModel
            {
                PacijentId = _context.Pacijents.Where(i=>i.KorisnickiNalogId == logiraniKorisnik.KorisnickiNalogId).Select(i=>i.PacijentId).FirstOrDefault(),
                datumstring = trenutnoVrijeme.ToString("dd.MM.yyyy"),
                Vrijeme = temp.ToString("HH") + zaokruziminute
            };


            return View("DodajTermin", model);
        }
        [Autorizacija(false,false,false,true)]
        [ActionName("snimi-termin")]
        public IActionResult SnimiTermin(TerminDodajUrediViewModel model)
        {
            var d = model.Datum;
            var t = DateTime.ParseExact(model.Vrijeme, "HH:mm",
                CultureInfo.InvariantCulture);
            var dtCombined = new DateTime(d.Year, d.Month, d.Day, t.Hour, t.Minute, t.Second);
            var noviTermin = new Termin
            {
                Hitan = model.Hitan,
                NaCekanju = true,
                Odobren = false,
                PacijentId = model.PacijentId,
                Razlog = model.Razlog,
                DatumVrijeme = dtCombined
            };
            _context.Termins.Add(noviTermin);
            _context.SaveChanges();


            TempData["successMessage"] = "Termin uspješno dodan.";
            return RedirectToAction("Index");
        }

        [Autorizacija(false,false,false,true)]
        [ActionName("izbrisi-termin")]
        public IActionResult IzbrisiTermin(int id)
        {
            var terminDelete = _context.Termins.Find(id);
            if (terminDelete != null)
            {
                _context.Termins.Remove(terminDelete);
                _context.SaveChanges();
            }



            TempData["successMessage"] = "Termin uspješno izbrisan.";
            return RedirectToAction("Index");
        }
    }
}