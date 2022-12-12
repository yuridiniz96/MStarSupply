using Microsoft.AspNetCore.Mvc;
using MStarSupply.Data.Entities;
using MStarSupply.Data.Repositories;
using MStarSupply.Models;

namespace MStarSupply.Controllers
{
    public class MercadoriasController : Controller
    {
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost] 
        public IActionResult Cadastro(MercadoriasCadastroModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var mercadoria = new Mercadoria();


                    mercadoria.IdMercadoria = Guid.NewGuid();
                    mercadoria.NomeMercadoria = model.NomeMercadoria;
                    mercadoria.NumeroRegistro = model.NumeroRegistro;
                    mercadoria.Fabricante = model.Fabricante;
                    mercadoria.TipoMercadoria = model.TipoMercadoria;
                    mercadoria.Descricao = model.Descricao;
                    mercadoria.DataCadastro = DateTime.Parse(model.DataCadastro);

                    var mercadoriaRepository = new MercadoriaRepository();
                    mercadoriaRepository.Create(mercadoria);

                    TempData["Mensagem"] = $"Cadastrado com sucesso!";
                    ModelState.Clear();
                }
                catch (Exception e)
                {
                    TempData["Mensagem"] = $"Falha ao cadastrar mercadoria: {e.Message}.";
                }
            }

            return View();
        }

        public IActionResult Consulta()
        {
            var lista = new List<MercadoriasConsultaModel>();

            try
            {
                var mercadoriaRepository = new MercadoriaRepository();
                var mercadoria = mercadoriaRepository.GetAll();


                foreach (var item in mercadoria)
                {
                    var model = new MercadoriasConsultaModel();

                    model.IdMercadoria = item.IdMercadoria;
                    model.NomeMercadoria = item.NomeMercadoria;
                    model.NumeroRegistro = item.NumeroRegistro;
                    model.Fabricante = item.Fabricante;
                    model.TipoMercadoria = item.TipoMercadoria;
                    model.Descricao = item.Descricao;
                    model.DataCadastro = item.DataCadastro.ToString("dd/MM/yyyy");

                    lista.Add(model);
                }
            }
            catch (Exception e)
            {
                TempData["Mensagem"] = $"Falha ao consultar: {e.Message}.";
            }

            return View(lista);
        }

        public IActionResult Edicao(Guid id)
        {
            var model = new MercadoriasEdicaoModel();

            try
            {
                var mercadoriaRepository = new MercadoriaRepository();
                var mercadoria = mercadoriaRepository.GetById(id);


                model.IdMercadoria = mercadoria.IdMercadoria;
                model.NomeMercadoria = mercadoria.NomeMercadoria;
                model.NumeroRegistro = mercadoria.NumeroRegistro;
                model.Fabricante = mercadoria.Fabricante;
                model.TipoMercadoria = mercadoria.TipoMercadoria;
                model.Descricao = mercadoria.Descricao;
                model.DataCadastro = mercadoria.DataCadastro.ToString("dd/MM/yyyy");
            }
            catch (Exception e)
            {
                TempData["Mensagem"] = e.Message;
            }

            return View(model);
        }

        [HttpPost] 
        public IActionResult Edicao(MercadoriasEdicaoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var mercadoria = new Mercadoria();

                    mercadoria.IdMercadoria = model.IdMercadoria;
                    mercadoria.NomeMercadoria = model.NomeMercadoria;
                    mercadoria.NumeroRegistro = model.NumeroRegistro;
                    mercadoria.Fabricante = model.Fabricante;
                    mercadoria.TipoMercadoria = model.TipoMercadoria;
                    mercadoria.Descricao = model.Descricao;
                    mercadoria.DataCadastro = DateTime.Parse(model.DataCadastro);

                    var contatoRepository = new MercadoriaRepository();
                    contatoRepository.Update(mercadoria);

                    TempData["Mensagem"] = $"Atualizado com sucesso.";
                    return RedirectToAction("Consulta");
                }
                catch (Exception e)
                {
                    TempData["Mensagem"] = e.Message;
                }
            }

            return View(model);
        }

        public IActionResult Exclusao(Guid id)
        {
            try
            {
                var mercadoria = new Mercadoria();
                mercadoria.IdMercadoria = id;

                var mercadoriaRepository = new MercadoriaRepository();
                mercadoriaRepository.Delete(mercadoria);

                TempData["Mensagem"] = $"Excluído com sucesso.";
            }
            catch (Exception e)
            {
                TempData["Mensagem"] = $"Falha ao excluir: {e.Message}.";
            }
            return RedirectToAction("Consulta");
        }
    }
}
