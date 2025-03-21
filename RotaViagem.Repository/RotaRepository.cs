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
    public class RotaRepository : GeralRepository, IRotaRepository
    {
        private readonly RotaViagemContext _context;
        public RotaRepository(RotaViagemContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Rota> GetByIdAsync(int id)
        {
            IQueryable<Rota> query = _context.Rotas
                .Include(rota => rota.LocalOrigem)
                .Include(rota => rota.LocalDestino);

            query = query.AsNoTracking().Where(p => p.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Rota[]> GetAllAsync()
        {
            IQueryable<Rota> query = _context.Rotas
                .Include(rota => rota.LocalOrigem)
                .Include(rota => rota.LocalDestino);

            query = query.AsNoTracking();

            return await query.ToArrayAsync();
        }
    }
}
