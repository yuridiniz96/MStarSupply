using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MStarSupply.Domain.Interfaces.Reports
{
    public interface IEntradaSaidaReports
    {
        byte[] CreateExcel(List<EntradaSaida> entradasSaidas);
        byte[] CreatePdf(List<EntradaSaida> entradasSaidas);
    }
}
