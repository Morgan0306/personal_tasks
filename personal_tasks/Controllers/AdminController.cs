using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using personal_tasks.Models;

namespace personal_tasks.Controllers
{
    public class AdminController : Controller
    {
        private readonly Personal_TasksContext _context;

        public AdminController(Personal_TasksContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> AccountManagement()
        {
            var users = await _context.Users
                              .Include(u => u.Role)
                              .ToListAsync();

            return View(users);
        }
    }
}
