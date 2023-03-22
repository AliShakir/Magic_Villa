using System.ComponentModel.DataAnnotations;

namespace Magic_Villa.Models.Data
{
    public class VillaNoUpdateDto
    {
        [Required]
        public int VillaNo { get; set; }
        public int VillaId { get; set; }
        public string SpecialDetails { get; set; }
    }
}
