using Microsoft.AspNetCore.Mvc;
using MStarSupply.Data.Repositories;
using MStarSupply.Models;

namespace MStarSupply.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Dashboard()
        {
            var model = new DashboardModel();
            try
            {
                model.DataInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");
                model.DataFim = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)).ToString("yyyy-MM-dd");

                var entradaSaidaRepository = new EntradaSaidaRepository();

                var entradaSaida = entradaSaidaRepository.GetDateForDashboard(DateTime.Parse(model.DataInicio), DateTime.Parse(model.DataFim));

                TempData["TotalEntradas"] = entradaSaida.Where(c => c.IsSaida == false).Count();

                TempData["TotalSaidas"] = entradaSaida.Where(c => c.IsSaida == true).Count();
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }
            return View(model);
        }


        [HttpPost]
        public IActionResult Dashboard(DashboardModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var entradaSaidaRepository = new EntradaSaidaRepository();

                    var entradaSaida = entradaSaidaRepository.GetDateForDashboard(DateTime.Parse(model.DataInicio), DateTime.Parse(model.DataFim));

                   TempData["TotalEntradas"] = entradaSaida.Where(c => c.IsSaida == false).Count();

                   TempData["TotalSaidas"] = entradaSaida.Where(c => c.IsSaida == true).Count();
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }
            return View(model);
        }
    }
}
