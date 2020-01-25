using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

using StomatoloskaOrdinacija.Data;
using StomatoloskaOrdinacija.Data.EntityModels;

namespace StomatoloskaOrdinacija.Web.Helper
{
    public class AutorizacijaAttribute : TypeFilterAttribute
    {
        public AutorizacijaAttribute(bool Administrator, bool Stomatolog, bool MedicinskoOsobolje, bool Pacijent) : base(typeof(MyAuthorizeImpl))
        {
            Arguments = new object[] { Administrator, Stomatolog, MedicinskoOsobolje, Pacijent };
        }
    }

    public class MyAuthorizeImpl : IAsyncActionFilter
    {
        private readonly bool _administrator;
        private readonly bool _stomatolog;
        private readonly bool _medicinskoOsoblje;
        private readonly bool _pacijent;

        public MyAuthorizeImpl(bool Administrator, bool Stomatolog, bool MedicinskoOsoblje, bool Pacijent)
        {
            _administrator = Administrator;
            _stomatolog = Stomatolog;
            _medicinskoOsoblje = MedicinskoOsoblje;
            _pacijent = Pacijent;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext filterContext, ActionExecutionDelegate next)
        {
            KorisnickiNalog korisnickiNalog = filterContext.HttpContext.GetLogiraniKorisnik();

            if (korisnickiNalog == null)
            {
                filterContext.Result = new RedirectToActionResult("Prijava", "Prijava", new { @area = "" });
                return;
            }

            MyContext dbContext = filterContext.HttpContext.RequestServices.GetService<MyContext>();

            if (_administrator && dbContext.Administrators.Any(i => i.KorisnickiNalogId == korisnickiNalog.KorisnickiNalogId))
            {
                await next();
                return;
            }

            if (_stomatolog && dbContext.Stomatologs.Any(i => i.KorisnickiNalogId == korisnickiNalog.KorisnickiNalogId))
            {
                await next();
                return;
            }

            if (_medicinskoOsoblje && dbContext.MedicinskoOsobljes.Any(i => i.KorisnickiNalogId == korisnickiNalog.KorisnickiNalogId))
            {
                await next();
                return;
            }

            if (_pacijent && dbContext.Pacijents.Any(i => i.KorisnickiNalogId == korisnickiNalog.KorisnickiNalogId))
            {
                await next();
                return;
            }

            filterContext.Result = new RedirectToActionResult("Prijava", "Prijava", new { @area = "" });
        }
    }
}