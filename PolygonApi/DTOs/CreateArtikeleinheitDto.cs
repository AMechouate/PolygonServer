using System.ComponentModel.DataAnnotations;

namespace PolygonApi.DTOs
{
    public class CreateArtikeleinheitDto
    {
        [Required]
        [MaxLength(20)]
        public string ArticleNo { get; set; } = "19500086";

        [MaxLength(10)]
        public string? ArtCode { get; set; } = "EIMER";

        public decimal QuantityPerUnit { get; set; } = 1;
    }
}

