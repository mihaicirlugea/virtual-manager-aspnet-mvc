using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PAW_PROJECT.Models;
using PAW_PROJECT.Services;

namespace PAW_PROJECT.Controllers
{
    [Authorize]
    public class FluxCapitalsController : Controller
    {
        private readonly IFluxCapitalService _financeService;
        private readonly IEcosistemService _ecosistemService;

        public FluxCapitalsController(IFluxCapitalService financeService, IEcosistemService ecosistemService)
        {
            _financeService = financeService;
            _ecosistemService = ecosistemService;
        }

        public async Task<IActionResult> Index() => View(await _financeService.GetTransactionsAsync());

        public async Task<IActionResult> Create()
        {
            var ecosisteme = await _ecosistemService.GetAllEcosistemsAsync();
            ViewData["EcosistemId"] = new SelectList(ecosisteme, "Id", "Denumire");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FluxCapital flux)
        {
            if (ModelState.IsValid)
            {
                await _financeService.CreateTransactionAsync(flux);
                return RedirectToAction(nameof(Index));
            }

            var ecosisteme = await _ecosistemService.GetAllEcosistemsAsync();
            ViewData["EcosistemId"] = new SelectList(ecosisteme, "Id", "Denumire", flux.EcosistemId);
            return View(flux);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var flux = await _financeService.GetTransactionByIdAsync(id.Value);
            if (flux == null) return NotFound();

            var ecosisteme = await _ecosistemService.GetAllEcosistemsAsync();
            ViewData["EcosistemId"] = new SelectList(ecosisteme, "Id", "Denumire", flux.EcosistemId);
            return View(flux);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FluxCapital flux)
        {
            if (id != flux.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _financeService.UpdateTransactionAsync(flux);
                return RedirectToAction(nameof(Index));
            }

            var ecosisteme = await _ecosistemService.GetAllEcosistemsAsync();
            ViewData["EcosistemId"] = new SelectList(ecosisteme, "Id", "Denumire", flux.EcosistemId);
            return View(flux);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _financeService.DeleteTransactionAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}