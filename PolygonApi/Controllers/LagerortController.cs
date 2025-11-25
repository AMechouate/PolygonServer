using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PolygonApi.Data;
using PolygonApi.DTOs;
using PolygonApi.Models;
using System.ComponentModel.DataAnnotations;

namespace PolygonApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LagerortController : ControllerBase
    {
        private readonly PolygonDbContext _context;
        private readonly ILogger<LagerortController> _logger;

        public LagerortController(PolygonDbContext context, ILogger<LagerortController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateLagerort([FromBody] CreateLagerortDto dto)
        {
            try
            {
                // Validation
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ErrorResponse
                    {
                        Status = "error",
                        Message = "Validierungsfehler",
                        Details = ModelState
                    });
                }

                // Check if already exists
                var exists = await _context.Lagerorte
                    .AnyAsync(l => l.StorageLocCode == dto.StorageLocCode);

                if (exists)
                {
                    return BadRequest(new ErrorResponse
                    {
                        Status = "error",
                        Message = "Lagerort mit diesem Code existiert bereits.",
                        Details = new { field = "storageLocCode", issue = "Code bereits vorhanden" }
                    });
                }

                // Create entity
                var lagerort = new Lagerort
                {
                    StorageLocCode = dto.StorageLocCode,
                    Name = dto.Name,
                    Name2 = dto.Name2,
                    Address = dto.Address,
                    Address2 = dto.Address2,
                    City = dto.City,
                    PostalCode = dto.PostalCode,
                    RespUnitCode = dto.RespUnitCode,
                    UserId = dto.UserId,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Lagerorte.Add(lagerort);
                await _context.SaveChangesAsync();

                return Ok(new ApiResponse
                {
                    Status = "success",
                    Message = "Daten erfolgreich gespeichert."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fehler beim Erstellen des Lagerorts");
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

