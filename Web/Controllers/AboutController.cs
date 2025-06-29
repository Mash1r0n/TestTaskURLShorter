using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("about")]
    public class AboutController : Controller
    {
        private readonly string _filePath;

        public AboutController(IWebHostEnvironment env)
        {
            _filePath = Path.Combine(env.WebRootPath, "files", "description.txt");
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            string content = await LoadDescriptionAsync();
            ViewBag.Description = content;
            ViewBag.IsAdmin = User.IsInRole("Admin");
            return View();
        }

        [Authorize(Roles = "Admin", AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<IActionResult> Update(string newContent)
        {
            await SaveDescriptionAsync(newContent);
            return RedirectToAction("Index");
        }

        private async Task<string> LoadDescriptionAsync()
        {
            return System.IO.File.Exists(_filePath)
                ? await System.IO.File.ReadAllTextAsync(_filePath)
                : "There is no information for now";
        }

        private async Task SaveDescriptionAsync(string content)
        {
            await System.IO.File.WriteAllTextAsync(_filePath, content ?? string.Empty);
        }
    }
}