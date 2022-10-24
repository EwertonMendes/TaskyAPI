using Microsoft.AspNetCore.Mvc;

namespace Tasky.Controllers
{
    public class ListController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            return Ok();
        }
    }
}
