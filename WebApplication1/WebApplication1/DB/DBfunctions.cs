using Npgsql;
using System.Numerics;
using WebApplication1.Models;

namespace WebApplication1.DB;

public static class DBfunctions
{
    private static string connString;

    static DBfunctions()
    {
        connString = "PORT=5432;DATABASE=football players;HOST=localhost;USER ID=postgres;PASSWORD=postgres";
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
            // Handle exceptions (log, throw, etc.)
        }
    }

    public static object MakeARequestToTheDB1(string request)
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

    public static object MakeARequestToTheDB2(string request, int playerId)
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

    public static void MakeARequestToTheDB3(string request, Player player)
    {
        ExecuteDBRequest(request,
            command =>
            {
                command.Parameters.AddWithValue("@Name", player.Name);
                command.Parameters.AddWithValue("@Surname", player.Surname);
                command.Parameters.AddWithValue("@TeamName", player.TeamName);
                command.Parameters.AddWithValue("@Gender", player.Gender.ToString());
                command.Parameters.AddWithValue("@DateOfBirth", player.DateOfBirth);
                command.Parameters.AddWithValue("@Country", player.Country.ToString());
                command.Parameters.AddWithValue("@PlayerId", player.Id);
            },
            reader => { });
    }

    public static object MakeARequestToTheDB4(string request)
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

    public static void MakeARequestToTheDB5(string request, Player player)
    {
        ExecuteDBRequest(request,
            command =>
            {
                command.Parameters.AddWithValue("@Name", player.Name);
                command.Parameters.AddWithValue("@Surname", player.Surname);
                command.Parameters.AddWithValue("@TeamName", player.TeamName);
                command.Parameters.AddWithValue("@Gender", player.Gender.ToString());
                command.Parameters.AddWithValue("@DateOfBirth", player.DateOfBirth);
                command.Parameters.AddWithValue("@Country", player.Country.ToString());
            },
            reader => { });
    }

    public static object MakeARequestToTheDB6(string request)
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

        return players;
    }

}

