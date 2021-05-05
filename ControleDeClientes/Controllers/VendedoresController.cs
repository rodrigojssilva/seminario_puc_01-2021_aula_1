using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControleDeClientes.Data;
using ControleDeClientes.Models;

namespace ControleDeClientes.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class VendedoresController : ControllerBase
    {
        private readonly ControleDeClientesContext _context;
        private readonly IDataRepository<Vendedor> _repository;

        public VendedoresController(ControleDeClientesContext context,
                                    IDataRepository<Vendedor> repository)
        {
            _context = context;
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<Vendedor> GetVendedor()
        {
            return _context.Vendedores.Include(x => x.Vendas).OrderBy(x => x.Nome);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vendedor>> GetVendedor(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vendedor = await _context.Vendedores.FindAsync(id);

            if (vendedor == null)
            {
                return NotFound();
            }

            return Ok(vendedor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVendedor(int id, Vendedor vendedor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vendedor.VendedorId)
            {
                return BadRequest();
            }

            _context.Entry(vendedor).State = EntityState.Modified;

            try
            {
                _repository.Update(vendedor);
                var save = await _repository.SaveAsync(vendedor);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendedorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Vendedor atualizado com sucesso!");
        }

        [HttpPost]
        public async Task<ActionResult<Vendedor>> PostVendedor(Vendedor vendedor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Add(vendedor);
            await _repository.SaveAsync(vendedor);

            return CreatedAtAction("GetVendedor", new { id = vendedor.VendedorId }, vendedor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendedor(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Dados inválidos!");
            }

            var vendedor = await _context.Vendedores.FindAsync(id);
            if (vendedor == null)
            {
                return NotFound();
            }

            _repository.Delete(vendedor);
            await _repository.SaveAsync(vendedor);

            return NoContent();
        }

        private bool VendedorExists(int id)
        {
            return _context.Vendedores.Any(e => e.VendedorId == id);
        }
    }
}
