using System.Collections.Generic;
using System.Threading.Tasks;
using CreditoService.Api.Models;

namespace CreditoService.Api.Services
{
    public interface ICreditoService
    {
        Task<IEnumerable<Credito>> GetAllAsync();
        Task<Credito?> GetByIdAsync(long id);
        Task<Credito> CreateAsync(Credito credito);

        // usado pelo KafkaConsumer
        Task InserirCreditoAsync(Credito credito);
    }
}
