using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using personal_tasks.Models;
using System.Security.Claims;

namespace personal_tasks.Controllers
{
    public class DashboardController : Controller
    {
        private readonly Personal_TasksContext _context;

        public DashboardController(Personal_TasksContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        { 
            
            return View("~/Views/Home/Dashboard.cshtml");
        }
    }
}
