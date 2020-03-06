using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace StomatoloskaOrdinacija.Web.ViewModels.Administracija
{
    public class AdministracijaDodajGrad
    {
        public string Naziv { get; set; }
        public List<SelectListItem> Drzave { get; set; }
        public string PostanskiBroj { get; set; }
    }
}
