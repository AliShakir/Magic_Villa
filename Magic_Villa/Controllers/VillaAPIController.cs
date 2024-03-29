﻿using AutoMapper;
using Azure;
using Magic_Villa.Data;
using Magic_Villa.Models;
using Magic_Villa.Models.Data;
using Magic_Villa.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

namespace Magic_Villa.Controllers
{
    [Route("api/v{version:apiVersion}/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IVillaRepository _villaRepository;

        public IMapper _mapper { get; }

        public VillaAPIController(IVillaRepository villaRepository, IMapper mapper)
        {
            _villaRepository = villaRepository;
            _mapper = mapper;
            this._response = new();
        }
        [HttpGet]
        [ResponseCache(Duration = 30)]
        public async Task<ActionResult<APIResponse>> GetVillasAsync([FromQuery(Name ="FillterOccupancy")]int? occupancy,string? search,int pageSize = 0, int pageNumber = 1)
        {
            try
            {
                IEnumerable<Villa> villaList;
                if(occupancy > 0)
                {
                    villaList =  await _villaRepository.GetAllAsync(c=>c.Occupancy == occupancy, pageNumber: pageNumber, pageSize: pageSize);
                }
                else
                {
                    villaList = await _villaRepository.GetAllAsync(pageNumber:pageNumber,pageSize:pageSize);
                }
                if (!string.IsNullOrWhiteSpace(search))
                {
                    villaList = await _villaRepository.GetAllAsync(c => c.Name == search);
                }
                Pagination pagination = new() { PageNumber= pageNumber,PageSize = pageSize };
                Response.Headers.Add("X-Pagination",JsonSerializer.Serialize(pagination));
                _response.Result = _mapper.Map<List<VillaDto>>(villaList);
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
        [HttpGet("id", Name = "GetVilla")]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetVillas(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return BadRequest(_response);
                }
                var villa = await _villaRepository.GetAsync(c => c.Id == id);
                if (villa == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<VillaDto>(villa);
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
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> CreateVilla([FromBody] VillaCreateDto villaDto)
        {
            try
            {
                if (await _villaRepository.GetAsync(c => c.Name.ToLower() == villaDto.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "villa already exists");
                    return BadRequest(ModelState);
                }
                if (villaDto == null)
                {
                    return BadRequest();
                }
                Villa villa = _mapper.Map<Villa>(villaDto);
                await _villaRepository.CreateAsync(villa);
                _response.Result = _mapper.Map<VillaDto>(villa);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetVilla", new { id = villa.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpDelete("id:int", Name = "DeleteVilla")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> DeleteVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var villa = await _villaRepository.GetAsync(c => c.Id == id);
                if (villa == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _villaRepository.RemoveAsync(villa);
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
        [HttpPut("id:int", Name = "UpdateVilla")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> UpdateVilla(int id, [FromBody] VillaUpdateDto villaDto)
        {
            try
            {
                if (villaDto == null || id != villaDto.Id)
                {
                    return BadRequest();
                }
                var model = _mapper.Map<Villa>(villaDto);

                await _villaRepository.UpdateAsync(model);

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
