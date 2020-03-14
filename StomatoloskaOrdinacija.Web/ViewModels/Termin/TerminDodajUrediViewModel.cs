using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StomatoloskaOrdinacija.Web.ViewModels.Termin
{
    public class TerminDodajUrediViewModel
    {
        [Required]
        public int PacijentId { get; set; }

        [Required]
        public DateTime Datum { get; set; }


        public string datumstring { get; set; }

        [Required]
        public string Vrijeme { get; set; }

        [Required]
        [StringLength(200)]
        public string Razlog { get; set; }

        [Required]
        public bool Hitan { get; set; }

    }
}
