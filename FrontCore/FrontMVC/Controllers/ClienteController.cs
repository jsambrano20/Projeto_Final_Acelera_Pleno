using FrontMVC.Interfaces;
using FrontMVC.Models.Cliente;
using FrontMVC.Models.Mesa;
using FrontMVC.Models.Prato;
using FrontMVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace FrontMVC.Controllers
{
    [Authorize]
    public class ClienteController : Controller
    {
        private readonly IServiceCliente<ClienteModel> _clienteService;

        public ClienteController(IServiceCliente<ClienteModel> clienteService)
        {
            _clienteService = clienteService;
        }

        //public async Task<IActionResult> Index()
        //{

        //    return View(list);
        //}

        public async Task<IActionResult> Index(string? like,int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            IEnumerable<ClienteModel> list = like == null ? await _clienteService.Listar() : await _clienteService.ListarLike(like);

            var pagedList = list.ToPagedList(pageNumber, pageSize);

            return View(pagedList);
        }

        [HttpGet]
        public async Task<IActionResult> Detalhes(Guid id)
        {
            var cliente = await _clienteService.FiltrarId(id);
            return View(cliente);
        }

        [HttpGet]
        public async Task<ClienteModel> BuscarCpf(string cpf)
        {
            var cliente = await _clienteService.FiltrarPorCpf(cpf);
            return cliente;
        }

        [HttpGet]
        public ActionResult Adicionar()
        {
            ClienteModel novo = new ClienteModel();
            return View(novo);
        }

        [HttpPost]
        public async Task<ActionResult> Adicionar(ClienteModel novo)
        {
            var retorno = await _clienteService.Adicionar(novo);

            if (retorno != null)
            {
                TempData["MsgAlert"] = "Cadastrado com Sucesso";
                return RedirectToAction("Index");
            }
            else
                throw new Exception("Error");
        }

        [HttpPost]
        public async Task<ActionResult> Delete(Guid id)
        {
            var retorno = await _clienteService.Excluir(id);

            if (retorno != null)
            {
                TempData["MsgAlert"] = "Deletado com Sucesso";
                return RedirectToAction("Index");
            }
            else
                throw new Exception("Error");
        }

        [HttpGet]
        public async Task<IActionResult> Alterar(Guid id)
        {
            var lista = await _clienteService.FiltrarId(id);
            return View(lista);
        }

        [HttpPost]
        public async Task<ActionResult> Alterar(ClienteModel cliente)
        {
            var retorno = await _clienteService.Atualizar(cliente, cliente.Id);

            if (retorno.DataAlteracao != cliente.DataAlteracao)
            {
                TempData["MsgAlert"] = "Alterado com Sucesso";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["MsgAlert"] = "Não alterado!";
                return RedirectToAction("Index");
            }
        }


    }
}
