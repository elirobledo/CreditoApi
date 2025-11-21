using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;using CreditoService.Api.Data;
using CreditoService.Api.Repositories;
using CreditoService.Api.Services;
using CreditoService.Api.Background;
using CreditoService.Api.Mappings;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Configuration
var configuration = builder.Configuration;

//Docker
var isDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";

if (isDocker)
{
    builder.Configuration["ConnectionStrings:DefaultConnection"] =
        "Host=postgres;Port=5432;Database=CreditoDb;Username=postgres;Password=postgres";

    builder.Configuration["Kafka:BootstrapServers"] = "kafka:9092";
}

// Add DbContext
var conn = configuration.GetConnectionString("DefaultConnection") ??
"Host=localhost;Port=5432;Database=CreditoDb;Username=postgres;Password=postgres";
///var conn = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(conn));

// DI registrations
builder.Services.AddScoped<ICreditoRepository, CreditoRepository>();
builder.Services.AddScoped<ICreditoService, CreditoServices>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

// Hosted service (Kafka consumer)
builder.Services.AddHostedService<KafkaConsumerHostedService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Apply migrations at startup (optional, useful in dev)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.MapControllers();
app.Run();