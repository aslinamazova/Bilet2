using Bizland.Areas.Admin.ViewModels;
using Bizland.DAL;
using Bizland.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bizland.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamsController : Controller
    {
        private readonly AppDbContext _context;

        public TeamsController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Team> teams= await _context.Teams.Where(t => !t.IsDeleted).OrderByDescending(t => t.Id).ToListAsync();
            return View(teams);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Create(CreateTeamVM teamVM)
        {
            return Json(teamVM);

        }

    }
}
