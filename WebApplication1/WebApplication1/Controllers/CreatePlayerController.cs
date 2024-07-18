using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using Npgsql;
using System.Numerics;
using WebApplication1.DB;

namespace WebApplication1.Controllers
{
    public class CreatePlayerController : Controller
    {
        public IActionResult Index()
        {
            

            ViewBag.TeamNames = DBfunctions.GetTeamNames();

            return View();
        }

        [HttpPost]
        public IActionResult AddPlayer(Player player)
        {

            if (ModelState.IsValid)
            {
                DBfunctions.UpdatePlayerData(player, includeId: false);

                return Redirect("/viewPlayers?page=1");
            }

            

            // пчему-то то внутри Index не вызывается....
            ViewBag.TeamNames = DBfunctions.GetTeamNames();

            return View("Index");
        }
    }
}
