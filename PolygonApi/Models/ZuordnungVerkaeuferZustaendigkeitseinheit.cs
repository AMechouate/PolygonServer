using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PolygonApi.Models
{
    [Table("ZuordnungVerkaeuferZustaendigkeitseinheit")]
    public class ZuordnungVerkaeuferZustaendigkeitseinheit
    {
        [Key]
        [Column(Order = 0)]
        [MaxLength(20)]
        public string SellerBuyerId { get; set; } = string.Empty;

        [Key]
        [Column(Order = 1)]
        [MaxLength(10)]
        public string RespUnitCode { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

