using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PolygonApi.Models
{
    [Table("Mitarbeiter")]
    public class Mitarbeiter
    {
        [Key]
        [MaxLength(20)]
        public string EmployeeNo { get; set; } = string.Empty;

        [MaxLength(30)]
        public string? FirstName { get; set; }

        [MaxLength(30)]
        public string? LastName { get; set; }

        [MaxLength(20)]
        public string? SellerBuyerCode { get; set; }

        [MaxLength(50)]
        public string? UserId { get; set; }

        [MaxLength(10)]
        public string? RespUnit { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

