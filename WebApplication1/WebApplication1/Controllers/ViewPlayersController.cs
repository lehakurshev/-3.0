using Microsoft.AspNetCore.Mvc;
using Npgsql;
using WebApplication1.DB;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ViewPlayersController : Controller
    {
        public IActionResult Index()
        {

            
            ViewBag.Players = DBfunctions.GetPlayers("SELECT * FROM easy_player");

            return View();
        }

        public IActionResult ChangePlayer(string id)
        {

            return Redirect($"/ChangePlayer/Player?playerId={int.Parse(id)}");
        }
    }
}
