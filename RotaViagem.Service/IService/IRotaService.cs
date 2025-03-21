using RotaViagem.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotaViagem.Service.IService
{
    public interface IRotaService
    {
        Task<RotaDto> Add(RotaAddDto model);
        Task<RotaDto> Update(RotaUpdateDto model);
        Task<bool> Delete(int id);
        Task<IList<RotaDto>> GetAllAsync();
        Task<RotaDto> GetByIdAsync(int id);
        Task<RotaResultadoDto> GetCalculeRotaAsync(string nomeLocalOrigem, string nomeLocalDestino);
    }
}
