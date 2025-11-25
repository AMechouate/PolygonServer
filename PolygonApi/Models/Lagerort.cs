using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PolygonApi.Models
{
    [Table("Lagerort")]
    public class Lagerort
    {
        [Key]
        [MaxLength(10)]
        public string StorageLocCode { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Name { get; set; }

        [MaxLength(50)]
        public string? Name2 { get; set; }

        [MaxLength(100)]
        public string? Address { get; set; }

        [MaxLength(50)]
        public string? Address2 { get; set; }

        [MaxLength(30)]
        public string? City { get; set; }

        [MaxLength(20)]
        public string? PostalCode { get; set; }

        [MaxLength(10)]
        public string? RespUnitCode { get; set; }

        [MaxLength(50)]
        public string? UserId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

