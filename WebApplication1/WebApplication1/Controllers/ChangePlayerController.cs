using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Numerics;
using System.Reflection;
using WebApplication1.DB;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ChangePlayerController : Controller
    {
        public IActionResult Player(int playerId)
        {
            ViewBag.TeamNames = DBfunctions.GetTeamNames();

            return View(DBfunctions.GetPlayerById(playerId));
        }

        [HttpPost]
        public IActionResult UpdatePlayer(Player player)
        {
            if (ModelState.IsValid)
            {
                DBfunctions.UpdatePlayerData(player);
                ViewBag.CurrentPage = 1;
                return Redirect("/viewPlayers?page=1");
            }

            ViewBag.TeamNames = DBfunctions.GetTeamNames();


            return View("Player");
        }


    }

}

