using System.ComponentModel.DataAnnotations;

namespace PAW_PROJECT.Models
{
    public class Ecosistem
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Denumirea este obligatorie")]
        [StringLength(100)]
        public string Denumire { get; set; } = string.Empty;

        // Adăugăm ? pentru a marca proprietatea ca fiind opțională
        public string? ObiectivMacro { get; set; }

        // Relații 1:N - Folosim ? pentru a evita erorile de validare la Create/Edit
        public virtual ICollection<Activitate>? Activitati { get; set; }
        public virtual ICollection<FluxCapital>? Fluxuri { get; set; }
        public virtual ICollection<ObiectivIntermediar>? Obiective { get; set; }
    }
}