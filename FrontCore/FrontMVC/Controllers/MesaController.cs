using AutoMapper;
using FrontMVC.Interfaces;
using FrontMVC.Models.Cliente;
using FrontMVC.Models.Mesa;
using FrontMVC.Models.Pedido;
using FrontMVC.Models.Prato;
using FrontMVC.Services;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System.IO;
using X.PagedList;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

namespace FrontMVC.Controllers
{
    [Authorize]
    public class MesaController : Controller
    {
        private readonly IServiceMesa<MesaModel> _service;
        private readonly IMapper _mapper;
        private readonly IServicePedido<PedidosMesa> _pedido;

        public MesaController(IServiceMesa<MesaModel> service, IMapper mapper, IServicePedido<PedidosMesa> pedido)
        {
            _service = service;
            _mapper = mapper;
            _pedido = pedido;
        }

           
        public async Task<IActionResult> Index(string? like, int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            IEnumerable<MesaModel> list = await _service.Listar();

            var pagedList = list.ToPagedList(pageNumber, pageSize);

            return View(pagedList);
        }


        [HttpGet]
        public ActionResult Adicionar()
        {
            MesaIncluir Novo = new MesaIncluir();
            return View(Novo);
        }

        [HttpPost]
        public async Task<ActionResult> Adicionar(MesaModel Novo)
        {
            
            var retorno = await _service.Adicionar(Novo);

            if (retorno != null)
                return RedirectToAction("Index");
            else
                throw new Exception("Error");

        }

        [HttpGet]
        public async Task<IActionResult> Alterar(Guid id)
        {
            MesaModel mesa = await _service.FiltrarId(id);                   
            
            return View(_mapper.Map<MesaAlterar>(mesa));
        }

        [HttpPost]
        public async Task<ActionResult> Alterar(MesaModel mesa)
        {
            try
            {
                var retorno = await _service.Atualizar(mesa, mesa.Id);
                TempData["MsgAlert"] = "Mesa alterada sucesso!";
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                TempData["MsgAlert"] = "Tente Novamente mais tarde!" + e.Message;
                return RedirectToAction("Alterar");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Detalhes(Guid id)
        {
            MesaModel mesa = await _service.FiltrarId(id);

            return View(mesa);
        }
        [HttpGet]
        public async Task<IActionResult> OcuparMesa(Guid id)
        {
            MesaModel mesa = await _service.FiltrarId(id);
            OcuparMesa model = _mapper.Map<OcuparMesa>(mesa);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> OcuparMesa(OcuparMesa ocuparMesa)
        {
            var retorno = await _service.OcuparMesa(ocuparMesa);
            if (retorno != null)
                return RedirectToAction("Index");
            else
                throw new Exception("Error");
        }

        [HttpPost]
        public async Task<IActionResult> DesocuparMesa(Guid id, string cpf)
        {
            IEnumerable<PedidosMesa> list = await _pedido.BaixarPedidosLiberarMesa(id, cpf);

            ViewData["lista"] = list;

            return View(list);

            /*var retorno = await _service.DesocuparMesa(id);

            if (retorno)
            {
                TempData["MsgAlert"] = "Mesa Desocupada com sucesso!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["MsgAlert"] = "Tente Novamente mais tarde!";
                return RedirectToAction("Index");
            }*/
        }
    }
}
