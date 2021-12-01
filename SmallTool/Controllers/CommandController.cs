using Microsoft.AspNetCore.Mvc;

namespace SmallTool.Controllers
{
    public class CommandController : Controller
    {
        [HttpPost]
        public IActionResult RunCommand(string command, string branch)
        {
            return Ok();
        }
    }
}