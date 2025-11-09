using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using OtoMangaStore.Application.Interfaces.Repositories;
using OtoMangaStore.Infrastructure.Persistence; 
using OtoMangaStore.Infrastructure;
using OtoMangaStore.Infrastructure.Repositories;

//agreguen los usings OJO
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 

// Repositorios
builder.Services.AddScoped<IMangaRepository, MangaRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (connectionString == null)
{
    throw new InvalidOperationException("La cadena de conexión 'DefaultConnection' no fue encontrada.");
}

builder.Services.AddDbContext<OtoDbContext>(options =>
    options.UseNpgsql(connectionString)
);

// Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();
app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

app.UseAuthorization();
app.MapControllers(); 
app.Run();