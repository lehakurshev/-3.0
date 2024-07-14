using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using Npgsql;
using System.Numerics;

namespace WebApplication1.Controllers
{
    public class CreatePlayerController : Controller
    {
        public IActionResult Index()
        {
            // Получение списка команд из базы данных
            string connString = "PORT=5432;DATABASE=football players;HOST=localhost;USER ID=postgres;PASSWORD=postgres";
            var teamNames = new List<string>();

            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var command = new NpgsqlCommand("SELECT DISTINCT team_name FROM easy_player", conn))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            teamNames.Add(reader.GetString(0));
                        }
                    }
                }
                conn.Close();
            }

            // Передача списка команд в представление
            ViewBag.TeamNames = teamNames;

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


            
            return Redirect("/viewPlayers");
        }
    }
}
