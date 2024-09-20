using AutoMapper;
using MagicVilla_villaAPI.Data;
using MagicVilla_villaAPI.Models;
using MagicVilla_villaAPI.Models.DTO;
using MagicVilla_villaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;

namespace MagicVilla_villaAPI.Controllers
{

    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        protected APIResponse response;
        private readonly IVillaRepository villaRepository;
        private readonly IMapper mapper;

        public VillaAPIController(IVillaRepository villaRepository, IMapper mapper)
        {
            this.villaRepository = villaRepository;
            this.mapper = mapper;
            this.response = new();
        }




        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetVillas()
        {
            try
            {
                IEnumerable<Villa> villaList = await villaRepository.GetAllAsync();
                response.Result = mapper.Map<List<VillaDto>>(villaList);
                response.StatusCode = HttpStatusCode.OK;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string>() { ex.ToString() };
            };
            //var villas = await  context.Villas.ToListAsync();
            //return Ok(villas);
            return response;
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(400)]
        public async Task<ActionResult<APIResponse>> GetVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(response);
                }
                var villa = await villaRepository.GetAsync(v => v.Id == id);
                if (villa == null)
                {
                    response.StatusCode = HttpStatusCode.NotFound;    
                    return NotFound(response);
                }
                response.Result = mapper.Map<VillaDto>(villa);
                response.StatusCode = HttpStatusCode.OK;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string>() { ex.ToString() };
            };
            return response;


        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateVilla([FromBody] VillaCreateDto villaCreateDto)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            try
            {
                if (await villaRepository.GetAsync(v => v.Name.ToLower() == villaCreateDto.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("CustomError", "Villa already exists");
                    return BadRequest(ModelState);
                }

                if (villaCreateDto == null)
                {
                    return BadRequest(villaCreateDto);
                }
                // if (villaDto.Id > 0)
                //{
                //    return StatusCode(StatusCodes.Status500InternalServerError);
                //}

                Villa villa = mapper.Map<Villa>(villaCreateDto);

                //Villa villa = new ()
                //{
                //    Name = villaCreateDto.Name,
                //    Amenity = villaCreateDto.Amenity,
                //    Details = villaCreateDto.Details,
                //    ImageUrl = villaCreateDto.ImageUrl,
                //    Occupancy = villaCreateDto.Occupancy,
                //    Rate = villaCreateDto.Rate,
                //    Sqft = villaCreateDto.Sqft
                //};
                await villaRepository.CreateAsync(villa);
                response.Result = mapper.Map<VillaDto>(villa);
                response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetVilla", new { id = villa.Id }, response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string>() { ex.ToString() };
            };
            return response;
        }

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> DeleteVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var villa = await villaRepository.GetAsync(x => x.Id == id);
                if (villa == null)
                {
                    return NotFound();
                }
                await villaRepository.RemoveAsync(villa);
                response.StatusCode = HttpStatusCode.NoContent;
                response.IsSuccess = true;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string>() { ex.ToString() };
            };
            return response;
        }


        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateVilla(int id, [FromBody] VillaUpdateDto villaUpdateDto)
        {
            try
            {
                if (villaUpdateDto == null || id != villaUpdateDto.Id)
                {
                    return BadRequest();
                }
                //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
                //villa.Name = villaDto.Name;
                //villa.Sqft = villaDto.Sqft;
                //villa.Occupancy = villaDto.Occupancy;


                Villa villa = mapper.Map<Villa>(villaUpdateDto);



                //Villa villa = new()
                //{
                //    Id = villaUpdateDto.Id,
                //    Name = villaUpdateDto.Name,
                //    Amenity = villaUpdateDto.Amenity,
                //    Details = villaUpdateDto.Details,
                //    ImageUrl = villaUpdateDto.ImageUrl,
                //    Occupancy = villaUpdateDto.Occupancy,
                //    Rate = villaUpdateDto.Rate,
                //    Sqft = villaUpdateDto.Sqft
                //};
                await villaRepository.UpdateAsync(villa);
                response.StatusCode = HttpStatusCode.NoContent;
                response.IsSuccess = true;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string>() { ex.ToString() };
            };
            return response;
        }


        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
            var villa = await villaRepository.GetAsync(v => v.Id == id, tracked: false);

            VillaUpdateDto villaUpdateDto = mapper.Map<VillaUpdateDto>(villa);

            //VillaUpdateDto villaDto = new()  
            //{
            //    Id = villa.Id,
            //    Name = villa.Name,
            //    Amenity = villa.Amenity,
            //    Details = villa.Details,
            //    ImageUrl = villa.ImageUrl,
            //    Occupancy= villa.Occupancy,
            //    Rate = villa.Rate,
            //    Sqft = villa.Sqft
            //};

            if (villa == null)
            {
                return BadRequest();
            }
            patchDto.ApplyTo(villaUpdateDto, ModelState);

            Villa model = mapper.Map<Villa>(villaUpdateDto);

            //Villa model = new()
            //{
            //    Id = villaDto.Id,
            //    Name = villaDto.Name,
            //    Amenity = villaDto.Amenity,
            //    Details = villaDto.Details,
            //    ImageUrl = villaDto.ImageUrl,
            //    Occupancy = villaDto.Occupancy,
            //    Rate = villaDto.Rate,
            //    Sqft = villaDto.Sqft
            //};
            await villaRepository.UpdateAsync(model);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }

    }
}
