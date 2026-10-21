using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PAW_PROJECT.Models
{
    public class ObiectivIntermediar
    {
        [Key]
        public int Id { get; set; }
        public decimal TintaAbsoluta { get; set; }
        public decimal ProgresCurent { get; set; }
        public string UnitateMasura { get; set; }

        public int EcosistemId { get; set; }
        [ForeignKey("EcosistemId")]
        public virtual Ecosistem Ecosistem { get; set; }
    }
}