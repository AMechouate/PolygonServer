using System.ComponentModel.DataAnnotations;

namespace PolygonApi.DTOs
{
    public class CreateZuordnungVerkaeuferZustaendigkeitseinheitDto
    {
        [Required]
        [MaxLength(20)]
        public string SellerBuyerId { get; set; } = "MMUSTER";

        [MaxLength(10)]
        public string? RespUnitCode { get; set; } = "KR";
    }
}

