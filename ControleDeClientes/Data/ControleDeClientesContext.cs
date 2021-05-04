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
    }
}
