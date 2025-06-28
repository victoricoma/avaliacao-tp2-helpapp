using System.ComponentModel.DataAnnotations;

namespace StockApp.Application.DTOs
{
    public class UserLoginDTO
    {
        [Required(ErrorMessage = "O campo username é obrigatório.")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "O campo senha é obrigatório.")]
        public string? Password { get; set; }
    }

}
