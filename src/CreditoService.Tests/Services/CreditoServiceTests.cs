using System.Threading.Tasks;
using Xunit;
using Moq;
using CreditoService.Api.Repositories;
using CreditoService.Api.Services;
using CreditoService.Api.Models;
using System.Collections.Generic;
using System.Linq;
using System;


namespace CreditoService.Tests.Services
{
    public class CreditoServiceTests
    {
        [Fact]
        public async Task CreateAsync_PreventsDuplicateByNumeroCredito()
        {
            var mockRepo = new Mock<ICreditoRepository>();
            var credito = new Credito { NumeroCredito = "NC-ABC" };
            mockRepo.Setup(r => r.ExistsByNumeroCreditoAsync(It.IsAny<string>())).ReturnsAsync(true);
            var service = new CreditoServices(mockRepo.Object);
            var result = await service.CreateAsync(credito);
            // If duplicate exists, service returns the same object (design choice above)
            Assert.Equal("NC-ABC", result.NumeroCredito);
            mockRepo.Verify(r => r.AddAsync(It.IsAny<Credito>()),
            Times.Never);
        }

        [Fact]
        public async Task CreateAsync_AddsWhenNotExists()
        {
            var mockRepo = new Mock<ICreditoRepository>();
            var credito = new Credito { NumeroCredito = "NC-NEW" };
            mockRepo.Setup(r => r.ExistsByNumeroCreditoAsync("NCNEW")).ReturnsAsync(false);
            mockRepo.Setup(r =>
            r.AddAsync(It.IsAny<Credito>())).Returns(Task.CompletedTask);
            mockRepo.Setup(r =>
            r.SaveChangesAsync()).Returns(Task.CompletedTask);
            var service = new CreditoServices(mockRepo.Object);
            var result = await service.CreateAsync(credito);
            mockRepo.Verify(r => r.AddAsync(It.IsAny<Credito>()),
            Times.Once);
            mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
    }
}
