using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PolygonApi.Models
{
    [Table("Artikelvariante")]
    public class Artikelvariante
    {
        [Key]
        [Column(Order = 0)]
        [MaxLength(20)]
        public string ArticleNo { get; set; } = string.Empty;

        [Key]
        [Column(Order = 1)]
        [MaxLength(10)]
        public string ArtCode { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? ArtDesc { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

