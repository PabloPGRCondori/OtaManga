using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OtoMangaStore.Api.Areas.Admin.Models;
using OtoMangaStore.Application.Interfaces.Repositories;
using OtoMangaStore.Domain.Models;

namespace OtoMangaStore.Api.Areas.Admin.Pages.Mangas
{
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _uow;
        private readonly IWebHostEnvironment _env;

        public CreateModel(IUnitOfWork uow, IWebHostEnvironment env)
        {
            _uow = uow;
            _env = env;
        }

        [BindProperty]
        public ContentEditModel Input { get; set; } = new ContentEditModel();

        [BindProperty]
        public IFormFile UploadImage { get; set; } = null!;

        public IEnumerable<SelectListItem> AuthorsSelect { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> CategoriesSelect { get; set; } = Enumerable.Empty<SelectListItem>();

        public async Task OnGetAsync()
        {
            AuthorsSelect = (await _uow.Authors.GetAllAsync()).Select(a => new SelectListItem(a.Name, a.Id.ToString()));
            CategoriesSelect = (await _uow.Categories.GetAllAsync()).Select(c => new SelectListItem(c.Name, c.Id.ToString()));
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync();
                return Page();
            }

            var item = new Content
            {
                Title = Input.Title,
                Stock = Input.Stock,
                Synopsis = Input.Synopsis,
                CategoryId = Input.CategoryId,
                AuthorId = Input.AuthorId,
                ImageUrl = Input.ImageUrl,
                // IsActive property: the domain model content doesn't include IsActive by default.
                // If you have this property, set it. Otherwise add it to the content entity.
            };

            // Upload image optional
            if (UploadImage != null && UploadImage.Length > 0)
            {
                var uploads = Path.Combine(_env.WebRootPath ?? "wwwroot", "images");
                if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(UploadImage.FileName)}";
                var filePath = Path.Combine(uploads, fileName);
                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    await UploadImage.CopyToAsync(fs);
                }
                item.ImageUrl = $"/images/{fileName}";
            }

            await _uow.Mangas.AddAsync(item);
            await _uow.SaveChangesAsync();

            TempData["Success"] = "Contenido creado correctamente";
            return RedirectToPage("Index");
        }
    }
}
