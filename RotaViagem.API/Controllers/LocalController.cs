using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RotaViagem.Service.Dtos;
using RotaViagem.Service.IService;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;
using RotaViagem.Service;

namespace RotaViagem.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LocalController : ControllerBase
    {
        private readonly ILocalService _localService;

        public LocalController(ILocalService localService)
        {
            _localService = localService;
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
                var local = await _localService.GetByIdAsync(id);
                if (local == null) return NoContent();

                return Ok(local);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar locais. Erro: {ex.Message}");
            }
        }

        // GET api/Local
        /// <summary>
        /// Lista os itens.
        /// </summary>
        /// <returns>Os itens da list</returns>
        /// <response code="200">Returna os itens da list cadastrados</response>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var locals = await _localService.GetAllAsync();
                if (locals == null) return NoContent();

                return Ok(locals);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar locais");
            }
        }

        // POST api/Local
        /// <summary>
        /// Cria um item na lista.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST /Local
        ///     {
        ///         "nome": "GRU"
        ///     }
        ///
        /// </remarks>
        /// <param name="value"></param>
        /// <returns>Um novo item criado</returns>
        /// <response code="201">Retorna o novo item criado</response>
        /// <response code="400">Se o item não for criado</response>    
        [HttpPost]
        public async Task<IActionResult> Post(LocalAddDto model)
        {
            try
            {
                var local = await _localService.Add(model);
                if (local == null) return NoContent();

                return Ok(local);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar adicionar locais. Erro: {ex.Message}");
            }
        }

        // PUT api/Local
        /// <summary>
        /// Altera um item da lista.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     PUT /Local
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
        public async Task<IActionResult> Update(LocalUpdateDto model)
        {
            try
            {
                var local = await _localService.Update(model);
                if (local == null) return NoContent();

                return Ok(local);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar adicionar locais. Erro: {ex.Message}");
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
                var result = await _localService.Delete(id);
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