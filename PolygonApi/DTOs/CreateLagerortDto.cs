using System.ComponentModel.DataAnnotations;

namespace PolygonApi.DTOs
{
    public class CreateLagerortDto
    {
        [Required]
        [MaxLength(10)]
        public string StorageLocCode { get; set; } = "LAGER AB L";

        [MaxLength(100)]
        public string? Name { get; set; } = "POLYGON Deutschland GmbH";

        [MaxLength(50)]
        public string? Name2 { get; set; } = "";

        [MaxLength(100)]
        public string? Address { get; set; } = "Duckstraße 2";

        [MaxLength(50)]
        public string? Address2 { get; set; } = "Aschaffenburg";

        [MaxLength(30)]
        public string? City { get; set; } = "Kleinostheim";

        [MaxLength(20)]
        public string? PostalCode { get; set; } = "63801";

        [MaxLength(10)]
        public string? RespUnitCode { get; set; } = "AB";

        [MaxLength(50)]
        public string? UserId { get; set; } = "VATROMMUSTER";
    }
}

