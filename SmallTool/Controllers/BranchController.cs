using ElectronNET.API;
using Microsoft.AspNetCore.Mvc;

namespace SmallTool.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BranchController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post()
        {
            Electron.Dialog.ShowErrorBox("An Error Message", "Demonstrating an error message.");
            return Ok();
        }
    }
}