using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PolygonApi.Models
{
    [Table("Projekt")]
    public class Projekt
    {
        [Key]
        [MaxLength(20)]
        public string ProjectNo { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? ProjectDesc { get; set; }

        [MaxLength(100)]
        public string? DeliveryToName { get; set; }

        [MaxLength(100)]
        public string? DeliveryAddress { get; set; }

        [MaxLength(30)]
        public string? DeliveryToCity { get; set; }

        [MaxLength(20)]
        public string? DeliveryToPostalCode { get; set; }

        [MaxLength(20)]
        public string? DamageAcceptaceNo { get; set; }

        [MaxLength(10)]
        public string? RespUnitCode { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

