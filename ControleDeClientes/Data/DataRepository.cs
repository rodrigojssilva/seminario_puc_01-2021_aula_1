using System.Threading.Tasks;

namespace ControleDeClientes.Data
{
    public class DataRepository<T> : IDataRepository<T> where T : class
    {
        private readonly ControleDeClientesContext _context;

        public DataRepository(ControleDeClientesContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {            
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<T> SaveAsync(T entity)
        {
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
