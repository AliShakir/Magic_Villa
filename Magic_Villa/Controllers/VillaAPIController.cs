using Magic_Villa.Data;
using Magic_Villa.Models;
using Magic_Villa.Models.Data;
using Microsoft.AspNetCore.Mvc;

namespace Magic_Villa.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public VillaAPIController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public ActionResult<IEnumerable<VillaDto>> GetVillas()
        {
            return Ok(_db.Villas.ToList());
        }
        [HttpGet("id", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDto> GetVillas(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = _db.Villas.FirstOrDefault(c => c.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            return Ok(villa);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDto> CreateVilla(VillaDto villa)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest();
            //}
            if(_db.Villas.FirstOrDefault(c=>c.Name.ToLower() == villa.Name.ToLower()) != null)
            {
                ModelState.AddModelError("", "villa already exists");
                return BadRequest(ModelState);
            }
            if (villa == null) { 
            return BadRequest();
            }
            if(villa.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            Villa model = new()
            {
                 Amenity = villa.Amenity,
                 Name = villa.Name,
                 Details = villa.Details,
                 ImageUrl = villa.ImageUrl,
                 Occupancy = villa.Occupancy,
                 Rate = villa.Rate,
                 Sqft = villa.Sqft,
                 CreatedDate = DateTime.Now,
            };
            _db.Villas.Add(model);
            _db.SaveChanges();
            return CreatedAtRoute("GetVilla", new {id=villa.Id},villa);
        }
        [HttpDelete("id:int", Name ="DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteVilla(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var villa = _db.Villas.FirstOrDefault(c=>c.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            _db.Villas.Remove(villa);
            _db.SaveChanges();
            return NoContent();
        }
        [HttpPut("id:int", Name = "UpdateVilla")]
        public IActionResult UpdateVilla(int id, [FromBody]VillaDto villaDto)
        {
            if (villaDto == null || id != villaDto.Id)
            {
                return BadRequest();
            }
            Villa model = new()
            {
                Amenity = villaDto.Amenity,
                Name = villaDto.Name,
                Details = villaDto.Details,
                ImageUrl = villaDto.ImageUrl,
                Occupancy = villaDto.Occupancy,
                Rate = villaDto.Rate,
                Sqft = villaDto.Sqft,
                UpdatedDate = DateTime.Now
            };
            _db.Villas.Update(model);
            _db.SaveChanges();

            return NoContent();
        }
    }
}
