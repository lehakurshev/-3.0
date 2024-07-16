using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Numerics;
using WebApplication1.DB;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ChangePlayerController : Controller
    {


        public IActionResult Player(int playerId)
        {

            ViewBag.TeamNames = DBfunctions.GetTeamNames("SELECT DISTINCT team_name FROM easy_player");


            return View(DBfunctions.GetPlayerById("SELECT * FROM easy_player WHERE id = @playerId", playerId));
        }


        public IActionResult Index(int playerId)
        {
            Console.WriteLine(111);
            return View();

        }


        [HttpPost]
        public IActionResult UpdatePlayer(Player player)
        {
            if (ModelState.IsValid)
            {
                var request = "UPDATE easy_player SET name = @Name, surname = @Surname, team_name = @TeamName, gender = @Gender, date_of_birth = @DateOfBirth, country = @Country WHERE id = @PlayerId";
                DBfunctions.UpdatePlayerDataWithId(request, player);



                return Redirect("/viewPlayers");
            }

            ViewBag.TeamNames = DBfunctions.GetTeamNames("SELECT DISTINCT team_name FROM easy_player");
            return View("Player");
        }


    }


}

