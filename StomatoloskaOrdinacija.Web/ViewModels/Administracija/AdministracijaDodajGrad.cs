using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace StomatoloskaOrdinacija.Web.ViewModels.Administracija
{
    public class AdministracijaDodajGrad
    {
        [Required]
        [StringLength(100)]
        public string Naziv { get; set; }

        public List<SelectListItem> Drzave { get; set; }

        [Required]
        [StringLength(20)] 
        public string PostanskiBroj { get; set; }
    }
}
