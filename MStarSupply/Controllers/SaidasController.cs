using Microsoft.AspNetCore.Mvc;
using MStarSupply.Data.Repositories;
using MStarSupply.Models;
using System.Reflection;

namespace MStarSupply.Controllers
{
    public class SaidasController : Controller
    {
        public IActionResult Consulta()
        {
            var model = new SaidaConsultaModel();
            try
            {
                model.DataInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");
                model.DataFim = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)).ToString("yyyy-MM-dd");

                var entradaSaidaRepository = new EntradaSaidaRepository();
                var saidas = entradaSaidaRepository.GetDateSaida(DateTime.Parse(model.DataInicio), DateTime.Parse(model.DataFim));

                model.Saidas = saidas;
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Consulta(SaidaConsultaModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var DataInicio = DateTime.Parse(model.DataInicio);
                    var DataFim = DateTime.Parse(model.DataFim);

                    var entradaRepository = new EntradaSaidaRepository();
                    var saidas = entradaRepository.GetDateSaida(DataInicio, DataFim);

                    var nomeArquivo = $"relatorio_{DateTime.Now.ToString("ddMMyyyyHHmmss")}";
                    var tipoArquivo = string.Empty;
                    byte[] arquivo = null;


                    model.Saidas = saidas;

                    if (model.Formato == "Excel")
                    {
                        nomeArquivo += ".xlsx";
                        tipoArquivo = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        arquivo = entradaRepository.GerarRelatorioExcel(saidas);

                        return File(arquivo, tipoArquivo, nomeArquivo);
                    }
                    if (model.Formato == "Pdf")
                    {
                        nomeArquivo += ".pdf";
                        tipoArquivo = "application/pdf";
                        arquivo = entradaRepository.GerarRelatorioPdf(saidas);

                        return File(arquivo, tipoArquivo, nomeArquivo);
                    }
                }
                catch (Exception e)
                {
                    TempData["Mensagem"] = $"Falha ao consultar: {e.Message}.";
                }
            }

            return View(model);
        }
    }
}
