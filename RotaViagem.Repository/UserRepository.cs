using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RotaViagem.Domain.Identity;
using RotaViagem.Repository.Contextos;
using RotaViagem.Repository.IRepository;

namespace RotaViagem.Repository
{
    public class UserRepository : GeralRepository, IUserRepository
    {
        private readonly RotaViagemContext _context;

        public UserRepository(RotaViagemContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            return await _context.Users
                                 .SingleOrDefaultAsync(user => user.UserName == userName.ToLower());
        }
    }
}