using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RotaViagem.Domain.Identity;

namespace RotaViagem.Repository.IRepository
{
    public interface IUserRepository : IGeralRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByUserNameAsync(string userName);
    }
}