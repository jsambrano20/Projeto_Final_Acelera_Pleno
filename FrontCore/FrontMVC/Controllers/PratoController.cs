using FrontMVC.Interfaces;
using FrontMVC.Models.Prato;
using FrontMVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Drawing;
using System.IO;
using System.Text;
using X.PagedList;
using static System.Net.Mime.MediaTypeNames;

namespace FrontMVC.Controllers
{
    [Authorize]
    public class PratoController : Controller
    {
        private readonly IServicePrato<PratoModel> pratoService;

        public PratoController(IServicePrato<PratoModel> pratoService)
        {
            this.pratoService = pratoService;
        }

        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var list = await pratoService.Listar();

            foreach (var imagem in list)
            {
                imagem.ConverterBase64ParaJpg();
            }

            var pagedList = list.ToPagedList(pageNumber, pageSize);

            return View(pagedList);
        }

        [HttpGet]
        public ActionResult Adicionar()
        {
            PratoModel Novo = new PratoModel();
            return View(Novo);
        }

        [HttpPost]
        public async Task<ActionResult> Adicionar(PratoModel Novo)
        {
            Novo.Foto = await pratoService.ConverteImg(Novo.FotoBase);

            var retorno = await pratoService.Adicionar(Novo);


            if (retorno != null)
                return RedirectToAction("Index");
            else
                throw new Exception("Error");
        }

        [HttpPost]
        public async Task<ActionResult> Delete(Guid id)
        {
            var retorno = await pratoService.Excluir(id);

            if (retorno != null)
                return RedirectToAction("Index");
            else
                throw new Exception("Error");
        }

        [HttpGet]
        public async Task<IActionResult> Alterar(Guid id)
        {
            var lista = await pratoService.FiltrarId(id);

            lista.ConverterBase64ParaJpg();


            return View(lista);
        }

        [HttpPost]
        public async Task<ActionResult> Alterar(PratoModel prato)
        {
            if (prato.FotoBase != null)
                prato.Foto = await pratoService.ConverteImg(prato.FotoBase);

            var retorno = await pratoService.Atualizar(prato, prato.Id);


            if (retorno != null)
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

        [HttpPost]
        public async Task<ActionResult> AtivarPrato(Guid id, bool status)
        {
            var retorno = await pratoService.AtivarPrato(id, status);


            TempData["MsgAlert"] = retorno;
            return RedirectToAction("Index");



        }

        [HttpPost]
        public async Task<ActionResult> InativarPrato(Guid id, bool status)
        {
            var retorno = await pratoService.InativarPrato(id, status);

            TempData["MsgAlert"] = retorno;
            return RedirectToAction("Index");


        }



    }
}
