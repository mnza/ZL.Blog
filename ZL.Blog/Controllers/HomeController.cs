using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using ZL.Blog.Models;

namespace ZL.Blog.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOptionsSnapshot<CollectConfig> _collectConfig;
        public HomeController(IOptionsSnapshot<CollectConfig> collectConfig)
        {
            this._collectConfig = collectConfig;
        }

        public IActionResult Index()
        {
            return View(_collectConfig.Value);
        }

        public IActionResult Privacy()
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