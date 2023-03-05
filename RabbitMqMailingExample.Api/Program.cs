using RabbitMqMailingExample.Api.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var assm = Assembly.GetExecutingAssembly();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assm));
builder.Services.AddRabbitMqService();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
