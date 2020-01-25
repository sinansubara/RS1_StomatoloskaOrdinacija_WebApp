using System;
using System.Linq;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using StomatoloskaOrdinacija.Data;
using StomatoloskaOrdinacija.Data.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace StomatoloskaOrdinacija.Web.Helper
{
    public static class Autentifikacija
    {
        private const string logiraniKorisnik = "logiraniKorisnik";

        public static void SetLogiraniKorisnik(this HttpContext context, KorisnickiNalog korisnickiNalog, bool sacuvajUCookie = false)
        {
            MyContext dbContext = context.RequestServices.GetService<MyContext>();

            string stariToken = context.Request.GetCookieJson<string>(logiraniKorisnik);

            if (stariToken != null)
            {
                Token obrisi = dbContext.Tokens.FirstOrDefault(i => i.Vrijednost == stariToken);

                if (obrisi != null)
                {
                    dbContext.Tokens.Remove(obrisi);
                    dbContext.SaveChanges();
                }
            }

            if (korisnickiNalog != null)
            {
                string token = Guid.NewGuid().ToString();

                dbContext.Tokens.Add(new Token
                {
                    Vrijednost = token,
                    KorisnickiNalogId = korisnickiNalog.KorisnickiNalogId,
                    Kreirano = DateTime.Now
                });

                dbContext.SaveChanges();
                context.Response.SetCookieJson(logiraniKorisnik, token);
            }
        }

        public static KorisnickiNalog GetLogiraniKorisnik(this HttpContext context)
        {
            MyContext dbContext = context.RequestServices.GetService<MyContext>();

            string token = context.Request.GetCookieJson<string>(logiraniKorisnik);

            if (token == null)
                return null;

            return dbContext.Tokens
                .Where(x => x.Vrijednost == token)
                .Select(s => s.KorisnickiNalog)
                .SingleOrDefault();
        }

        public static string GetTrenutniToken(this HttpContext context)
        {
            return context.Request.GetCookieJson<string>(logiraniKorisnik);
        }
    }
}