



public enum Gender
{
    Man,
    Woman
}

public enum Country
{
    Russia,
    USA,
    Italy
}

public struct Player
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string TeamName { get; set; }
    public Gender Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Country Country { get; set; }

    public Player(
        string Name,
        string Surname,
        string TeamName,
        Gender Gender,
        DateTime DateOfBirth,
        Country Country)
    {
        this.Name = Name;
        this.Surname = Surname;
        this.TeamName = TeamName;
        this.Gender = Gender;
        this.DateOfBirth = DateOfBirth;
        this.Country = Country;
    }
}

public class PlayersInfo
{
    private static Dictionary<int,Player> Players;

    public PlayersInfo()
    {
        Players = new Dictionary<int,Player>();
    }

    public void AddPlayer(int id, Player player)
    {
        Players.Add(id, player);
    }

    public void RemovePlayer(int id)
    {
        Players.Remove(id);
    }

    
}

