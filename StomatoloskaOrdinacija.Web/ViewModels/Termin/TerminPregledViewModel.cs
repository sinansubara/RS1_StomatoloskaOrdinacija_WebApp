using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StomatoloskaOrdinacija.Web.ViewModels.Termin
{
    public class TerminPregledViewModel
    {
        public int TerminId { get; set; }
        public int PacijentId { get; set; }
        public string Pacijent { get; set; }
        public DateTime Datum { get; set; }
        public DateTime Vrijeme { get; set; }
        public string Razlog { get; set; }
        public string Hitan { get; set; }
        public string Odobren { get; set; }
        public string NaCekanju { get; set; }
    }
}
