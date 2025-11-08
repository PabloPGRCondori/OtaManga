using Microsoft.AspNetCore.Mvc;
using OtoMangaStore.Application.DTOs;
using OtoMangaStore.Application.Interfaces.Repositories;

namespace OtoMangaStore.Api.Controllers;

[ApiController]
[Route("api/manga")] // Ruta específica
public class MangaController : ControllerBase
{
    private readonly IMangaRepository _mangaRepository;

    // Inyectamos la interfaz (recibirá el MangaRepository REAL)
    public MangaController(IMangaRepository mangaRepository)
    {
        _mangaRepository = mangaRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MangaDto>>> GetMangaCatalog()
    {
        var mangas = await _mangaRepository.GetAllAsync();

        // Convierte los modelos de Dominio a DTOs
        var mangaDtos = mangas.Select(m => new MangaDto
        {
            Id = m.Id,
            Title = m.Title,
            Author = m.Author,
            Price = m.Price,
            ImageUrl = m.ImageUrl
        });

        return Ok(mangaDtos);
    }
}