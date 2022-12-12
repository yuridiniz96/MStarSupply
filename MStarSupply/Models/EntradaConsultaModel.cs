using MStarSupply.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace MStarSupply.Models
{
    public class EntradaConsultaModel
    {
        [Required(ErrorMessage = "Por favor, informe a data de início.")]
        public string DataInicio { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data de término.")]
        public string DataFim { get; set; }

        public string? Formato { get; set; }

        public List<EntradaSaida>? Entradas { get; set; }
    }
}
