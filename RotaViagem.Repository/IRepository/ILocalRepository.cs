using RotaViagem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotaViagem.Repository.IRepository
{
    public interface ILocalRepository : IGeralRepository
    {
        Task<Local> GetByIdAsync(int id);
        Task<Local> GetByNomeAsync(string nome);
        Task<Local[]> GetAllAsync();
    }
}