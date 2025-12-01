using System;
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
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _uow;
        private readonly IWebHostEnvironment _env;

        public EditModel(IUnitOfWork uow, IWebHostEnvironment env)
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

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var item = await _uow.Mangas.GetByIdAsync(id);
            if (item == null) return NotFound();

            Input = new ContentEditModel
            {
                Id = item.Id,
                Title = item.Title,
                Stock = item.Stock,
                Synopsis = item.Synopsis,
                ImageUrl = item.ImageUrl,
                CategoryId = item.CategoryId,
                AuthorId = item.AuthorId,
                IsActive = false
            };

            AuthorsSelect = (await _uow.Authors.GetAllAsync()).Select(a => new SelectListItem(a.Name, a.Id.ToString()));
            CategoriesSelect = (await _uow.Categories.GetAllAsync()).Select(c => new SelectListItem(c.Name, c.Id.ToString()));

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                AuthorsSelect = (await _uow.Authors.GetAllAsync()).Select(a => new SelectListItem(a.Name, a.Id.ToString()));
                CategoriesSelect = (await _uow.Categories.GetAllAsync()).Select(c => new SelectListItem(c.Name, c.Id.ToString()));
                return Page();
            }

            var item = await _uow.Mangas.GetByIdAsync(Input.Id);
            if (item == null) return NotFound();

            item.Title = Input.Title;
            item.Stock = Input.Stock;
            item.Synopsis = Input.Synopsis;
            item.CategoryId = Input.CategoryId;
            item.AuthorId = Input.AuthorId;
            item.ImageUrl = Input.ImageUrl;

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

            await _uow.Mangas.UpdateAsync(item);
            await _uow.SaveChangesAsync();

            TempData["Success"] = "Contenido actualizado";
            return RedirectToPage("Index");
        }
    }
}
