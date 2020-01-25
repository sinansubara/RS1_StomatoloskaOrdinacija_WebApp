using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StomatoloskaOrdinacija.Data;

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
            return View();
        }

        public IActionResult PregledProfila()
        {
            return View();
        }

        public IActionResult Odjava()
        {
            return View();
        }
    }
}