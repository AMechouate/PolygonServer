using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PolygonApi.Data;
using PolygonApi.DTOs;
using PolygonApi.Models;

namespace PolygonApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjektController : ControllerBase
    {
        private readonly PolygonDbContext _context;
        private readonly ILogger<ProjektController> _logger;

        public ProjektController(PolygonDbContext context, ILogger<ProjektController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateProjekt([FromBody] CreateProjektDto dto)
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

                var exists = await _context.Projekte
                    .AnyAsync(p => p.ProjectNo == dto.ProjectNo);

                if (exists)
                {
                    return BadRequest(new ErrorResponse
                    {
                        Status = "error",
                        Message = "Projekt mit dieser Nummer existiert bereits.",
                        Details = new { field = "projectNo", issue = "Nummer bereits vorhanden" }
                    });
                }

                var projekt = new Projekt
                {
                    ProjectNo = dto.ProjectNo,
                    ProjectDesc = dto.ProjectDesc,
                    DeliveryToName = dto.DeliveryToName,
                    DeliveryAddress = dto.DeliveryAddress,
                    DeliveryToCity = dto.DeliveryToCity,
                    DeliveryToPostalCode = dto.DeliveryToPostalCode,
                    DamageAcceptaceNo = dto.DamageAcceptaceNo,
                    RespUnitCode = dto.RespUnitCode,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Projekte.Add(projekt);
                await _context.SaveChangesAsync();

                return Ok(new ApiResponse
                {
                    Status = "success",
                    Message = "Daten erfolgreich gespeichert."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fehler beim Erstellen des Projekts");
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

