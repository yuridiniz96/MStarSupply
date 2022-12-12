using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MStarSupply.Data.Entities
{
    public class Mercadoria
    {
        public Guid IdMercadoria { get; set; }
        public string NomeMercadoria { get; set; }
        public string NumeroRegistro { get; set; }
        public string Fabricante { get; set; }
        public string TipoMercadoria { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
