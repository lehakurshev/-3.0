namespace WebApplication1.Models
{

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

    public class Player
    {
        public string Name { get; set; }
        public int Id { get; set; }
        
        public string Surname { get; set; }
        public string TeamName { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Country Country { get; set; }
    }

    


}
