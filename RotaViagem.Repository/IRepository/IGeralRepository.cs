using System.Threading.Tasks;
using RotaViagem.Domain;

namespace RotaViagem.Repository.IRepository
{
    public interface IGeralRepository
    {
        //GERAL
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void DeleteRange<T>(T[] entity) where T : class;
        Task<bool> SaveChangesAsync();
    }
}