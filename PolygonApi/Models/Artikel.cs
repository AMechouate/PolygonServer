using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PolygonApi.Models
{
    [Table("Artikel")]
    public class Artikel
    {
        [Key]
        [MaxLength(20)]
        public string ArticleNo { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Description { get; set; }

        [MaxLength(10)]
        public string? BaseUnit { get; set; }

        public int Type { get; set; } = 0; // 0=Bestand, 1=Service, 2=kein Bestand

        [MaxLength(20)]
        public string? CreditorNo { get; set; }

        [MaxLength(50)]
        public string? CreditorArticleNo { get; set; }

        public bool Blocked { get; set; } = false;

        [MaxLength(250)]
        public string? BlockReason { get; set; }

        [MaxLength(10)]
        public string? PurchaseUnitCode { get; set; }

        [MaxLength(20)]
        public string? ArtCatCode { get; set; }

        [MaxLength(100)]
        public string? CreditorDescr { get; set; }

        [MaxLength(20)]
        public string? CreditorStdVariant { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

