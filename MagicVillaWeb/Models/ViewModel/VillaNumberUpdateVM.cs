using MagicVillaWeb.Models.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicVillaWeb.Models.ViewModel
{
    public class VillaNumberUpdateVM
    {
        public VillaNumberUpdateVM()
        {
            VillaNumber = new VillaNoUpdateDto();
        }
        public VillaNoUpdateDto VillaNumber { get; set; }
        
        [ValidateNever]
        public IEnumerable<SelectListItem>  VillaList { get; set; }
    }
}
