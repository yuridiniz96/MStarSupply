using MStarSupply.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace MStarSupply.Models
{
    public class SaidaConsultaModel
    {
        [Required(ErrorMessage = "Por favor, informe a data de início.")]
        public string DataInicio { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data de término.")]
        public string DataFim { get; set; }

        public string? Formato { get; set; }

        public List<EntradaSaida>? Saidas { get; set; }
    }
}
