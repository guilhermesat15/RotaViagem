using RotaViagem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotaViagem.Repository.IRepository
{
    public interface IRotaRepository: IGeralRepository
    {
        Task<Rota> GetByIdAsync(int id);
        Task<Rota[]> GetAllAsync();
    }
}
