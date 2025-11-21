using Confluent.Kafka;
using CreditoService.Api.Models;
using CreditoService.Api.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;


namespace CreditoService.Api.Background
{
    // Hosted service que consome mensagens do Kafka a cada 500ms
    public class KafkaConsumerHostedService : BackgroundService
    {
        private readonly ILogger<KafkaConsumerHostedService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly ConsumerConfig _config;


        public KafkaConsumerHostedService(
            ILogger<KafkaConsumerHostedService> logger,
            IServiceProvider serviceProvider,
            IConfiguration configuration)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;

            _config = new ConsumerConfig
            {
                BootstrapServers = configuration["Kafka:BootstrapServers"],
                GroupId = configuration["Kafka:GroupId"],
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false
            };
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var consumer = new ConsumerBuilder<Ignore, string>(_config).Build();
            consumer.Subscribe("creditos-topic");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var result = consumer.Consume(TimeSpan.FromMilliseconds(500));
                    if (result == null)
                        continue;

                    _logger.LogInformation("Mensagem Kafka recebida: {value}", result.Message.Value);

                    var credito = JsonConvert.DeserializeObject<Credito>(result.Message.Value);

                    if (credito != null)
                    {
                        using var scope = _serviceProvider.CreateScope();
                        var creditoService = scope.ServiceProvider.GetRequiredService<ICreditoService>();

                        await creditoService.InserirCreditoAsync(credito);
                    }

                    consumer.Commit(result);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro no consumer Kafka");
                }
            }
        }
    }
}

