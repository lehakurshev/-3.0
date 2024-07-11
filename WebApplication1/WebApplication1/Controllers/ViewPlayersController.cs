using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class ViewPlayersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
