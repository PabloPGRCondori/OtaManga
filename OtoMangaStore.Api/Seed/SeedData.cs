using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OtoMangaStore.Domain.Models;
using OtoMangaStore.Infrastructure.Persistence;

namespace OtoMangaStore.Api.Seed
{
    public static class SeedData
    {
        public static async Task SeedAsync(
            OtoDbContext db,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration config,
            ILogger logger)
        {
            var roles = new[] { "Admin", "Editor", "Client" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var adminEmail = config["Admin:Email"];
            var adminPassword = config["Admin:Password"];

            if (!string.IsNullOrWhiteSpace(adminEmail) && !string.IsNullOrWhiteSpace(adminPassword))
            {
                var adminUser = await userManager.FindByEmailAsync(adminEmail);
                if (adminUser == null)
                {
                    adminUser = new ApplicationUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
                    var createResult = await userManager.CreateAsync(adminUser, adminPassword);
                    if (createResult.Succeeded)
                    {
                        await userManager.AddToRoleAsync(adminUser, "Admin");
                    }
                    else
                    {
                        logger.LogWarning("Admin user creation failed: {Errors}", string.Join(", ", createResult.Errors.Select(e => e.Description)));
                    }
                }
            }

            if (!db.Categories.Any())
            {
                db.Categories.AddRange(
                    new Category { Name = "Manga" },
                    new Category { Name = "LightNovel" },
                    new Category { Name = "Anime" }
                );
                await db.SaveChangesAsync();
            }

            var author = db.Authors.FirstOrDefault();
            if (author == null)
            {
                author = new Author { Name = "Autor Demo", Description = "Autor inicial para pruebas" };
                db.Authors.Add(author);
                await db.SaveChangesAsync();
            }

            var mangaCategory = db.Categories.First(c => c.Name == "Manga");
            if (!db.Mangas.Any())
            {
                var item = new Content
                {
                    Title = "Manga Demo",
                    Stock = 100,
                    Synopsis = "Sinopsis de prueba",
                    ImageUrl = "https://example.com/manga-demo.jpg",
                    CategoryId = mangaCategory.Id,
                    AuthorId = author.Id
                };
                db.Mangas.Add(item);
                await db.SaveChangesAsync();

                db.PriceHistories.Add(new PriceHistory
                {
                    MangaId = item.Id,
                    Price = 9.99m,
                    EffectiveDate = DateTime.UtcNow
                });
                await db.SaveChangesAsync();
            }
        }
    }
}
