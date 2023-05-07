using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NetMaps.Models;
using System.Diagnostics;

namespace NetMaps.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GeoContext _context;

        public HomeController(ILogger<HomeController> logger, IOptions<SettingsConfig> settings)
        {
            _logger = logger;
            _context = new GeoContext(settings);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            var meusPontos = _context.Pontos;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}