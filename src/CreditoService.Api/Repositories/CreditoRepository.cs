using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CreditoService.Api.Models;
using CreditoService.Api.Data;


namespace CreditoService.Api.Repositories
{
    public class CreditoRepository : ICreditoRepository
    {
        private readonly AppDbContext _context;
        public CreditoRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Credito credito)
        {
            await _context.Creditos.AddAsync(credito);
        }
        public async Task<IEnumerable<Credito>> GetAllAsync()
        {
            return await _context.Creditos.AsNoTracking().ToListAsync();
        }
        public async Task<Credito?> GetByIdAsync(long id)
        {
            return await _context.Creditos.FindAsync(id);
        }
        public async Task<bool> ExistsByNumeroCreditoAsync(string
        numeroCredito)
        {
            return await _context.Creditos.AnyAsync(c => c.NumeroCredito ==
            numeroCredito);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
