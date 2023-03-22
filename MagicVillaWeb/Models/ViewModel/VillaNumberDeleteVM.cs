using MagicVillaWeb.Models.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicVillaWeb.Models.ViewModel
{
    public class VillaNumberDeleteVM
    {
        public VillaNumberDeleteVM()
        {
            VillaNumber = new VillaNoDto();
        }
        public VillaNoDto VillaNumber { get; set; }
        
        [ValidateNever]
        public IEnumerable<SelectListItem>  VillaList { get; set; }
    }
}
