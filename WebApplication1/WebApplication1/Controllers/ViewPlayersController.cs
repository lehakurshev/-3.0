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

                            /*Console.WriteLine($"ID: {id}");
                            Console.WriteLine($"Имя: {name}");
                            Console.WriteLine($"Фамилия: {surname}");
                            Console.WriteLine($"Команда: {teamName}");
                            Console.WriteLine($"Пол: {gender}");
                            Console.WriteLine($"Дата рождения: {dateOfBirth}");
                            Console.WriteLine($"Страна: {country}");
                            Console.WriteLine("-------");*/
                        }
                    }
                }

                conn.Close();
            }



            List<string> footballers = new List<string>();

            for (int i = 1; i <= 100; i++)
            {
                footballers.Add(i.ToString());
            }

            ViewBag.Footballers = footballers;

            return View();
        }

        public IActionResult ChangePlayer(string id)
        {
            //Console.WriteLine(id);
            return Redirect($"/ChangePlayer/Player?playerId={int.Parse(id)}");
        }
    }
}
