using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using Npgsql;

namespace WebApplication1.Controllers
{
    public class CreatePlayerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPlayer(Player player)
        {
            string connString = "PORT=5432;DATABASE=football players;HOST=localhost;USER ID=postgres;PASSWORD=postgres";

            using (var conn = new NpgsqlConnection())
            {
                conn.ConnectionString = connString;
                conn.Open();

                using (var command = new NpgsqlCommand("INSERT INTO easy_player (name, surname, team_name, gender, date_of_birth, country) VALUES (@Name, @Surname, @TeamName, @Gender, @DateOfBirth, @Country)", conn))
                {
                    command.Parameters.AddWithValue("@Name", player.Name);
                    command.Parameters.AddWithValue("@Surname", player.Surname);
                    command.Parameters.AddWithValue("@TeamName", player.TeamName);
                    command.Parameters.AddWithValue("@Gender", player.Gender.ToString()); 
                    command.Parameters.AddWithValue("@DateOfBirth", player.DateOfBirth);
                    command.Parameters.AddWithValue("@Country", player.Country.ToString()); 

                    command.ExecuteNonQuery();
                }

                    conn.Close();
            }


                Console.WriteLine(player.Name);
            Console.WriteLine(player.Surname);
            Console.WriteLine(player.TeamName);
            Console.WriteLine(player.Gender);
            Console.WriteLine(player.Country);
            Console.WriteLine(player.DateOfBirth);
            return Redirect("/viewPlayers");
        }
    }
}
