using System.ComponentModel.DataAnnotations;

namespace MStarSupply.Models
{
    public class AccountLoginModel
    {
        [EmailAddress(ErrorMessage = "Por favor, informe um endereço de e-mail válido.")]
        [Required(ErrorMessage = "Por favor, informe seu e-mail de acesso.")]
        public string Email { get; set; }

        [MinLength(8, ErrorMessage = "A senha deve conter no minimo {1} caracteres.")]
        [MaxLength(20, ErrorMessage = "A senha deve conter no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe sua senha de acesso.")]
        public string Senha { get; set; }
    }
}
