using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PolygonApi.Data;
using PolygonApi.DTOs;
using PolygonApi.Models;

namespace PolygonApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ZustaendigkeitseinheitencodController : ControllerBase
    {
        private readonly PolygonDbContext _context;
        private readonly ILogger<ZustaendigkeitseinheitencodController> _logger;

        public ZustaendigkeitseinheitencodController(PolygonDbContext context, ILogger<ZustaendigkeitseinheitencodController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateZustaendigkeitseinheitencod([FromBody] CreateZustaendigkeitseinheitencodDto dto)
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

                var exists = await _context.Zustaendigkeitseinheitencodes
                    .AnyAsync(z => z.RespCode == dto.RespCode);

                if (exists)
                {
                    return BadRequest(new ErrorResponse
                    {
                        Status = "error",
                        Message = "Zuständigkeitseinheitencode existiert bereits.",
                        Details = new { field = "respCode", issue = "Code bereits vorhanden" }
                    });
                }

                var zustaendigkeitseinheitencod = new Zustaendigkeitseinheitencod
                {
                    RespCode = dto.RespCode,
                    Name = dto.Name,
                    Address = dto.Address,
                    Address2 = dto.Address2,
                    City = dto.City,
                    PostalCode = dto.PostalCode,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Zustaendigkeitseinheitencodes.Add(zustaendigkeitseinheitencod);
                await _context.SaveChangesAsync();

                return Ok(new ApiResponse
                {
                    Status = "success",
                    Message = "Daten erfolgreich gespeichert."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fehler beim Erstellen des Zuständigkeitseinheitencodes");
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

