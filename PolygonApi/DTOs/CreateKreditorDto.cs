using System.ComponentModel.DataAnnotations;

namespace PolygonApi.DTOs
{
    public class CreateKreditorDto
    {
        [Required]
        [MaxLength(20)]
        public string CreditorNo { get; set; } = "712183";

        [MaxLength(100)]
        public string? Name { get; set; } = "WaWa AG Baustoffe";

        [MaxLength(50)]
        public string? Name2 { get; set; } = "";

        [MaxLength(100)]
        public string? Address { get; set; } = "Grundstr. 2";

        [MaxLength(50)]
        public string? Address2 { get; set; } = "";

        [MaxLength(30)]
        public string? City { get; set; } = "Wattenscheid";

        [MaxLength(20)]
        public string? PostalCode { get; set; } = "63868";
    }
}

