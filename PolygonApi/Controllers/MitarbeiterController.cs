using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PolygonApi.Data;
using PolygonApi.DTOs;
using PolygonApi.Models;

namespace PolygonApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MitarbeiterController : ControllerBase
    {
        private readonly PolygonDbContext _context;
        private readonly ILogger<MitarbeiterController> _logger;

        public MitarbeiterController(PolygonDbContext context, ILogger<MitarbeiterController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateMitarbeiter([FromBody] CreateMitarbeiterDto dto)
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

                var exists = await _context.Mitarbeiter
                    .AnyAsync(m => m.EmployeeNo == dto.EmployeeNo);

                if (exists)
                {
                    return BadRequest(new ErrorResponse
                    {
                        Status = "error",
                        Message = "Mitarbeiter mit dieser Nummer existiert bereits.",
                        Details = new { field = "employeeNo", issue = "Nummer bereits vorhanden" }
                    });
                }

                var mitarbeiter = new Mitarbeiter
                {
                    EmployeeNo = dto.EmployeeNo,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    SellerBuyerCode = dto.SellerBuyerCode,
                    UserId = dto.UserId,
                    RespUnit = dto.RespUnit,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Mitarbeiter.Add(mitarbeiter);
                await _context.SaveChangesAsync();

                return Ok(new ApiResponse
                {
                    Status = "success",
                    Message = "Daten erfolgreich gespeichert."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fehler beim Erstellen des Mitarbeiters");
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

