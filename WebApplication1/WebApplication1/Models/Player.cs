using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Поле 'Имя' обязательно для заполнения")]
        [StringLength(50, ErrorMessage = "Имя должно содержать не более 50 символов")]
        [RegularExpression(@"^\S.*\S$", ErrorMessage = "Имя не должно содержать пробелы в начале и в конце или состоять из одного символа")]
        public string Name { get; set; }

        [Key]
        public int Id { get; set; }

        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Поле 'Фамилия' обязательно для заполнения")]
        [StringLength(50, ErrorMessage = "Фамилия должна содержать не более 50 символов")]
        [RegularExpression(@"^\S.*\S$", ErrorMessage = "Фамилия не должно содержать пробелы в начале и в конце или состоять из одного символа")]
        public string Surname { get; set; }

        [Display(Name = "Назмание Команды")]
        [Required(ErrorMessage = "Поле 'Название команды' обязательно для заполнения")]
        [StringLength(100, ErrorMessage = "Название команды должно содержать не более 100 символов")]
        [RegularExpression(@"^\S.*\S$", ErrorMessage = "Название команды не должно содержать пробелы в начале и в конце или состоять из одного символа")]
        public string TeamName { get; set; }


        /// <summary>
        /// 
        /// </summary>

        private Gender gender;

        [Display(Name = "Пол")]
        [Required(ErrorMessage = "Поле 'Пол' обязательно для заполнения")]
        [StringLength(10, ErrorMessage = "Поле 'Пол' должно содержать не более 10 символов")]
        public string GenderString
        {
            get { return gender.ToString(); }
            set { gender = (Gender)Enum.Parse(typeof(Gender), value, true); }
        }

        [NotMapped]
        public Gender Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        /// <summary>
        /// 
        /// </summary>

        [Display(Name = "Дата Рождения")]
        [Required(ErrorMessage = "Поле 'Дата рождения' обязательно для заполнения")]
        [DataType(DataType.Date, ErrorMessage = "Некорректный формат даты")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [PastDate(ErrorMessage = "Дата рождения не может быть из будущего")]
        [Range(typeof(DateTime), "01/01/1900", "01/01/2100", ErrorMessage = "Некорректное значение даты рождения")]
        [EmptyDate(ErrorMessage = "Поле 'Дата рождения' не может быть пустым")] 
        public DateTime DateOfBirth { get; set; }



        private Country country;

        [Display(Name = "Страна")]
        [Required(ErrorMessage = "Поле 'Страна' обязательно для заполнения")]
        [StringLength(50, ErrorMessage = "Страна должна содержать не более 50 символов")]
        public string CountryString
        {
            get { return country.ToString(); }
            set { country = (Country)Enum.Parse(typeof(Country), value, true); }
        }

        [NotMapped]
        public Country Country
        {
            get { return country; }
            set { country = value; }
        }

       
    }


    public class PastDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            

            if (value is DateTime date)
            {
                return date <= DateTime.Now;
            }
            return false;
        }
    }

    public class EmptyDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            // Проверяем, что значение не пусто и не равно null
            return value != null && !string.IsNullOrEmpty(value.ToString());
        }
    }
}
