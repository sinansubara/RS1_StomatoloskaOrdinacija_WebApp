using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StomatoloskaOrdinacija.Data.EntityModels
{
    [Table("UspostavljenaDijagnoza")]
    public class UspostavljenaDijagnoza
    {
        [Key]
        public int UspostavljenaDijagnozaId { get; set; }

        [ForeignKey(nameof(Dijagnoza))]
        public int DijagnozaId { get; set; }
        public Dijagnoza Dijagnoza { get; set; }

        [Required]
        [Column(TypeName = "TINYINT")] 
        public int JacinaDijagnoze { get; set; }

        [Required]
        [StringLength(300)]
        public string Napomena { get; set; }
    }
}