using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebMotors.Domain.Contracts;
using WebMotors.Domain.Models;
using WebMotors.Domain.Models.Anuncio;

namespace WebMotors.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/anuncios")]
    public class AnuncioController : ControllerBase
    {
        private readonly IAnuncioService _anuncioService;

        public AnuncioController(IAnuncioService anuncioService)
        {
            _anuncioService = anuncioService;
        }

        [HttpGet("marca")]
        public async Task<IActionResult> ObterMarcas()
        {
            var model = await _anuncioService.ObterMarcas();

            return Ok(model);
        }

        [HttpGet("modelo/{marca-id:int}")]
        public async Task<IActionResult> ObterModelo([FromRoute(Name = "marca-id")] int marcaId)
        {
            var model = await _anuncioService.ObterModelos(marcaId);

            return Ok(model);
        }

        [HttpGet("versao/{modelo-id:int}")]
        public async Task<IActionResult> ObterVersao([FromRoute(Name = "modelo-id")] int modeloId)
        {
            var model = await _anuncioService.ObterVersoes(modeloId);

            return Ok(model);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObterPorCodigo(int id)
        {
            var model = await _anuncioService.ObterPorCodigo(id);

            return Ok(model);
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            var model = await _anuncioService.ObterTodos();

            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar(AnuncioRequest model)
        {
            var modelCreated = await _anuncioService.Adicionar(model);

            return Ok(modelCreated);
        }


        [HttpPut]
        public async Task<IActionResult> Atualizar(AnuncioAtualizarRequest model)
        {
            var modelUpdated = await _anuncioService.Atualizar(model);

            return Ok(modelUpdated);
        }

        [HttpDelete("{id:int}")]
        public async Task<NoContentResult> Delete(int id)
        {
            await _anuncioService.Remover(id);

            return NoContent();
        }
    }
}
