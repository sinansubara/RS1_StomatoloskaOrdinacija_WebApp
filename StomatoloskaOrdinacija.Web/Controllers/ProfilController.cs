using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StomatoloskaOrdinacija.Data;
using StomatoloskaOrdinacija.Data.EntityModels;
using StomatoloskaOrdinacija.Web.Helper;

namespace StomatoloskaOrdinacija.Web.Controllers
{
    public class ProfilController : Controller
    {
        private MyContext _context;

        public ProfilController(MyContext context)
        {
            _context = context;
        }

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

        public IActionResult PregledProfila()
        {
            return View();
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
            //List<Token> tokeni = _context.Tokens.Where
            //    (x => (DateTime.Now - x.Kreirano).TotalHours >= 24 && x.KorisnickiNalogId == logiraniKorisnik.KorisnickiNalogId).ToList();

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