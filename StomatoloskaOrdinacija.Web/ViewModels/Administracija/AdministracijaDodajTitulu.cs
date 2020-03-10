using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StomatoloskaOrdinacija.Web.ViewModels.Administracija
{
    public class AdministracijaDodajTitulu
    {
        [Required]
        [StringLength(50)]
        public string Naziv;
    }
}
