using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RotaViagem.Service.Dtos;

namespace RotaViagem.Service.IService
{
    public interface ITokenService
    {
        Task<string> CreateToken(UserUpdateDto userUpdateDto);
    }
}