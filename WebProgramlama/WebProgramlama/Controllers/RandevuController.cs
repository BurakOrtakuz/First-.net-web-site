using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebProgramlama.Models;

namespace WebProgramlama.Controllers
{
    public class RandevuController : Controller
    {
        private readonly ILogger<RandevuController> _logger;

        public RandevuController(ILogger<RandevuController> logger)
        {
            _logger = logger;
        }

        public IActionResult Randevual()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}