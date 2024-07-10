

using System;
using Npgsql;

public class Programm
{
    public static void Main(string[] args)
    {
        ///////////////////////////////////////////////////////////////

        var players = new PlayersInfo();

        var player1 = new Player("A", "B", "Team1", Gender.Man, new DateTime(), Country.Russia);

        players.AddPlayer(1, player1);

        //////////////////////////////////////////////////////////

        string connString = "PORT=5432;DATABASE=football players;HOST=localhost;USER ID=postgres;PASSWORD=postgres";

        using (var conn = new NpgsqlConnection())
        {
            conn.ConnectionString = connString;
            conn.Open();


            // Создание списка игроков
            var otherPlayers = new List<Player>
                {
                    new Player { Name = "Иван", Surname = "Иванов", TeamName = "Спартак", Gender = Gender.Man, DateOfBirth = new DateTime(1995, 1, 15), Country = Country.Russia },
                    new Player { Name = "Мария", Surname = "Петрова", TeamName = "Динамо", Gender = Gender.Woman, DateOfBirth = new DateTime(1998, 5, 20), Country = Country.Russia },
                    new Player { Name = "John", Surname = "Doe", TeamName = "Real Madrid", Gender = Gender.Man, DateOfBirth = new DateTime(1997, 10, 10), Country = Country.USA },
                };

            // Вставка игроков в таблицу
            using (var command = new NpgsqlCommand("INSERT INTO easy_player (name, surname, team_name, gender, date_of_birth, country) VALUES (@Name, @Surname, @TeamName, @Gender, @DateOfBirth, @Country)", conn))
            {
                foreach (var player in otherPlayers)
                {
                    command.Parameters.AddWithValue("@Name", player.Name);
                    command.Parameters.AddWithValue("@Surname", player.Surname);
                    command.Parameters.AddWithValue("@TeamName", player.TeamName);
                    command.Parameters.AddWithValue("@Gender", player.Gender.ToString()); // Преобразование Gender в строку
                    command.Parameters.AddWithValue("@DateOfBirth", player.DateOfBirth);
                    command.Parameters.AddWithValue("@Country", player.Country.ToString()); // Преобразование Country в строку

                    command.ExecuteNonQuery();
                }
            }
            conn.Close();
            Console.WriteLine("Table player created and populated with data.");
        }


        ///////////////////////////////////////////////////////////////////////////////


        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        app.MapGet("/", () => "Create football player!");

        app.Run();

        //////////////////////////////////////////////////////////

    }
}


