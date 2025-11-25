using System.ComponentModel.DataAnnotations;

namespace PolygonApi.DTOs
{
    public class CreateMitarbeiterDto
    {
        [Required]
        [MaxLength(20)]
        public string EmployeeNo { get; set; } = "1324";

        [MaxLength(30)]
        public string? FirstName { get; set; } = "Michael";

        [MaxLength(30)]
        public string? LastName { get; set; } = "Muster";

        [MaxLength(20)]
        public string? SellerBuyerCode { get; set; } = "MMUSTER";

        [MaxLength(50)]
        public string? UserId { get; set; } = "VATROMMUSTER";

        [MaxLength(10)]
        public string? RespUnit { get; set; } = "";
    }
}

