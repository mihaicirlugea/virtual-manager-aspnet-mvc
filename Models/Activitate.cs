using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PAW_PROJECT.Models
{
    public class Activitate
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Numele activității este obligatoriu.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Denumirea trebuie să aibă între 3 și 100 de caractere.")]
        public string Denumire { get; set; } = string.Empty;

        [Required(ErrorMessage = "Specificați efortul în ore.")]
        [Range(0.1, 500, ErrorMessage = "Efortul trebuie să fie între 0.1 și 500 ore.")]
        [Display(Name = "Efort (Ore)")]
        public decimal EfortOre { get; set; }

        [Required]
        public int Status { get; set; } // 0=Backlog, 1=In Progress, 2=Done

        [Required]
        public DateTime DataInregistrare { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Selectarea unui ecosistem este obligatorie.")]
        public int EcosistemId { get; set; }

        [ForeignKey("EcosistemId")]
        public virtual Ecosistem? Ecosistem { get; set; }

        public virtual ICollection<Eticheta>? Etichete { get; set; }
    }
}