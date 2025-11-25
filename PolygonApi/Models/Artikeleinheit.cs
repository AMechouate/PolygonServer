using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PolygonApi.Models
{
    [Table("Artikeleinheit")]
    public class Artikeleinheit
    {
        [Key]
        [Column(Order = 0)]
        [MaxLength(20)]
        public string ArticleNo { get; set; } = string.Empty;

        [Key]
        [Column(Order = 1)]
        [MaxLength(10)]
        public string ArtCode { get; set; } = string.Empty;

        public decimal QuantityPerUnit { get; set; } = 1;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

