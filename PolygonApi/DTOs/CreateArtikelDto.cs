using System.ComponentModel.DataAnnotations;

namespace PolygonApi.DTOs
{
    public class CreateArtikelDto
    {
        [Required]
        [MaxLength(20)]
        public string ArticleNo { get; set; } = "19500086";

        [MaxLength(100)]
        public string? Description { get; set; } = "Sopro Design Fuge Flex, 1-10mm, DF 10, 5kg";

        [MaxLength(10)]
        public string? BaseUnit { get; set; } = "EIMER";

        [Range(0, 3)]
        public int Type { get; set; } = 0; // 0=Bestand, 1=Service, 2=kein Bestand

        [MaxLength(20)]
        public string? CreditorNo { get; set; } = "712183";

        [MaxLength(50)]
        public string? CreditorArticleNo { get; set; } = "1338666 - 1481770";

        public bool Blocked { get; set; } = false;

        [MaxLength(250)]
        public string? BlockReason { get; set; } = "";

        [MaxLength(10)]
        public string? PurchaseUnitCode { get; set; } = "EIMER";

        [MaxLength(20)]
        public string? ArtCatCode { get; set; } = "VERBRAUCHSMATERIAL";

        [MaxLength(100)]
        public string? CreditorDescr { get; set; } = "Sopro Design Fuge Flex 1-10mm DF 10 5kg";

        [MaxLength(20)]
        public string? CreditorStdVariant { get; set; } = "";
    }
}

