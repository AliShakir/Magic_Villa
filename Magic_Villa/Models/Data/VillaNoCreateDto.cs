using System.ComponentModel.DataAnnotations;

namespace Magic_Villa.Models.Data
{
    public class VillaNoCreateDto
    {
        [Required]
        public int VillaNo { get; set; }
        public int VillaId { get; set; }
        public string SpecialDetails { get; set; }
    }
}
