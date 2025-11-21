using CreditoService.Api.Models;
using CreditoService.Api.Repositories;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace CreditoService.Api.Services
{
        public class CreditoServices : ICreditoService
        {
            private readonly ICreditoRepository _repository;

            public CreditoServices(ICreditoRepository repository)
            {
                _repository = repository;
            }

            public async Task<Credito> CreateAsync(Credito credito)
            {
                var exists = await _repository.ExistsByNumeroCreditoAsync(credito.NumeroCredito);
                if (exists)
                    return credito; // ou lançar exceção conforme a regra

                await _repository.AddAsync(credito);
                await _repository.SaveChangesAsync();
                return credito;
            }

            public async Task<IEnumerable<Credito>> GetAllAsync()
            {
                return await _repository.GetAllAsync();
            }

            public async Task<Credito?> GetByIdAsync(long id)
            {
                return await _repository.GetByIdAsync(id);
            }

            public async Task InserirCreditoAsync(Credito credito)
            {
                await _repository.AddAsync(credito);
                await _repository.SaveChangesAsync();  // 🔥 importante para Kafka
            }
        }
    }

