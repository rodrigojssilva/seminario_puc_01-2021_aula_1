using ControleDeClientes.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleDeClientes.Data
{
    public class ControleDeClientesContext : DbContext
    {
        public ControleDeClientesContext (DbContextOptions<ControleDeClientesContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Vendedor> Vendedores { get; set; }

        public DbSet<Produto> Produtos { get; set; }

        public DbSet<Venda> Vendas { get; set; }
        public DbSet<VendaItem> VendasItens { get; set; }
    }
}
