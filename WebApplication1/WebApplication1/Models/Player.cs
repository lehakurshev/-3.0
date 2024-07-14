using System.ComponentModel.DataAnnotations;

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

        [Key]
        public int Id { get; set; }
        
        public string Surname { get; set; }
        public string TeamName { get; set; }
        public string Gender { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
        public string Country { get; set; }
    }
}
