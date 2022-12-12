using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MStarSupply.Data.Entities
{
    public class EntradaSaida
    {
        public Guid IdEntradaSaida { get; set; }
        public DateTime DataEntrada { get; set; }
        public Guid IdMercadoria { get; set; }
        public string NomeMercadoria { get; set; }
        public bool IsSaida { get; set; }
        public DateTime DataSaida { get; set; }
    }
}
