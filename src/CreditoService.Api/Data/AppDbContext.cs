using CreditoService.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace CreditoService.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Credito> Creditos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Credito>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.NumeroCredito).IsUnique();

                // Mapear nomes das colunas caso necessário já feito via attributes
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
