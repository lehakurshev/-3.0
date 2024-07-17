using Npgsql;
using System.Numerics;
using WebApplication1.Models;

namespace WebApplication1.DB;

public static class DBfunctions
{
    private static string connString;

    static DBfunctions()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        IConfigurationRoot configuration = builder.Build();
        connString = configuration.GetConnectionString("DefaultConnection");
    }

    private static void ExecuteDBRequest(string request, Action<NpgsqlCommand> addParameters, Action<NpgsqlDataReader> readResult)
    {
        try
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var command = new NpgsqlCommand(request, conn))
                {
                    addParameters(command);

                    using (var reader = command.ExecuteReader())
                    {
                        readResult(reader);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // exceptions 
        }
    }

    // не удачно создалась таблица -> чтоб переместить объект в конец его приходится одалять и создавать заново... потом надо исправить
    public static void UpdatePlayerData(Player player, bool includeId = true, string requestDelete = Requests.RequestDeletePlayerData, string requestInsert = Requests.RequestInsertPlayerData)
    {
        // Удаление игрока
        ExecuteDBRequest(requestDelete,
            command =>
            {
                command.Parameters.AddWithValue("@PlayerId", player.Id);
            },
            reader => { });

        // Вставка обновленного игрока
        ExecuteDBRequest(requestInsert,
            command =>
            {
                command.Parameters.AddWithValue("@Name", player.Name);
                command.Parameters.AddWithValue("@Surname", player.Surname);
                command.Parameters.AddWithValue("@TeamName", player.TeamName);
                command.Parameters.AddWithValue("@Gender", player.Gender.ToString());
                command.Parameters.AddWithValue("@DateOfBirth", player.DateOfBirth);
                command.Parameters.AddWithValue("@Country", player.Country.ToString());
                if (includeId)
                {
                    command.Parameters.AddWithValue("@PlayerId", player.Id);
                }
            },
            reader => { });
    }

    public static object GetPlayerById(int playerId, string request = Requests.RequestGetPlayerById)
    {
        var player = new Player();

        ExecuteDBRequest(request,
            command => command.Parameters.AddWithValue("playerId", playerId),
            reader =>
            {
                if (reader.Read())
                {
                    player.Id = reader.GetInt32(0);
                    player.Name = reader.GetString(1);
                    player.Surname = reader.GetString(2);
                    player.TeamName = reader.GetString(3);
                    player.Gender = reader.GetString(4);
                    player.DateOfBirth = reader.GetDateTime(5);
                    player.Country = reader.GetString(6);
                }
            });

        return player;
    }

    


    public static object GetTeamNames(string request = Requests.RequestGetTeamNames) // GetTeamNames("SELECT DISTINCT team_name FROM easy_player")
    {
        var teamNames = new List<string>();

        ExecuteDBRequest(request,
            command => { },
            reader =>
            {
                while (reader.Read())
                {
                    teamNames.Add(reader.GetString(0));
                }
            });

        return teamNames;
    }

    



    public static List<Player> GetPlayers(string request = Requests.RequestGetPlayers)
    {
        var players = new List<Player>();

        ExecuteDBRequest(request,
            command => { },
            reader =>
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    string surname = reader.GetString(2);
                    string teamName = reader.GetString(3);
                    string gender = reader.GetString(4);
                    DateTime dateOfBirth = reader.GetDateTime(5);
                    string country = reader.GetString(6);

                    var player = new Player
                    {
                        Id = id,
                        Name = name,
                        Surname = surname,
                        TeamName = teamName,
                        Gender = gender,
                        DateOfBirth = dateOfBirth,
                        Country = country
                    };

                    players.Add(player);
                }
            });

        // Сортировка списка игроков по Id
        players.Sort((p1, p2) => p1.Id.CompareTo(p2.Id));

        return players;
    }



}

public static class Requests
{
    // чтоб не переписывать для новой таблицы
    public const string Table = "players";

    public const string RequestGetPlayerById = $"SELECT * FROM {Table} WHERE id = @playerId";
    public const string RequestGetTeamNames = $"SELECT DISTINCT team_name FROM {Table}";
    public const string RequestGetPlayers = $"SELECT * FROM {Table}";

    //public const string RequestUpdatePlayerData = $"UPDATE {Table} SET name = @Name, surname = @Surname, team_name = @TeamName, gender = @Gender, date_of_birth = @DateOfBirth, country = @Country WHERE id = @PlayerId";

    public const string RequestDeletePlayerData = $"DELETE FROM {Table} WHERE id = @PlayerId";
    public const string RequestInsertPlayerData = $"INSERT INTO {Table} (name, surname, team_name, gender, date_of_birth, country) VALUES (@Name, @Surname, @TeamName, @Gender, @DateOfBirth, @Country)";

}
