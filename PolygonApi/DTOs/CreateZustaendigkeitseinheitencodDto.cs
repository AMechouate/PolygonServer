using System.ComponentModel.DataAnnotations;

namespace PolygonApi.DTOs
{
    public class CreateZustaendigkeitseinheitencodDto
    {
        [Required]
        [MaxLength(10)]
        public string RespCode { get; set; } = "AB";

        [MaxLength(100)]
        public string? Name { get; set; } = "Kleinostheim";

        [MaxLength(100)]
        public string? Address { get; set; } = "Duckstraße 2";

        [MaxLength(50)]
        public string? Address2 { get; set; } = "Aschaffenburg";

        [MaxLength(30)]
        public string? City { get; set; } = "Kleinostheim";

        [MaxLength(20)]
        public string? PostalCode { get; set; } = "63801";
    }
}

