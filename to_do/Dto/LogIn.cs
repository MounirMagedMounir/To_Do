using System.ComponentModel.DataAnnotations;

namespace to_do.Dto
{
    public class LogIn
    {

        [EmailAddress]
        [Required(ErrorMessage = "Email is Requierd")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password is Requierd")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
