using System.ComponentModel.DataAnnotations;

namespace to_do.Dto
{
    public class Register
    {
        public string? Id { get; set; } = string.Empty;


        [Required(ErrorMessage = "User Name is Requierd")]
        public string Name { get; set; } = string.Empty;


        [EmailAddress]
        [Required(ErrorMessage = "Email is Requierd")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;


        [Required(ErrorMessage = "Password is Requierd")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;


        [Required(ErrorMessage = "ConfirmPassword is Requierd")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
