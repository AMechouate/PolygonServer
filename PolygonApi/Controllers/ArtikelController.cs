using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PolygonApi.Data;
using PolygonApi.DTOs;
using PolygonApi.Models;

namespace PolygonApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArtikelController : ControllerBase
    {
        private readonly PolygonDbContext _context;
        private readonly ILogger<ArtikelController> _logger;

        public ArtikelController(PolygonDbContext context, ILogger<ArtikelController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateArtikel([FromBody] CreateArtikelDto dto)
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

                var exists = await _context.Artikel
                    .AnyAsync(a => a.ArticleNo == dto.ArticleNo);

                if (exists)
                {
                    return BadRequest(new ErrorResponse
                    {
                        Status = "error",
                        Message = "Artikel mit dieser Nummer existiert bereits.",
                        Details = new { field = "articleNo", issue = "Nummer bereits vorhanden" }
                    });
                }

                var artikel = new Artikel
                {
                    ArticleNo = dto.ArticleNo,
                    Description = dto.Description,
                    BaseUnit = dto.BaseUnit,
                    Type = dto.Type,
                    CreditorNo = dto.CreditorNo,
                    CreditorArticleNo = dto.CreditorArticleNo,
                    Blocked = dto.Blocked,
                    BlockReason = dto.BlockReason,
                    PurchaseUnitCode = dto.PurchaseUnitCode,
                    ArtCatCode = dto.ArtCatCode,
                    CreditorDescr = dto.CreditorDescr,
                    CreditorStdVariant = dto.CreditorStdVariant,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Artikel.Add(artikel);
                await _context.SaveChangesAsync();

                return Ok(new ApiResponse
                {
                    Status = "success",
                    Message = "Daten erfolgreich gespeichert."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fehler beim Erstellen des Artikels");
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

