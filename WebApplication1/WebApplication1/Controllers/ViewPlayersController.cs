using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;
using WebApplication1.DB;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ViewPlayersController : Controller
    {
        public IActionResult Index(int page)
        {

            ViewBag.Players = DBfunctions.GetPlayers();
            ViewBag.curentPage = page;
            return View();
        }

        public IActionResult ChangePlayer(string id)
        {
            return Redirect($"/ChangePlayer/Player?playerId={int.Parse(id)}");
        }

        public IActionResult GoToThePage(int page)
        {
            ViewBag.curentPage = page;
            return Redirect($"/ViewPlayers?page={page}");
        }
    }
}
