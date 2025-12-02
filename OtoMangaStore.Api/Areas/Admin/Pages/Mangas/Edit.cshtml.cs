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
using MediatR;
using OtoMangaStore.Application.UseCases.Mangas.Queries.GetMangaById;
using OtoMangaStore.Application.UseCases.Mangas.Commands.UpdateManga;

using OtoMangaStore.Domain.Models;

namespace OtoMangaStore.Api.Areas.Admin.Pages.Mangas
{
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _uow;
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _env;

        public EditModel(IUnitOfWork uow, IMediator mediator, IWebHostEnvironment env)
        {
            _uow = uow;
            _mediator = mediator;
            _env = env;
        }

        [BindProperty]
        public ContentEditModel Input { get; set; } = new ContentEditModel();

        [BindProperty]
        public IFormFile? UploadImage { get; set; }

        public IEnumerable<SelectListItem> AuthorsSelect { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> CategoriesSelect { get; set; } = Enumerable.Empty<SelectListItem>();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var item = await _mediator.Send(new GetMangaByIdQuery(id));
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
                Price = 0m
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

            var command = new UpdateMangaCommand
            {
                Id = Input.Id,
                Title = Input.Title,
                Stock = Input.Stock,
                Description = Input.Synopsis,
                CategoryId = Input.CategoryId,
                AuthorId = Input.AuthorId,
                CoverImageUrl = Input.ImageUrl,
                Price = Input.Price
            };

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

            try 
            {
                await _mediator.Send(command);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            TempData["Success"] = "Contenido actualizado";
            return RedirectToPage("Index");
        }
    }
}
