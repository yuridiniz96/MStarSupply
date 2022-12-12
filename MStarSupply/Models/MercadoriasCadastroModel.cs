using System.ComponentModel.DataAnnotations;

namespace MStarSupply.Models
{
    public class MercadoriasCadastroModel
    {
        [MinLength(3, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(20, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe o nome da mecadoria.")]
        public string NomeMercadoria { get; set; }

        [MinLength(5, ErrorMessage = "Por favor, informe no mínimo {1} digitos.")]
        [MaxLength(10, ErrorMessage = "Por favor, informe no máximo {1} digitos.")]
        [Required(ErrorMessage = "Por favor, informe o número de registro da mercadoria.")]
        public string NumeroRegistro { get; set; }

        [Required(ErrorMessage = "Por favor, informe o fabricante da mercadoria.")]
        public string Fabricante { get; set; }

        [Required(ErrorMessage = "Por favor, informe o tipo da mercadoria.")]
        public string TipoMercadoria { get; set; }

        [MinLength(6, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(20, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe a descrição da mercadoria.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data de cadastro da mercadoria.")]
        public string DataCadastro { get; set; }

    }
}
