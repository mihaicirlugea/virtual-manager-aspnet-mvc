using System;

namespace PAW_PROJECT.Models
{
    public class DashboardViewModel
    {
        public int TaskuriActive { get; set; }
        public decimal BalantaTotala { get; set; }
        public decimal CheltuieliLuna { get; set; }

        // Date pentru Grafic Plăcintă (Finanțe)
        public string[] NumeEcosisteme { get; set; } = Array.Empty<string>();
        public decimal[] SumeEcosisteme { get; set; } = Array.Empty<decimal>();

        // Date pentru Grafic Bare (Activități)
        public string[] EticheteZile { get; set; } = Array.Empty<string>();
        public decimal[] EfortZile { get; set; } = Array.Empty<decimal>();
    }
}