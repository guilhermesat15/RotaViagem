using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using RotaViagem.Service.IService;
using RotaViagem.Service.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting;

namespace SysRota.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RotaController : ControllerBase
    {
        private readonly IRotaService _rotaService;

        public RotaController(IRotaService rotaService)
        {
            _rotaService = rotaService;
        }


        // GET api/Rota/CalculoRota/{nomeLocalOrigem}/{nomeLocalDestino}
        /// <summary>
        /// Retorna a rota de viagem mais barata independente da quantidade de conexões.
        /// </summary>
        /// <returns>Retorna a viagem mais barata</returns>
        /// <response code="200">Resultado da consulta: **GRU - BRC - SCL - ORL - CDG ao custo de $40**</response>
        [HttpGet("CalculoRota/{nomeLocalOrigem}/{nomeLocalDestino}")]
        public async Task<IActionResult> GetCalculeRotaAsync(string nomeLocalOrigem, string nomeLocalDestino)
        {
            try
            {
                var rotas = await _rotaService.GetCalculeRotaAsync(nomeLocalOrigem, nomeLocalDestino);
                if (rotas == null) return NoContent();

                return Ok(rotas);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar rotas");
            }
        }


        /// <summary>
        /// Busca o item da lista cadastrado.
        /// </summary>
        /// <returns>Os itens da To-do list</returns>
        /// <response code="200">Returna o item cadastrado</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var rota = await _rotaService.GetByIdAsync(id);
                if (rota == null) return NoContent();

                return Ok(rota);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar rotas. Erro: {ex.Message}");
            }
        }

        // GET api/Rota
        /// <summary>
        /// Lista os itens da lista.
        /// </summary>
        /// <returns>Os itens da list</returns>
        /// <response code="200">Returna os itens da list cadastrados</response>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var rotas = await _rotaService.GetAllAsync();
                if (rotas == null) return NoContent();

                return Ok(rotas);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar rotas");
            }
        }


        // POST api/Rota
        /// <summary>
        /// Cria um item na lista.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST /Rota
        ///     {
        ///         "localOrigemId": 1,
        ///         "localDestinoId": 2,
        ///         "custoViagem": 10
        ///     }
        ///
        /// </remarks>
        /// <param name="value"></param>
        /// <returns>Um novo item criado</returns>
        /// <response code="201">Retorna o novo item criado</response>
        /// <response code="400">Se o item não for criado</response>     
        [HttpPost]
        public async Task<IActionResult> Post(RotaAddDto model)
        {
            try
            {
                var rota = await _rotaService.Add(model);
                if (rota == null) return NoContent();

                return Ok(rota);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar adicionar eventos. Erro: {ex.Message}");
            }
        }

        // PUT api/Rota
        /// <summary>
        /// Altera um item na lista.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     PUT /Rota
        ///     {
        ///         "id": 1,
        ///         "nome": "GRU"
        ///     }
        ///
        /// </remarks>
        /// <param name="value"></param>
        /// <returns>Alteração de dados</returns>
        /// <response code="201">Retorna o item alterado</response>
        /// <response code="400">Se o item não for alterado</response>
        [HttpPut]
        public async Task<IActionResult> Update(RotaUpdateDto model)
        {
            try
            {
                var rota = await _rotaService.Update(model);
                if (rota == null) return NoContent();

                return Ok(rota);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar adicionar eventos. Erro: {ex.Message}");
            }
        }

        /// <summary>
        /// Deleta o item da lista.
        /// </summary>
        /// <returns>Deleta o item da lista e returna true.</returns>
        /// <response code="200">Returna true se o item for deletado</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _rotaService.Delete(id);
                if (result == false) return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar rotas. Erro: {ex.Message}");
            }
        }

    }
}