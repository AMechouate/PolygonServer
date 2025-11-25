using System.ComponentModel.DataAnnotations;

namespace PolygonApi.DTOs
{
    public class CreateArtikelreferenzDto
    {
        [Required]
        [MaxLength(20)]
        public string ArticleNo { get; set; } = "19500086";

        [MaxLength(10)]
        public string? ArtVariantCode { get; set; } = "ANTHRAZIT";

        [MaxLength(10)]
        public string? Unit { get; set; } = "EIMER";

        [Range(0, 3)]
        public int RefTyp { get; set; } = 2; // 0="", 1=Debitor, 2=Kreditor, 3=Barcode

        [MaxLength(20)]
        public string? RefTypNo { get; set; } = "712183";

        [MaxLength(50)]
        public string? RefNo { get; set; } = "1338670";
    }
}

