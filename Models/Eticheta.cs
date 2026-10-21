using System.ComponentModel.DataAnnotations;

namespace PAW_PROJECT.Models
{
    public class Eticheta
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Denumire { get; set; }

        public virtual ICollection<Activitate> Activitati { get; set; }
    }
}