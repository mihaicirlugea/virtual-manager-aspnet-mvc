using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PAW_PROJECT.Models;
using PAW_PROJECT.Services;

namespace PAW_PROJECT.Controllers
{
    [Authorize]
    public class ActivitatiController : Controller
    {
        private readonly IActivitateService _taskService;
        private readonly IEcosistemService _ecosistemService;

        public ActivitatiController(IActivitateService taskService, IEcosistemService ecosistemService)
        {
            _taskService = taskService;
            _ecosistemService = ecosistemService;
        }

        public async Task<IActionResult> Index() => View(await _taskService.GetTasksAsync());

        public async Task<IActionResult> Create()
        {
            var ecosisteme = await _ecosistemService.GetAllEcosistemsAsync();
            ViewData["EcosistemId"] = new SelectList(ecosisteme, "Id", "Denumire");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Activitate activitate)
        {
            if (ModelState.IsValid)
            {
                await _taskService.CreateTaskAsync(activitate);
                return RedirectToAction(nameof(Index));
            }

            var ecosisteme = await _ecosistemService.GetAllEcosistemsAsync();
            ViewData["EcosistemId"] = new SelectList(ecosisteme, "Id", "Denumire", activitate.EcosistemId);
            return View(activitate);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var activitate = await _taskService.GetTaskByIdAsync(id.Value);
            if (activitate == null) return NotFound();

            var ecosisteme = await _ecosistemService.GetAllEcosistemsAsync();
            ViewData["EcosistemId"] = new SelectList(ecosisteme, "Id", "Denumire", activitate.EcosistemId);

            return View(activitate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Activitate activitate)
        {
            if (id != activitate.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _taskService.UpdateTaskAsync(activitate);
                return RedirectToAction(nameof(Index));
            }

            var ecosisteme = await _ecosistemService.GetAllEcosistemsAsync();
            ViewData["EcosistemId"] = new SelectList(ecosisteme, "Id", "Denumire", activitate.EcosistemId);
            return View(activitate);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _taskService.DeleteTaskAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}