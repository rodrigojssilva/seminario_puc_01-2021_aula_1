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
    [ApiController]
    public class VendedoresController : ControllerBase
    {
        private readonly ControleDeClientesContext _context;

        public VendedoresController(ControleDeClientesContext context)
        {
            _context = context;
        }

        // GET: api/Vendedors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vendedor>>> GetVendedor()
        {
            return await _context.Vendedores.ToListAsync();
        }

        // GET: api/Vendedors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vendedor>> GetVendedor(int id)
        {
            var vendedor = await _context.Vendedores.FindAsync(id);

            if (vendedor == null)
            {
                return NotFound();
            }

            return vendedor;
        }

        // PUT: api/Vendedors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVendedor(int id, Vendedor vendedor)
        {
            if (id != vendedor.VendedorId)
            {
                return BadRequest();
            }

            _context.Entry(vendedor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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

            return NoContent();
        }

        // POST: api/Vendedors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vendedor>> PostVendedor(Vendedor vendedor)
        {
            _context.Vendedores.Add(vendedor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVendedor", new { id = vendedor.VendedorId }, vendedor);
        }

        // DELETE: api/Vendedors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendedor(int id)
        {
            var vendedor = await _context.Vendedores.FindAsync(id);
            if (vendedor == null)
            {
                return NotFound();
            }

            _context.Vendedores.Remove(vendedor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VendedorExists(int id)
        {
            return _context.Vendedores.Any(e => e.VendedorId == id);
        }
    }
}
