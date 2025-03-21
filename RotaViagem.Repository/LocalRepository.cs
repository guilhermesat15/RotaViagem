using Microsoft.EntityFrameworkCore;
using RotaViagem.Domain;
using RotaViagem.Repository.Contextos;
using RotaViagem.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotaViagem.Repository
{
    public class LocalRepository : GeralRepository, ILocalRepository
    {
        private readonly RotaViagemContext _context;
        public LocalRepository(RotaViagemContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Local> GetByIdAsync(int id)
        {
            IQueryable<Local> query = _context.Locals;
            query = query.AsNoTracking().Where(p => p.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Local[]> GetAllAsync()
        {
            IQueryable<Local> query = _context.Locals;
            query = query.AsNoTracking();

            return await query.ToArrayAsync();
        }

        public async Task<Local> GetByNomeAsync(string nome)
        {
            IQueryable<Local> query = _context.Locals;
            query = query.AsNoTracking().Where(p => p.Nome == nome);

            return await query.FirstOrDefaultAsync();
        }
    }
}
