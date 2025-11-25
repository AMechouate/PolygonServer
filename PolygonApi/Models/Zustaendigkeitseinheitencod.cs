using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PolygonApi.Models
{
    [Table("Zustaendigkeitseinheitencod")]
    public class Zustaendigkeitseinheitencod
    {
        [Key]
        [MaxLength(10)]
        public string RespCode { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Name { get; set; }

        [MaxLength(100)]
        public string? Address { get; set; }

        [MaxLength(50)]
        public string? Address2 { get; set; }

        [MaxLength(30)]
        public string? City { get; set; }

        [MaxLength(20)]
        public string? PostalCode { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

