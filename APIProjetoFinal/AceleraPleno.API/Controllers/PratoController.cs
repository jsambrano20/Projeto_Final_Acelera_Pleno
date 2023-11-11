using AceleraPleno.API.Interface;
using AceleraPleno.API.Models;
using AceleraPleno.API.Models.PartialModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AceleraPleno.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PratoController : ControllerBase
    {
        private readonly IRepositoryPrato<Prato> _iRepository;

        public PratoController(IRepositoryPrato<Prato> iRepository)
        {
            _iRepository = iRepository;
        }

        //[Authorize]
        [AllowAnonymous]
        [HttpGet, Route("Listar")]
        public async Task<IActionResult> Listar()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }


            var pratos = await _iRepository.Listar();
            return Ok(pratos);
        }

        //[Authorize]
        [AllowAnonymous]
        [HttpPost, Route("Incluir")]
        public async Task<IActionResult> Incluir(Prato prato)
        {
            try
            {
                await _iRepository.Adicionar(prato);

                return Ok(prato);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //[Authorize]
        [AllowAnonymous]
        [HttpPut, Route("Alterar/{id}")]
        public async Task<Prato> AlterarPrato(Guid id, Prato prato)
        {
            return await _iRepository.Atualizar(prato, id);
        }

        //[Authorize]
        [AllowAnonymous]
        [HttpGet, Route("FiltrarPorId/{id}")]
        public async Task<Prato> ListarPratoPorId(Guid id)
        {
            return await _iRepository.FiltrarId(id);
        }

        //[Authorize]
        [AllowAnonymous]
        [HttpPut, Route("AtivarPrato/{id}")]
        public async Task<string> OcuparMesa(Guid id)
        {
            return await _iRepository.AtivarPrato(id);
        }

        //[Authorize]
        [AllowAnonymous]
        [HttpPut, Route("InativarPrato/{id}")]
        public async Task<string> DesocuparMesa(Guid id)
        {
            return await _iRepository.InativarPrato(id);
        }

        //[Authorize]
        [AllowAnonymous]
        [HttpDelete, Route("Deletar/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!await _iRepository.Excluir(id))
                return BadRequest();

            var prato = await _iRepository.Listar();
            return Ok(prato);
        }
    }
}
