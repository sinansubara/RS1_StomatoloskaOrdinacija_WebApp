using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StomatoloskaOrdinacija.Data.EntityModels
{
    [Table("Pregledi")]
    public class Pregled
    {
        [Key]
        public int PregledId { get; set; }

        [ForeignKey(nameof(Stomatolog))]
        public int StomatologId { get; set; }
        public Stomatolog Stomatolog { get; set; }

        [ForeignKey(nameof(Termin))]
        public int TerminId { get; set; }
        public Termin Termin { get; set; }

        [ForeignKey(nameof(Pacijent))]
        public int PacijentId { get; set; }
        public Pacijent Pacijent { get; set; }

        [ForeignKey(nameof(Terapija))]
        public int TerapijaId { get; set; }
        public Terapija Terapija { get; set; }

        [ForeignKey(nameof(UspostavljenaDijagnoza))]
        public int UspostavljenaDijagnozaId { get; set; }
        public UspostavljenaDijagnoza UspostavljenaDijagnoza { get; set; }

        [Required]
        [Column(TypeName = "SMALLINT")]
        [Display(Name = "Trajanje pregleda")]
        public int TrajanjePregleda { get; set; }

        [Required]
        [StringLength(200)]
        public string Napomena { get; set; }

    }
}