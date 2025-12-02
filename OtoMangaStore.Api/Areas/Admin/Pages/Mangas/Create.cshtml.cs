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
using MediatR;
using OtoMangaStore.Application.UseCases.Mangas.Commands.CreateManga;

using OtoMangaStore.Domain.Models;

namespace OtoMangaStore.Api.Areas.Admin.Pages.Mangas
{
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _uow;
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _env;

        public CreateModel(IUnitOfWork uow, IMediator mediator, IWebHostEnvironment env)
        {
            _uow = uow;
            _mediator = mediator;
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

            var command = new CreateMangaCommand
            {
                Title = Input.Title,
                Stock = Input.Stock,
                Description = Input.Synopsis,
                CategoryId = Input.CategoryId,
                AuthorId = Input.AuthorId,
                CoverImageUrl = Input.ImageUrl,
                Price = 0 // Default price, can be updated later
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
                command.CoverImageUrl = $"/images/{fileName}";
            }

            await _mediator.Send(command);

            TempData["Success"] = "Contenido creado correctamente";
            return RedirectToPage("Index");
        }
    }
}
