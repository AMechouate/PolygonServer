using Microsoft.AspNetCore.Mvc;
using PolygonApi.Data;
using PolygonApi.DTOs;
using PolygonApi.Models;

namespace PolygonApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArtikelreferenzController : ControllerBase
    {
        private readonly PolygonDbContext _context;
        private readonly ILogger<ArtikelreferenzController> _logger;

        public ArtikelreferenzController(PolygonDbContext context, ILogger<ArtikelreferenzController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateArtikelreferenz([FromBody] CreateArtikelreferenzDto dto)
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

                var artikelreferenz = new Artikelreferenz
                {
                    ArticleNo = dto.ArticleNo,
                    ArtVariantCode = dto.ArtVariantCode,
                    Unit = dto.Unit,
                    RefTyp = dto.RefTyp,
                    RefTypNo = dto.RefTypNo,
                    RefNo = dto.RefNo,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Artikelreferenzen.Add(artikelreferenz);
                await _context.SaveChangesAsync();

                return Ok(new ApiResponse
                {
                    Status = "success",
                    Message = "Daten erfolgreich gespeichert."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fehler beim Erstellen der Artikelreferenz");
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

