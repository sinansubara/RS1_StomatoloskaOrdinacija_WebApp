using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StomatoloskaOrdinacija.Web.ViewModels.Administracija
{
    public class AdministracijaPrikazGradViewModel
    {
        public int GradId { get; set; }
        public string Drzava { get; set; }
        public string Grad { get; set; }
        public string PostanskiBroj { get; set; }
    }
}
