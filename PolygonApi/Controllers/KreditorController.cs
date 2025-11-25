using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PolygonApi.Data;
using PolygonApi.DTOs;
using PolygonApi.Models;

namespace PolygonApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KreditorController : ControllerBase
    {
        private readonly PolygonDbContext _context;
        private readonly ILogger<KreditorController> _logger;

        public KreditorController(PolygonDbContext context, ILogger<KreditorController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateKreditor([FromBody] CreateKreditorDto dto)
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

                var exists = await _context.Kreditoren
                    .AnyAsync(k => k.CreditorNo == dto.CreditorNo);

                if (exists)
                {
                    return BadRequest(new ErrorResponse
                    {
                        Status = "error",
                        Message = "Kreditor mit dieser Nummer existiert bereits.",
                        Details = new { field = "creditorNo", issue = "Nummer bereits vorhanden" }
                    });
                }

                var kreditor = new Kreditor
                {
                    CreditorNo = dto.CreditorNo,
                    Name = dto.Name,
                    Name2 = dto.Name2,
                    Address = dto.Address,
                    Address2 = dto.Address2,
                    City = dto.City,
                    PostalCode = dto.PostalCode,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Kreditoren.Add(kreditor);
                await _context.SaveChangesAsync();

                return Ok(new ApiResponse
                {
                    Status = "success",
                    Message = "Daten erfolgreich gespeichert."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fehler beim Erstellen des Kreditors");
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

