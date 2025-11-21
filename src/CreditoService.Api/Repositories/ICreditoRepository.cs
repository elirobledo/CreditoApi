using System.Collections.Generic;
using System.Threading.Tasks;
using CreditoService.Api.Models;

namespace CreditoService.Api.Repositories
{
    public interface ICreditoRepository
    {
        Task<Credito?> GetByIdAsync(long id);
        Task<IEnumerable<Credito>> GetAllAsync();
        Task AddAsync(Credito credito);
        Task<bool> ExistsByNumeroCreditoAsync(string numeroCredito);
        Task SaveChangesAsync();


    }
}
