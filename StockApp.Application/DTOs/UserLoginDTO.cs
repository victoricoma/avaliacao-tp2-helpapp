using System.ComponentModel.DataAnnotations;

namespace StockApp.Application.DTOs
{
    public class UserLoginDTO
    {
        [Required(ErrorMessage = "O campo email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O formato do email é inválido.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "O campo senha é obrigatório.")]
        public string? Password { get; set; }
    }

}
