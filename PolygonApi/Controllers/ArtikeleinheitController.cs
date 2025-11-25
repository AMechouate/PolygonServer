using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PolygonApi.Data;
using PolygonApi.DTOs;
using PolygonApi.Models;

namespace PolygonApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArtikeleinheitController : ControllerBase
    {
        private readonly PolygonDbContext _context;
        private readonly ILogger<ArtikeleinheitController> _logger;

        public ArtikeleinheitController(PolygonDbContext context, ILogger<ArtikeleinheitController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateArtikeleinheit([FromBody] CreateArtikeleinheitDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ErrorResponse
                    {
                        Status = "error",
                        Message = "Validierungsfehler",
                        Details = ModelState
                    });
                }

                var exists = await _context.Artikeleinheiten
                    .AnyAsync(a => a.ArticleNo == dto.ArticleNo && a.ArtCode == dto.ArtCode);

                if (exists)
                {
                    return BadRequest(new ErrorResponse
                    {
                        Status = "error",
                        Message = "Artikeleinheit existiert bereits.",
                        Details = new { field = "articleNo, artCode", issue = "Kombination bereits vorhanden" }
                    });
                }

                var artikeleinheit = new Artikeleinheit
                {
                    ArticleNo = dto.ArticleNo,
                    ArtCode = dto.ArtCode ?? string.Empty,
                    QuantityPerUnit = dto.QuantityPerUnit,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Artikeleinheiten.Add(artikeleinheit);
                await _context.SaveChangesAsync();

                return Ok(new ApiResponse
                {
                    Status = "success",
                    Message = "Daten erfolgreich gespeichert."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fehler beim Erstellen der Artikeleinheit");
                return StatusCode(500, new ErrorResponse
                {
                    Status = "error",
                    Message = "Interner Serverfehler",
                    Details = new { error = ex.Message }
                });
            }
        }
    }
}

