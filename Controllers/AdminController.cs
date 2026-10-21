using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PAW_PROJECT.Models;

namespace PAW_PROJECT.Controllers
{
    [Authorize(Roles = "Admin")] // DOAR administratorul are voie aici
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // GET: /Admin/Users
        public async Task<IActionResult> Users()
        {
            // Extreagem toți utilizatorii din baza de date
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }
    }
}