﻿using System;
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
    [Route("api/[controller]/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly ControleDeClientesContext _context;
        private readonly IDataRepository<Produto> _repository;

        public ProdutosController(ControleDeClientesContext context,
                                  IDataRepository<Produto> repository)
        {
            _context = context;
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<Produto> RecuperaListaDeProdutos()
        {
            return _context.Produtos.OrderBy(x => x.Descricao);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetProduto(int id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null)
            {
                return NotFound();
            }

            return Ok(produto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduto(int id, Produto produto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != produto.ProdutoId)
            {
                return BadRequest();
            }

            _context.Entry(produto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                _repository.Update(produto);
                var save = await _repository.SaveAsync(produto);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Produto atualizado com sucesso!");
        }

        // POST: api/Produtoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Produto>> PostProduto(Produto produto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Add(produto);
            await _repository.SaveAsync(produto);

            return CreatedAtAction("GetProduto", new { id = produto.ProdutoId }, produto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Dados inválidos!");
            }

            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            _repository.Delete(produto);
            await _repository.SaveAsync(produto);

            return Ok(produto);
        }


        /// <summary>
        /// O estoque do produto será atualizado de acordo com a quantidade informada
        /// </summary>
        /// <param name="id">Código do produto que será atualizado</param>
        /// <param name="quantidade">Quantidade do produto que irá ser adicionada ou retirada do estoque</param>
        /// <returns></returns>
        [HttpPut()]
        public async Task<IActionResult> AtualizaEstoqueProduto(int id, int quantidade)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var produto = await _context.Produtos.FindAsync(id);

                produto.Quantidade += quantidade;
                _repository.Update(produto);
                await _repository.SaveAsync(produto);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Estoque do produto atualizado com sucesso!");
        }

        private bool ProdutoExists(int id)
        {
            return _context.Produtos.Any(e => e.ProdutoId == id);
        }
    }
}
