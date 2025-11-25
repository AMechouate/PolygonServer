using System.ComponentModel.DataAnnotations;

namespace PolygonApi.DTOs
{
    public class CreateProjektDto
    {
        [Required]
        [MaxLength(20)]
        public string ProjectNo { get; set; } = "AB-MA-25002959";

        [MaxLength(100)]
        public string? ProjectDesc { get; set; } = "GDV, Wasserschaden Erlenbach, Stolbinger";

        [MaxLength(100)]
        public string? DeliveryToName { get; set; } = "Berta Kramer";

        [MaxLength(100)]
        public string? DeliveryAddress { get; set; } = "Am Stadtpark 8";

        [MaxLength(30)]
        public string? DeliveryToCity { get; set; } = "Erlenbach a. Main";

        [MaxLength(20)]
        public string? DeliveryToPostalCode { get; set; } = "63906";

        [MaxLength(20)]
        public string? DamageAcceptaceNo { get; set; } = "1440271";

        [MaxLength(10)]
        public string? RespUnitCode { get; set; } = "AB";
    }
}

