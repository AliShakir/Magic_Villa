using AutoMapper;
using Azure;
using Magic_Villa.Data;
using Magic_Villa.Models;
using Magic_Villa.Models.Data;
using Magic_Villa.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Magic_Villa.Controllers
{
    [Route("api/VillaNumberAPI")]
    [ApiController]
    public class VillaNumberAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IVillaNumberRepository _villaNumberRepository;
        private readonly IVillaRepository _dbVilla;
        public IMapper _mapper { get; }

        public VillaNumberAPIController(IVillaNumberRepository villaNumberRepository, IMapper mapper, IVillaRepository dbVilla)
        {
            _villaNumberRepository = villaNumberRepository;
            _mapper = mapper;
            this._response = new();
            _dbVilla = dbVilla;
        }
        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetVillasNumberAsync()
        {
            try
            {
                IEnumerable<VillaNumber> villaList = await _villaNumberRepository.GetAllAsync(includeProperties:"Villa");
                _response.Result = _mapper.Map<List<VillaNoDto>>(villaList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpGet("id", Name = "GetVillaNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetVillaNumber(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return BadRequest(_response);
                }
                var villaNo = await _villaNumberRepository.GetAsync(c => c.VillaNo == id);
                if (villaNo == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<VillaNoDto>(villaNo);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> CreateVillaNumber([FromBody] VillaNoCreateDto villaDto)
        {
            try
            {
                if (await _villaNumberRepository.GetAsync(c => c.VillaNo == villaDto.VillaNo) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "villa no already exists");
                    return BadRequest(ModelState);
                }
                if(await _dbVilla.GetAsync(p =>p.Id == villaDto.VillaId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Villa Id is Invalid");
                    return BadRequest(ModelState);
                }
                if (villaDto == null)
                {
                    return BadRequest();
                }
                VillaNumber villa = _mapper.Map<VillaNumber>(villaDto);
                await _villaNumberRepository.CreateAsync(villa);
                _response.Result = _mapper.Map<VillaNumber>(villaDto);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetVillaNumber", new { id = villa.VillaNo }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpDelete("id:int", Name = "DeleteVillaNumber")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> DeleteVillaNumber(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var villa = await _villaNumberRepository.GetAsync(c => c.VillaNo == id);
                if (villa == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _villaNumberRepository.RemoveAsync(villa);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpPut("id:int", Name = "UpdateVillaNumber")]
        public async Task<ActionResult<APIResponse>> UpdateVillaNumber(int id, [FromBody] VillaNoUpdateDto villaDto)
        {
            try
            {
                if (villaDto == null || id != villaDto.VillaNo)
                {
                    return BadRequest();
                }
                if (await _dbVilla.GetAsync(p => p.Id == villaDto.VillaId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Villa Id is Invalid");
                    return BadRequest(ModelState);
                }
                var model = _mapper.Map<VillaNumber>(villaDto);

                await _villaNumberRepository.UpdateAsync(model);

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}
