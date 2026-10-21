using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PAW_PROJECT.Models
{
    public class Risipa
    {
        [Key]
        public int Id { get; set; }
        public decimal CostPierdere { get; set; }
        public string Cauza { get; set; }
        public DateTime DataIncident { get; set; }

        public int EcosistemId { get; set; }
        [ForeignKey("EcosistemId")]
        public virtual Ecosistem Ecosistem { get; set; }
    }
}