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
using OtoMangaStore.Application.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
    builder.Services.AddDbContext<OtoDbContext>(options =>
        options.UseInMemoryDatabase("OtoMangaDev"));
}
else
{
    builder.Services.AddDbContext<OtoDbContext>(options =>
        options.UseNpgsql(connectionString));
}

// ✅ AddIdentity con SignInManager
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.User.RequireUniqueEmail = true;

        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 6;
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
        options.Lockout.AllowedForNewUsers = true;
        options.SignIn.RequireConfirmedEmail = false;
        options.SignIn.RequireConfirmedPhoneNumber = false;
    })
    .AddEntityFrameworkStores<OtoDbContext>()  // Asegúrate de que tu DbContext esté configurado
    .AddDefaultTokenProviders()  // Esto habilita tanto SignInManager como UserManager
    .AddSignInManager();  // Aunque en la mayoría de los casos esto no es necesario, agregámoslo explícitamente

// ✅ AUTENTICACIÓN DUAL: JWT + Cookies
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];

if (!string.IsNullOrWhiteSpace(jwtKey))
{
    builder.Services.AddAuthentication(options =>
    {
        // Por defecto usar JWT
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
    })
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.LoginPath = "/api/auth/admin/login";
        options.LogoutPath = "/api/auth/logout";
        options.AccessDeniedPath = "/api/auth/access-denied";
        options.ExpireTimeSpan = TimeSpan.FromHours(24);
        options.SlidingExpiration = true;
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.Cookie.Name = "OtoMangaStore.Admin.Auth";
        
        options.Events.OnRedirectToLogin = context =>
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        };
        options.Events.OnRedirectToAccessDenied = context =>
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            return Task.CompletedTask;
        };
    });
}
else
{
    // Si no hay JWT configurado, usar solo Cookies
    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = "/api/auth/admin/login";
            options.LogoutPath = "/api/auth/logout";
            options.AccessDeniedPath = "/api/auth/access-denied";
            options.ExpireTimeSpan = TimeSpan.FromHours(24);
            options.SlidingExpiration = true;
            options.Cookie.HttpOnly = true;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.Cookie.SameSite = SameSiteMode.Lax;
            options.Cookie.Name = "OtoMangaStore.Admin.Auth";
            
            options.Events.OnRedirectToLogin = context =>
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return Task.CompletedTask;
            };
            options.Events.OnRedirectToAccessDenied = context =>
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                return Task.CompletedTask;
            };
        });
}

// ✅ Configurar las cookies de Identity para usar el mismo esquema
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "OtoMangaStore.Admin.Auth";
    options.ExpireTimeSpan = TimeSpan.FromHours(24);
    options.SlidingExpiration = true;
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Lax;
    
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.CompletedTask;
    };
    options.Events.OnRedirectToAccessDenied = context =>
    {
        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        return Task.CompletedTask;
    };
});

// ✅ CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
            "http://localhost:3000",
            "http://localhost:5173",
            "https://tu-dominio.com"
        )
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
    });
    
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddDataProtection();

// MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(OtoMangaStore.Application.UseCases.Mangas.Commands.CreateManga.CreateMangaCommand).Assembly));

// Repositories
builder.Services.AddScoped<IMangaRepository, MangaRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IClickMetricsRepository, ClickMetricsRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Services (Legacy removed)

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors(app.Environment.IsDevelopment() ? "AllowAll" : "AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers(); 
app.MapRazorPages(); // ✅ Habilitar rutas de Razor Pages

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
            logger.LogInformation("Sample Manga: {Title}, Category: {Cat}, Author: {Auth}, CurrentPrice: {Price}", 
                sample.Title, sample.Category?.Name, sample.Author?.Name, price);
        }
    }
}

app.Run();