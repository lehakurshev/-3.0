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

            ViewBag.TeamNames = DBfunctions.MakeARequestToTheDB1("SELECT DISTINCT team_name FROM easy_player");


            return View(DBfunctions.MakeARequestToTheDB2("SELECT * FROM easy_player WHERE id = @playerId", playerId));
        }


        public IActionResult Index(int playerId)
        {
            Console.WriteLine(111);
            return View();

        }


        [HttpPost]
        public IActionResult UpdatePlayer(Player player)
        {
            var request = "UPDATE easy_player SET name = @Name, surname = @Surname, team_name = @TeamName, gender = @Gender, date_of_birth = @DateOfBirth, country = @Country WHERE id = @PlayerId";
            DBfunctions.MakeARequestToTheDB2(request, player);



            return Redirect("/viewPlayers");
        }


    }


}

