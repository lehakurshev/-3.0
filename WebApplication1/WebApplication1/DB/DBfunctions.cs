using Npgsql;
using WebApplication1.Models;

namespace WebApplication1.DB
{
    public static class DBfunctions
    {

        public static object MakeARequestToTheDB1(string request)
        {
            string connString = "PORT=5432;DATABASE=football players;HOST=localhost;USER ID=postgres;PASSWORD=postgres";
            var teamNames = new List<string>();

            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var command = new NpgsqlCommand(request, conn))
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


            return teamNames;
        }


        public static object MakeARequestToTheDB2(string request, int playerId)
        {
            string connString = "PORT=5432;DATABASE=football players;HOST=localhost;USER ID=postgres;PASSWORD=postgres";

            Player player = new Player();

            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (var command = new NpgsqlCommand(request, conn))
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

            return player;
        }


        public static void MakeARequestToTheDB2(string request, Player player)
        {
            string connString = "PORT=5432;DATABASE=football players;HOST=localhost;USER ID=postgres;PASSWORD=postgres";

            var playerId = player.Id;

            using (var conn = new NpgsqlConnection())
            {
                conn.ConnectionString = connString;
                conn.Open();

                using (var command = new NpgsqlCommand(request, conn))
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
        }



        }
}
