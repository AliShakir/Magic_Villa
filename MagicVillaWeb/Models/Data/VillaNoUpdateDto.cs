using System.ComponentModel.DataAnnotations;

namespace MagicVillaWeb.Models.Data
{
    public class VillaNoUpdateDto
    {
        [Required]
        public int VillaNo { get; set; }
        public int VillaId { get; set; }
        public string SpecialDetails { get; set; }
    }
}
