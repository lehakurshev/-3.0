using Microsoft.AspNetCore.Mvc;
using Npgsql;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ViewPlayersController : Controller
    {
        public IActionResult Index()
        {
            List<string> names = new List<string>();
            List<string> surnames = new List<string>();
            List<string> teamNames = new List<string>();
            List<string> genders = new List<string>();
            List<string> dateOfBirths = new List<string>();
            List<string> countrys = new List<string>();

            List<Player> players = new List<Player>();

            string connString = "PORT=5432;DATABASE=football players;HOST=localhost;USER ID=postgres;PASSWORD=postgres";

            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (var command = new NpgsqlCommand("SELECT * FROM easy_player", conn))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Получение данных из каждой строки
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string surname = reader.GetString(2);
                            string teamName = reader.GetString(3);
                            string gender = reader.GetString(4); // Преобразуем в строку
                            DateTime dateOfBirth = reader.GetDateTime(5);
                            string country = reader.GetString(6); // Преобразуем в строку

                            var player = new Player();

                            player.Id = id;
                            player.Name = name;
                            player.Surname = surname;
                            player.TeamName = teamName;
                            player.Gender = gender;
                            player.DateOfBirth = dateOfBirth;
                            player.Country = country;



                            players.Add(player);
                        }
                    }
                }

                conn.Close();
            }



            
            ViewBag.Players = players;

            return View();
        }

        public IActionResult ChangePlayer(string id)
        {
            //Console.WriteLine(id);
            return Redirect($"/ChangePlayer/Player?playerId={int.Parse(id)}");
        }
    }
}
