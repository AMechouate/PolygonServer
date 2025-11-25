using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PolygonApi.Models
{
    [Table("Artikelreferenz")]
    public class Artikelreferenz
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string ArticleNo { get; set; } = string.Empty;

        [MaxLength(10)]
        public string? ArtVariantCode { get; set; }

        [MaxLength(10)]
        public string? Unit { get; set; }

        public int RefTyp { get; set; } = 2; // 0="", 1=Debitor, 2=Kreditor, 3=Barcode

        [MaxLength(20)]
        public string? RefTypNo { get; set; }

        [MaxLength(50)]
        public string? RefNo { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

