using ControleContatos.Models;
using ControleContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleContatos.Controllers
{
    public class ContatoController : Controller
    {
        private readonly IContatoRepositorio _contatoRepositorio;



        public ContatoController(IContatoRepositorio contatoRepositorio)
        {
            this._contatoRepositorio = contatoRepositorio;
        }
        public IActionResult Index()
        {
            List<ContatoModel> contatos = _contatoRepositorio.BuscarTodos();
            return View(contatos);
        }
        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
            ContatoModel contato = _contatoRepositorio.ListarPorId(id);
            return View(contato);
        }

        public IActionResult DeletarConfirmacao(int id)
        {
            ContatoModel contato = _contatoRepositorio.ListarPorId(id);
            return View(contato);
        }


        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _contatoRepositorio.Apagar(id);

                if (apagado == true)
                {
                    TempData["mensagemSucesso"] = "Contato apagado com sucesso";
                }
                else
                {
                    TempData["mensagemErro"] = "Não foi possível apagar o contato";
                }
                return RedirectToAction("Index");

            }
            catch (Exception erro)
            {
                TempData["mensagemErro"] = $"Não foi possível apagar o contato, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public IActionResult Criar(ContatoModel contato)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    _contatoRepositorio.Adicionar(contato);
                    TempData["mensagemSucesso"] = "Contato cadastrado com sucesso";
                    return RedirectToAction("Index");
                }
                return View(contato);

            }
            catch (Exception erro)
            {
                TempData["mensagemErro"] = $"Não foi possível cadastrar o Contato. Detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }

        }



        [HttpPost]
        public IActionResult Editar(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _contatoRepositorio.Alterar(contato);
                    TempData["mensagemSucesso"] = "Contato Atualizado com sucesso";
                    return RedirectToAction("Index");
                }

                return View(contato);
            }
            catch (Exception erro)
            {
                TempData["mensagemErro"] = $"Não foi possível Atualizar o Contato. Detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

    }
}
