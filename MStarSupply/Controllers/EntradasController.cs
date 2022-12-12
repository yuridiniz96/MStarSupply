using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MStarSupply.Data.Entities;
using MStarSupply.Data.Repositories;
using MStarSupply.Models;

namespace MStarSupply.Controllers
{
    public class EntradasController : Controller
    {
        public async Task<IActionResult> Cadastro()
        {
            var model = new EntradaCadastroModel
            {
                listagemMercadorias = await ObterListagemMercadorias()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastro (EntradaCadastroModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var mercadoriaRepository = new MercadoriaRepository();

                    for (int i = 0; i < model.Quantidade; i++)
                    {
                        var entrada = new EntradaSaida();

                        entrada.IdEntradaSaida = Guid.NewGuid();
                        entrada.DataEntrada = DateTime.Parse(model.DataEntrada);
                        entrada.IdMercadoria = model.IdMercadoria;
                        entrada.NomeMercadoria = mercadoriaRepository.GetNameById(model.IdMercadoria).NomeMercadoria;


                        var entradaSaidaRespository = new EntradaSaidaRepository();
                        entradaSaidaRespository.CreateEntrada(entrada);
                    }

                    TempData["Mensagem"] = $"Cadastrado com sucesso!";
                }
                catch (Exception e)
                {
                    TempData["Mensagem"] = $"Falha ao cadastrar: {e.Message}.";
                }
            }
            model.listagemMercadorias = await ObterListagemMercadorias();
            return View(model);
        }

        public IActionResult Consulta()
        {
            var model = new EntradaConsultaModel();
            try
            {
                model.DataInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");
                model.DataFim = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)).ToString("yyyy-MM-dd");

                var entradaSaidaRepository = new EntradaSaidaRepository();
                var entradas = entradaSaidaRepository.GetDateEntrada(DateTime.Parse(model.DataInicio), DateTime.Parse(model.DataFim));

                model.Entradas = entradas;


                

            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Consulta(EntradaConsultaModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dataInicio = DateTime.Parse(model.DataInicio);
                    var dataFim = DateTime.Parse(model.DataFim);

                    var entradaRepository = new EntradaSaidaRepository();
                    var entradas = entradaRepository.GetDateEntrada(dataInicio, dataFim);

                    var nomeArquivo = $"relatorio_{DateTime.Now.ToString("ddMMyyyyHHmmss")}";
                    var tipoArquivo = string.Empty;
                    byte[] arquivo = null;


                    model.Entradas = entradas;

                    if (model.Formato == "Excel")
                    {
                        nomeArquivo += ".xlsx";
                        tipoArquivo = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        arquivo = entradaRepository.GerarRelatorioExcel(entradas);

                        return File(arquivo, tipoArquivo, nomeArquivo);
                    }
                    if(model.Formato == "Pdf")
                    {
                        nomeArquivo += ".pdf";
                        tipoArquivo = "application/pdf";
                        arquivo = entradaRepository.GerarRelatorioPdf(entradas);

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

        public IActionResult Edicao(Guid id)
        {
            try
            {
                var saida = new EntradaSaida();
                saida.IdEntradaSaida = id;

                var entradaSaidaRepository = new EntradaSaidaRepository();
                entradaSaidaRepository.UpdateForSaida(saida);

                TempData["Mensagem"] = $"Saída com sucesso.";
            }
            catch (Exception e)
            {
                TempData["Mensagem"] = e.Message;
            }

            return RedirectToAction("Consulta");
        }

        public async Task<List<SelectListItem>> ObterListagemMercadorias()
        {
            var mercadoriaRepository = new MercadoriaRepository();


            var itens = new List<SelectListItem>();
            foreach(var item in mercadoriaRepository.GetAllDistinct())
            {
                itens.Add(new SelectListItem
                {
                    Value = item.IdMercadoria.ToString(),
                    Text = $"{item.NomeMercadoria.ToUpper()}"
                });
            }
            return itens;
        }

    }
}
