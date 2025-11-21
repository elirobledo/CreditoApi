using CreditoService.Api.Data;
using CreditoService.Api.Models;
using CreditoService.Api.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CreditoService.Tests.Repositories
{
    public class CreditoRepositoryTests
    {
        private AppDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public async Task AddAndGetCredito()
        {
            var db = CreateDbContext();
            var repo = new CreditoRepository(db);
            var credit = new Credito
            {
                NumeroCredito = "NC-1",
                NumeroNfse = "NF-1",
                DataConstituicao = DateTime.UtcNow,
                ValorIssqn = 100m,
                TipoCredito = "TIPO1",
                SimplesNacional = false,
                Aliquota = 2.5m,
                ValorFaturado = 500m,
                ValorDeducao = 10m,
                BaseCalculo = 490m
            };
            await repo.AddAsync(credit);
            await repo.SaveChangesAsync();
            var all = await repo.GetAllAsync();
            Assert.Contains(all, c => c.NumeroCredito == "NC-1");
        }
    }
}
