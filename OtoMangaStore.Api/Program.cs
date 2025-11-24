using Microsoft.EntityFrameworkCore;
using OtoMangaStore.Infrastructure.Persistence;
using OtoMangaStore.Application.Interfaces.Repositories;
using OtoMangaStore.Infrastructure.Repositories;
using OtoMangaStore.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using OtoMangaStore.Domain.Models;
using OtoMangaStore.Api.Seed;
using OtoMangaStore.Application.Interfaces.Services;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (connectionString == null)
{
    throw new InvalidOperationException("La cadena de conexión 'DefaultConnection' no fue encontrada.");
}

builder.Services.AddDbContext<OtoDbContext>(options =>
    options.UseNpgsql(connectionString)
);

builder.Services.AddIdentityCore<ApplicationUser>(options =>
{
    options.User.RequireUniqueEmail = true;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<OtoDbContext>()
    .AddDefaultTokenProviders();

var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];

if (!string.IsNullOrWhiteSpace(jwtKey))
{
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = true;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });
}

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddScoped<IMangaRepository, MangaRepository>();
builder.Services.AddScoped<IPriceHistoryRepository, PriceHistoryRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OtoMangaStore.Application.Services.OrderService>();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers(); 

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var db = services.GetRequiredService<OtoDbContext>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var logger = services.GetRequiredService<ILoggerFactory>().CreateLogger("Seed");
    await SeedData.SeedAsync(db, userManager, roleManager, builder.Configuration, logger);

    if (app.Environment.IsDevelopment())
    {
        var uow = services.GetRequiredService<IUnitOfWork>();
        var sample = await uow.Mangas.GetMangaDetailsAsync(1);
        if (sample != null)
        {
            var price = await uow.PriceHistory.GetCurrentPriceAsync(sample.Id);
            logger.LogInformation("Sample Manga: {Title}, Category: {Cat}, Author: {Auth}, CurrentPrice: {Price}", sample.Title, sample.Category?.Name, sample.Author?.Name, price);
        }
    }
}
app.Run();
