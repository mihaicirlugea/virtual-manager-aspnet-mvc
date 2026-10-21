using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PAW_PROJECT.Models;
using PAW_PROJECT.Services;

namespace PAW_PROJECT.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IActivitateService _taskService;
        private readonly IFluxCapitalService _financeService;

        public HomeController(IActivitateService taskService, IFluxCapitalService financeService)
        {
            _taskService = taskService;
            _financeService = financeService;
        }

        public IActionResult Index() => View();

        public async Task<IActionResult> Dashboard()
        {
            var taskuri = await _taskService.GetTasksAsync();
            var tranzactii = await _financeService.GetTransactionsAsync();

            // 1. Calcul date pentru Grafic Finanțe (Ecosisteme)
            var dateFinante = tranzactii
                .Where(t => t.Vector == -1)
                .GroupBy(t => t.Ecosistem?.Denumire ?? "General")
                .Select(g => new { Nume = g.Key, Total = g.Sum(t => t.SumaRON) })
                .ToList();

            // 2. Calcul date pentru Grafic Activități (Ultimele 7 zile)
            var ultimele7Zile = Enumerable.Range(0, 7)
                .Select(i => DateTime.Now.Date.AddDays(-i))
                .OrderBy(d => d)
                .ToList();

            var dateActivitati = ultimele7Zile.Select(zi => new
            {
                NumeZi = zi.ToString("dddd"),
                TotalOre = taskuri
                    .Where(t => t.DataInregistrare.Date == zi.Date)
                    .Sum(t => t.EfortOre)
            }).ToList();

            var model = new DashboardViewModel
            {
                TaskuriActive = taskuri.Count(t => t.Status != 2),
                BalantaTotala = tranzactii.Sum(t => t.SumaRON * t.Vector),
                CheltuieliLuna = tranzactii
                    .Where(t => t.Vector == -1 && t.DataTimp.Month == DateTime.Now.Month)
                    .Sum(t => t.SumaRON),

                NumeEcosisteme = dateFinante.Select(x => x.Nume).ToArray(),
                SumeEcosisteme = dateFinante.Select(x => x.Total).ToArray(),

                EticheteZile = dateActivitati.Select(x => x.NumeZi).ToArray(),
                EfortZile = dateActivitati.Select(x => x.TotalOre).ToArray()
            };

            return View(model);
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}