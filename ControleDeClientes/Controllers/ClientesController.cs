using ControleDeClientes.Data;
using ControleDeClientes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ControleDeClientes.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [SwaggerTag(description: "Controla todos os dados cadastrais dos clientes do sistema.")]
    public class ClientesController : ControllerBase
    {
        private readonly ControleDeClientesContext _context;
        private readonly IDataRepository<Cliente> _repository;

        public ClientesController(ControleDeClientesContext context,
                                  IDataRepository<Cliente> repository)
        {
            _context = context;
            _repository = repository;
        }

        /// <summary>
        /// Retornar todos os clientes ordenados em ordem alfabética.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Cliente> GetCliente()
        {
            return _context.Clientes.OrderBy(x => x.Nome);
        }

        /// <summary>
        /// Retorna um único cliente de acordo com o ID buscado!
        /// </summary>
        /// <param name="id">O id do cliente a ser buscado</param>
        /// <returns></returns>
        /// <response code="417">Se o Id não puder ser buscado, será disparada uma exception.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id == 999)
            {
                return new ObjectResult("Você não pode buscar pelo cliente id 999") { StatusCode = (int)HttpStatusCode.ExpectationFailed };
            }

            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        /// <summary>
        /// Atualizar os dados cadastrais do cliente
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cliente"></param>
        /// <returns></returns>
        /// <remarks>
        /// Exemplo: 
        /// 
        ///     PUT
        ///     {
        ///         "clienteId": 1,
        ///         "nome": "Nome do Cliente",
        ///         "documento": "111.222.333-96"
        ///     }
        ///     
        /// </remarks>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cliente.ClienteId)
            {
                return BadRequest();
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                _repository.Update(cliente);
                var save = await _repository.SaveAsync(cliente);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Cliente atualizado com sucesso!");
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Add(cliente);
            await _repository.SaveAsync(cliente);

            return CreatedAtAction("GetCliente", new { id = cliente.ClienteId }, cliente);
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(200, type: typeof(string), description: "Cliente deletado com sucesso!")]
        [SwaggerResponse(404, type: typeof(string), description: "Cliente não encontrado!")]
        [SwaggerResponse(500, type: typeof(string), description: "Erro interno!")]
        [SwaggerResponse(417, type: typeof(string), description: "Se o Id não puder ser buscado, será disparada uma exception!")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Dados inválidos!");
            }

            if (id == 999)
            {
                return new ObjectResult("Você não pode buscar pelo cliente id 999") { StatusCode = (int)HttpStatusCode.ExpectationFailed };
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _repository.Delete(cliente);
            await _repository.SaveAsync(cliente);

            return Ok(cliente);
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.ClienteId == id);
        }
    }
}
