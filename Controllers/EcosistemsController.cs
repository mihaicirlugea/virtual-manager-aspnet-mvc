using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PAW_PROJECT.Models;
using PAW_PROJECT.Services;

namespace PAW_PROJECT.Controllers
{
    [Authorize]
    public class EcosistemsController : Controller
    {
        private readonly IEcosistemService _ecosistemService;
        public EcosistemsController(IEcosistemService ecosistemService) => _ecosistemService = ecosistemService;

        public async Task<IActionResult> Index() => View(await _ecosistemService.GetAllEcosistemsAsync());

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Denumire,ObiectivMacro")] Ecosistem ecosistem)
        {
            // Eliminăm erorile de validare pentru listele de navigare (Activitati/FluxCapitals) 
            // care nu vin din formular, altfel ModelState.IsValid va fi mereu false.
            ModelState.Remove("Activitati");
            ModelState.Remove("FluxCapitals");

            if (ModelState.IsValid)
            {
                await _ecosistemService.CreateEcosistemAsync(ecosistem);
                return RedirectToAction(nameof(Index));
            }
            return View(ecosistem);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var ecosistem = await _ecosistemService.GetEcosistemByIdAsync(id.Value);
            if (ecosistem == null) return NotFound();
            return View(ecosistem);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var ecosistem = await _ecosistemService.GetEcosistemByIdAsync(id.Value);
            return ecosistem == null ? NotFound() : View(ecosistem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Denumire,ObiectivMacro")] Ecosistem ecosistem)
        {
            if (id != ecosistem.Id) return NotFound();

            ModelState.Remove("Activitati");
            ModelState.Remove("FluxCapitals");

            if (ModelState.IsValid)
            {
                await _ecosistemService.UpdateEcosistemAsync(ecosistem);
                return RedirectToAction(nameof(Index));
            }
            return View(ecosistem);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var ecosistem = await _ecosistemService.GetEcosistemByIdAsync(id.Value);
            return ecosistem == null ? NotFound() : View(ecosistem);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _ecosistemService.DeleteEcosistemAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}