using System.ComponentModel.DataAnnotations;

namespace Magic_Villa.Models.Data
{
    public class VillaNoDto
    {
        [Required]
        public int VillaNo { get; set; }
        [Required]
        public int VillaId { get; set; }
        public string SpecialDetails { get; set; }
        public VillaDto Villa { get; set; }
    }
}
