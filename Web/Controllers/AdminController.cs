using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ComicStore.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserService _service;

        public AdminController(IUserService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index(string role)
        {
            ViewData["Role"] = role;
            return View(_service.GetAllUser(role));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> LockUser(string id, string userRole)
        {
            await _service.LockUser(id);
            return RedirectToAction("Index", new {role = userRole});
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UnlockUser(string id, string userRole)
        {
            await _service.UnlockUser(id);
            return RedirectToAction("Index", new { role = userRole});
        }
    }
}
