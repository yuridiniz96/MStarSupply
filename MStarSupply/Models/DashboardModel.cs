using System.ComponentModel.DataAnnotations;

namespace MStarSupply.Models
{
    public class DashboardModel
    {
        [Required(ErrorMessage = "Por favor, informe a data de início.")]
        public string DataInicio { get; set; }
        [Required(ErrorMessage = "Por favor, informe a data de término.")]
        public string DataFim { get; set; }
    }
}
