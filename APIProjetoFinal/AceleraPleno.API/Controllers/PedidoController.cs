using AceleraPleno.API.Interface;
using AceleraPleno.API.Models;
using AceleraPleno.API.Models.PartialModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AceleraPleno.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : Controller
    {
        private readonly IRepositoryPedido<Pedido> _iRepositoryPedido;

        public PedidoController(IRepositoryPedido<Pedido> iRepositoryPedido)
        {
            _iRepositoryPedido = iRepositoryPedido;
        }

        [AllowAnonymous]
        [HttpGet, Route("Listar")]
        public async Task<IActionResult> Listar()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }


            var pedidos = await _iRepositoryPedido.Listar();
            return Ok(pedidos);
        }

        [Authorize]
        [HttpPost, Route("Incluir")]
        public async Task<IActionResult> Incluir(Pedido pedido)
        {
            await _iRepositoryPedido.Adicionar(pedido);
            return Ok(pedido);
        }

        //[Authorize]
        //[HttpPost, Route("Incluir")]
        //public async Task<IActionResult> Incluir2(Guid mesaId, Guid pratoId, int qtd, decimal valor)
        //{
        //    //await _iRepositoryPedido.Adicionar2(mesaId, pratoId, qtd, valor);
        //    return Ok(await _iRepositoryPedido.Adicionar2(mesaId, pratoId, qtd, valor));
        //}

        [AllowAnonymous]
        [HttpGet, Route("FiltrarPorId/{id}")]
        public async Task<IActionResult> FiltrarPorId(Guid id)
        {
            var pedido = await _iRepositoryPedido.FiltrarId(id);
            return Ok(pedido);
        }

        [Authorize]
        [HttpPut, Route("Alterar/{id}")]
        public async Task<IActionResult> Atualizar(Guid id, Pedido pedido)
        {
            if (pedido == null)
                return BadRequest();
            var pedidoAtualizado = await _iRepositoryPedido.Atualizar(pedido, pedido.Id);
            return Ok(pedidoAtualizado);
        }

        [Authorize]
        [HttpPut, Route("Preparando/{id}")]
        public async Task<IActionResult> AtualizarParaPreparando(Guid id)
        {
            if (id == null)
                return BadRequest();
            await _iRepositoryPedido.AlterarPedidoParaPreparando(id);
            var pedidoAtualizado = await _iRepositoryPedido.FiltrarId(id);
            return Ok(pedidoAtualizado);
        }

        [Authorize]
        [HttpPut, Route("Disponivel/{id}")]
        public async Task<IActionResult> AtualizarParaDisponivel(Guid id)
        {
            if (id == null)
                return BadRequest();
            await _iRepositoryPedido.AlterarPedidoParaDisponivel(id);
            var pedidoAtualizado = await _iRepositoryPedido.FiltrarId(id);
            return Ok(pedidoAtualizado);
        }

        [AllowAnonymous]
        [HttpPut, Route("Entregue/{id}")]
        public async Task<IActionResult> AtualizarParaEntregue(Guid id)
        {
            if (id == null)
                return BadRequest();
            await _iRepositoryPedido.AlterarPedidoParaEntregue(id);
            var pedidoAtualizado = await _iRepositoryPedido.FiltrarId(id);
            return Ok(pedidoAtualizado);
        }

        [Authorize]
        [HttpPut, Route("Cancelado/{id}")]
        public async Task<IActionResult> AtualizarParaCancelado(Guid id)
        {
            if (id == null)
                return BadRequest();
            await _iRepositoryPedido.AlterarPedidoParaCancelado(id);
            var pedidoAtualizado = await _iRepositoryPedido.FiltrarId(id);
            return Ok(pedidoAtualizado);
        }

        [Authorize]
        [HttpPut, Route("Baixado/{id}")]
        public async Task<IActionResult> AtualizarParaBaixado(Guid id)
        {
            if (id == null)
                return BadRequest();
            await _iRepositoryPedido.AlterarPedidoParaBaixado(id);
            var pedidoAtualizado = await _iRepositoryPedido.FiltrarId(id);
            return Ok(pedidoAtualizado);
        }

        [Authorize]
        [HttpPut, Route("BaixarPedidosLiberarMesa/{cpf}/{idmesa}")]
        public async Task<IActionResult> BaixarPedidosDeClientePorMesa(string cpf, Guid idmesa)
        {
            FiltrarPedidoMesaCliente filtro = new FiltrarPedidoMesaCliente();
            filtro.IdMesa = idmesa;
            filtro.CPF = cpf;
            var pedidos = await _iRepositoryPedido.BaixarPedidosMesaCliente(filtro);
            return Ok(pedidos);
        }

        [Authorize]
        [HttpDelete, Route("Deletar/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!await _iRepositoryPedido.Excluir(id))
                return BadRequest();

            var pedidos = await _iRepositoryPedido.Listar();
            return Ok(pedidos);
        }

        [Authorize]
        [HttpGet, Route("FiltrarPorMesaCliente/{cpf}/{idmesa}")]
        public async Task<IActionResult> FiltrarPorMesaCliente(string cpf, Guid idmesa)
        {
            FiltrarPedidoMesaCliente filtro = new FiltrarPedidoMesaCliente();
            filtro.IdMesa = idmesa;
            filtro.CPF = cpf;
            var pedidos = await _iRepositoryPedido.FiltarPorMesaECPF(filtro);
            return Ok(pedidos);
        }
    }
}
