using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Numerics;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ChangePlayerController : Controller
    {


        public IActionResult Player(int playerId)
        {
            
            string connString = "PORT=5432;DATABASE=football players;HOST=localhost;USER ID=postgres;PASSWORD=postgres";

            Player player = new Player();

            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (var command = new NpgsqlCommand("SELECT * FROM easy_player WHERE id = @playerId", conn))
                {
                    command.Parameters.AddWithValue("playerId", playerId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            player.Id = reader.GetInt32(0);
                            player.Name = reader.GetString(1);
                            player.Surname = reader.GetString(2);
                            player.TeamName = reader.GetString(3);
                            player.Gender = reader.GetString(4);
                            player.DateOfBirth = reader.GetDateTime(5);
                            player.Country = reader.GetString(6);
                        }
                    }
                }

                conn.Close();
            }

            return View(player);
        }


        public IActionResult Index(int playerId)
        {
            Console.WriteLine(111);
            return View();

        }


        [HttpPost]
        public IActionResult UpdatePlayer(Player player)
        {
            Console.WriteLine($"Player {player.Id}");

            string connString = "PORT=5432;DATABASE=football players;HOST=localhost;USER ID=postgres;PASSWORD=postgres";

            var playerId = player.Id;

            using (var conn = new NpgsqlConnection())
            {
                conn.ConnectionString = connString;
                conn.Open();

                using (var command = new NpgsqlCommand("UPDATE easy_player SET name = @Name, surname = @Surname, team_name = @TeamName, gender = @Gender, date_of_birth = @DateOfBirth, country = @Country WHERE id = @PlayerId", conn))
                {
                    command.Parameters.AddWithValue("@Name", player.Name);
                    command.Parameters.AddWithValue("@Surname", player.Surname);
                    command.Parameters.AddWithValue("@TeamName", player.TeamName);
                    command.Parameters.AddWithValue("@Gender", player.Gender.ToString());
                    command.Parameters.AddWithValue("@DateOfBirth", player.DateOfBirth);
                    command.Parameters.AddWithValue("@Country", player.Country.ToString());
                    command.Parameters.AddWithValue("@PlayerId", playerId);

                    command.ExecuteNonQuery();
                }

                conn.Close();
            }

            return Redirect("/viewPlayers");
        }


    }


}

