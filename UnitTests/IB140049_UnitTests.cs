using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StomatoloskaOrdinacija.Data;
using StomatoloskaOrdinacija.Data.EntityModels;
using StomatoloskaOrdinacija.Web.Controllers;
using StomatoloskaOrdinacija.Web.ViewModels.Termin;

namespace StomatoloskaOrdinacija.UnitTests
{
    [TestClass]
    public class IB140049_UnitTests
    {
        public MyContext CreateContextForInMemory()
        {
            var option = new DbContextOptionsBuilder<MyContext>().UseInMemoryDatabase(databaseName: "Test_Database").Options;

            var context = new MyContext(option);
            if (context != null)
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            return context;
        }

        private readonly MyContext _context;

        public IB140049_UnitTests()
        {
            _context = CreateContextForInMemory();

            var drzava = new Drzava { Naziv = "..." };
            var grad = new Grad { Naziv = "...", Drzava = drzava, PostanskiBroj = "..."};
            var titula = new Titula { Naziv = "..." };

            var korisnickiNalog = new KorisnickiNalog
            {
                Ime = "...",
                Prezime = "...",
                Email = "...",
                LozinkaHash = "...",
                LozinkaSalt = "...",
                Permisije = 3,
                Kreirano = DateTime.Now,
                JMBG = "...",
                Adresa = "...",
                DatumRodjenja = DateTime.Now,
                Mobitel = "...",
                Grad = grad,
                Spol = "...",
                Slika = "..."
            };

            var pacijent = new Pacijent
            {
                KorisnickiNalog = korisnickiNalog,
                AlergijaNaLijek = false,
                Aparatic = false,
                Navlake = false,
                Proteza = false,
                Terapija = false
            };

            var termin = new Termin
            {
                Pacijent = pacijent,
                DatumVrijeme = DateTime.Now,
                Hitan = false,
                NaCekanju = true,
                Odobren = false,
                Razlog = "..."
            };

            _context.AddRange(drzava, grad, titula, korisnickiNalog, pacijent, termin);
            _context.SaveChanges();
        }


        [TestMethod]
        [DataRow(1)]
        public void Test_ListaSvihTerminaPacijenta(int id)
        {
            var kontroler = new TerminController(_context);
            var vr = kontroler.ListaSvihTerminaPacijenta(id);
            var rezultat = vr.Count;

            Assert.AreEqual(1, rezultat);
        }

        [TestMethod]
        [DataRow(1)]
        public void Test_FunkcijaTermin(int id)
        {
            try
            {
                var kontroler = new TerminController(_context);

                var vr = kontroler.PromjeniStanjeTermina(id, "odobri") as ViewResult;
                var rezultat = vr.Model as TerminPregledViewModel;
                Assert.AreEqual("Ne", rezultat.NaCekanju);
                Assert.AreEqual("Da", rezultat.Odobren);

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
