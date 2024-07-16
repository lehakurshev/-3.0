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
            Console.WriteLine(1);

            ViewBag.TeamNames = DBfunctions.GetTeamNames("SELECT DISTINCT team_name FROM easy_player");

            return View();
        }

        [HttpPost]
        public IActionResult AddPlayer(Player player)
        {

            if (ModelState.IsValid)
            {

                

                var request = "INSERT INTO easy_player (name, surname, team_name, gender, date_of_birth, country) VALUES (@Name, @Surname, @TeamName, @Gender, @DateOfBirth, @Country)";

                DBfunctions.UpdatePlayerDataWithOutId(request, player);




                return Redirect("/viewPlayers");
            }

            

            Console.WriteLine(player.DateOfBirth);


            // пчему-то то что внутри Index не вызывается....
            ViewBag.TeamNames = DBfunctions.GetTeamNames("SELECT DISTINCT team_name FROM easy_player");

            return View("Index");

            

            
        }
    }
}
