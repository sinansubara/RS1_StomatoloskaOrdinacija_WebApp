using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StomatoloskaOrdinacija.Web.ViewModels.Administracija
{
    public class AdministracijaDodajDrzavu
    {
        [Required]
        [StringLength(100)]
        public string Naziv { get; set; }
    }
}
