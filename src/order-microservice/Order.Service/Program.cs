using ECommerce.Shared.Infrastructure.RabbitMq;
using Order.Service.Endpoints;
using Order.Service.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IOrderStore, InMemoryOrderStore>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRabbitMqEventBus(builder.Configuration);

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.RegisterEndpoints();

app.UseHttpsRedirection();

await app.RunAsync();