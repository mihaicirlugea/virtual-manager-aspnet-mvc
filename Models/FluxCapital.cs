using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PAW_PROJECT.Models
{
    public class FluxCapital
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Introduceți suma tranzacției.")]
        [Range(0.01, 1000000, ErrorMessage = "Suma trebuie să fie pozitivă.")]
        [Display(Name = "Sumă (RON)")]
        public decimal SumaRON { get; set; }

        [Required]
        [Display(Name = "Tip (Venit/Cheltuială)")]
        public int Vector { get; set; } // 1 sau -1

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataTimp { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Alegeți ecosistemul asociat.")]
        public int EcosistemId { get; set; }

        [ForeignKey("EcosistemId")]
        public virtual Ecosistem? Ecosistem { get; set; }
    }
}