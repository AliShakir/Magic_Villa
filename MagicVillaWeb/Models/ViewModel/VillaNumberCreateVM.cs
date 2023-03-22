using MagicVillaWeb.Models.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicVillaWeb.Models.ViewModel
{
    public class VillaNumberCreateVM
    {
        public VillaNumberCreateVM()
        {
            VillaNumber = new VillaNoCreateDto();
        }
        public VillaNoCreateDto VillaNumber { get; set; }
        
        [ValidateNever]
        public IEnumerable<SelectListItem>  VillaList { get; set; }
    }
}
