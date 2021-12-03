using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmallTool.Models;

namespace SmallTool.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BranchHandler _branchHandler;

        public HomeController(ILogger<HomeController> logger, BranchHandler branchHandler)
        {
            _logger = logger;
            _branchHandler = branchHandler;
        }

        public IActionResult Index()
        {
            _branchHandler.RefreshBuilds();
            return View(_branchHandler.BranchTuples);
        }

        public IActionResult About()
        {
            return View();
        }

        public JsonResult GetBranchTuples()
        {
            return Json(_branchHandler.BranchTuples, new JsonSerializerOptions{ReferenceHandler = ReferenceHandler.Preserve});
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}