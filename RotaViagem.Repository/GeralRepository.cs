using System.Threading.Tasks;
using RotaViagem.Repository.Contextos;
using RotaViagem.Repository.IRepository;

namespace RotaViagem.Repository
{
    public class GeralRepository : IGeralRepository
    {
        private readonly RotaViagemContext _context;
        public GeralRepository(RotaViagemContext context)
        {
            _context = context;

        }
        public void Add<T>(T entity) where T : class
        {
            _context.AddAsync(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void DeleteRange<T>(T[] entityArray) where T : class
        {
            _context.RemoveRange(entityArray);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}