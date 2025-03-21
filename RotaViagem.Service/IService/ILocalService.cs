using RotaViagem.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotaViagem.Service.IService
{
    public interface ILocalService
    {
        Task<LocalDto> Add(LocalAddDto model);
        Task<LocalDto> Update(LocalUpdateDto model);
        Task<bool> Delete(int id);
        Task<IList<LocalDto>> GetAllAsync();
        Task<LocalDto> GetByIdAsync(int id);
        Task<LocalDto> GetByNomeAsync(string nome);
    }
}
