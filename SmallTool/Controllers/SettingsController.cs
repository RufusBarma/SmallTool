using Microsoft.AspNetCore.Mvc;

namespace SmallTool.Controllers
{
    public class SettingsController: Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}