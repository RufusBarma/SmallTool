using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmallTool.Extensions;
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
            return View(_branchHandler);
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}