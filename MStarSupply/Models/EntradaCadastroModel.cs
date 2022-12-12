using Microsoft.AspNetCore.Mvc.Rendering;
using MStarSupply.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace MStarSupply.Models
{
    public class EntradaCadastroModel
    {
        [Required(ErrorMessage = "Por favor, informe a Data de Entrada.")]
        public string DataEntrada { get; set; }

        [Required(ErrorMessage = "Por favor, informe a Quantidade.")]
        public int Quantidade { get; set; }
        public Guid IdMercadoria { get; set; }
        public List<SelectListItem>? listagemMercadorias { get; set; }

    }
}
