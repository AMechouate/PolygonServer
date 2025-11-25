using System.ComponentModel.DataAnnotations;

namespace PolygonApi.DTOs
{
    public class CreateArtikelvarianteDto
    {
        [Required]
        [MaxLength(20)]
        public string ArticleNo { get; set; } = "19500086";

        [MaxLength(10)]
        public string? ArtCode { get; set; } = "ANTHRAZIT";

        [MaxLength(100)]
        public string? ArtDesc { get; set; } = "Sopro Design Fuge Flex 1-10mm DF 10 5kg, anthrazit";
    }
}

