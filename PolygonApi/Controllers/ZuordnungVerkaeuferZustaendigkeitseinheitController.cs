using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PolygonApi.Data;
using PolygonApi.DTOs;
using PolygonApi.Models;

namespace PolygonApi.Controllers
{
    [ApiController]
    [Route("zuordnung-verkaeufer-zustaendigkeitseinheit")]
    public class ZuordnungVerkaeuferZustaendigkeitseinheitController : ControllerBase
    {
        private readonly PolygonDbContext _context;
        private readonly ILogger<ZuordnungVerkaeuferZustaendigkeitseinheitController> _logger;

        public ZuordnungVerkaeuferZustaendigkeitseinheitController(PolygonDbContext context, ILogger<ZuordnungVerkaeuferZustaendigkeitseinheitController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateZuordnung([FromBody] CreateZuordnungVerkaeuferZustaendigkeitseinheitDto dto)
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

                var exists = await _context.ZuordnungVerkaeuferZustaendigkeitseinheiten
                    .AnyAsync(z => z.SellerBuyerId == dto.SellerBuyerId && z.RespUnitCode == dto.RespUnitCode);

                if (exists)
                {
                    return BadRequest(new ErrorResponse
                    {
                        Status = "error",
                        Message = "Zuordnung existiert bereits.",
                        Details = new { field = "sellerBuyerId, respUnitCode", issue = "Kombination bereits vorhanden" }
                    });
                }

                var zuordnung = new ZuordnungVerkaeuferZustaendigkeitseinheit
                {
                    SellerBuyerId = dto.SellerBuyerId,
                    RespUnitCode = dto.RespUnitCode ?? string.Empty,
                    CreatedAt = DateTime.UtcNow
                };

                _context.ZuordnungVerkaeuferZustaendigkeitseinheiten.Add(zuordnung);
                await _context.SaveChangesAsync();

                return Ok(new ApiResponse
                {
                    Status = "success",
                    Message = "Daten erfolgreich gespeichert."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fehler beim Erstellen der Zuordnung");
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

