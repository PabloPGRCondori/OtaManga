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
using OtoMangaStore.Application.Interfaces.Services;
using OtoMangaStore.Application.DTOs.Mangas;
using OtoMangaStore.Domain.Models;

namespace OtoMangaStore.Api.Areas.Admin.Pages.Mangas
{
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _uow;
        private readonly IMangaService _mangaService;
        private readonly IWebHostEnvironment _env;

        public CreateModel(IUnitOfWork uow, IMangaService mangaService, IWebHostEnvironment env)
        {
            _uow = uow;
            _mangaService = mangaService;
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

            var dto = new CreateMangaDto
            {
                Title = Input.Title,
                Stock = Input.Stock,
                Synopsis = Input.Synopsis,
                CategoryId = Input.CategoryId,
                AuthorId = Input.AuthorId,
                ImageUrl = Input.ImageUrl
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
                dto.ImageUrl = $"/images/{fileName}";
            }

            await _mangaService.CreateMangaAsync(dto);

            TempData["Success"] = "Contenido creado correctamente";
            return RedirectToPage("Index");
        }
    }
}
