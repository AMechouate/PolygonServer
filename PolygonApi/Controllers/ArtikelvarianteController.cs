using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PolygonApi.Data;
using PolygonApi.DTOs;
using PolygonApi.Models;

namespace PolygonApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArtikelvarianteController : ControllerBase
    {
        private readonly PolygonDbContext _context;
        private readonly ILogger<ArtikelvarianteController> _logger;

        public ArtikelvarianteController(PolygonDbContext context, ILogger<ArtikelvarianteController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateArtikelvariante([FromBody] CreateArtikelvarianteDto dto)
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

                var exists = await _context.Artikelvarianten
                    .AnyAsync(a => a.ArticleNo == dto.ArticleNo && a.ArtCode == dto.ArtCode);

                if (exists)
                {
                    return BadRequest(new ErrorResponse
                    {
                        Status = "error",
                        Message = "Artikelvariante existiert bereits.",
                        Details = new { field = "articleNo, artCode", issue = "Kombination bereits vorhanden" }
                    });
                }

                var artikelvariante = new Artikelvariante
                {
                    ArticleNo = dto.ArticleNo,
                    ArtCode = dto.ArtCode ?? string.Empty,
                    ArtDesc = dto.ArtDesc,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Artikelvarianten.Add(artikelvariante);
                await _context.SaveChangesAsync();

                return Ok(new ApiResponse
                {
                    Status = "success",
                    Message = "Daten erfolgreich gespeichert."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fehler beim Erstellen der Artikelvariante");
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

