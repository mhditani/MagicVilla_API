using AutoMapper;
using MagicVilla_villaAPI.Models;
using MagicVilla_villaAPI.Models.DTO;
using MagicVilla_villaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVilla_villaAPI.Controllers
{
    [Route("api/VillaNumberAPI")]
    [ApiController]
    public class VillaNumberAPIController : ControllerBase
    {
        protected APIResponse response;
        private readonly IVillaNumberRepository villaNumberRepository;
        private readonly IMapper mapper;
        private readonly IVillaRepository villaRepository;

        public VillaNumberAPIController(IVillaNumberRepository villaNumberRepository, IMapper mapper, IVillaRepository villaRepository)
        {
            this.villaNumberRepository = villaNumberRepository;
            this.mapper = mapper;
            this.villaRepository = villaRepository;
            this.response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult<APIResponse>> GetVillaNumbers()
        {
            try
            {
                IEnumerable<VillaNumber> villaNumbersList = await villaNumberRepository.GetAllAsync();
                response.Result = mapper.Map<List<VillaNumberDto>>(villaNumbersList);    
                response.StatusCode = HttpStatusCode.OK;
                return Ok(response);
            }
            catch (Exception ex)
            {

                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return response;
        }



        [HttpGet("{id:int}", Name = "GetVillaNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetVillaNumber(int id)
        {
            try
            {
                if(id == 0)
                {
                    response.StatusCode=HttpStatusCode.BadRequest;
                    return BadRequest(response);
                }
                var villaNumber = await villaNumberRepository.GetAsync(v => v.VillaNo == id);
                if(villaNumber == null)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(response);
                }
                response.Result = mapper.Map<VillaNumberDto>(villaNumber);
                response.StatusCode = HttpStatusCode.OK;
                return Ok(response);
            }
            catch (Exception ex)
            {

                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return response;
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateVillaNumber([FromBody] VillaNumberCreateDto villaNumberCreateDto)
        {
            try
            {
                if(await villaNumberRepository.GetAsync(v => v.VillaNo == villaNumberCreateDto.VillaNo) != null)
                {
                    ModelState.AddModelError("CustomError", "Villa Number already exists");
                    return BadRequest(ModelState);
                }
                if(await villaRepository.GetAsync(v => v.Id == villaNumberCreateDto.VillaId) == null)
                {
                    ModelState.AddModelError("CustomError", "Villa Id is invalid");
                    return BadRequest(ModelState);
                }
                if(villaNumberCreateDto == null)
                {
                    return BadRequest(villaNumberCreateDto);
                }
                VillaNumber villaNumber = mapper.Map<VillaNumber>(villaNumberCreateDto);
                await villaNumberRepository.CreateAsync(villaNumber);
                response.Result = mapper.Map<VillaNumberDto>(villaNumber);
                return CreatedAtRoute("GetVillaNumber", new { id = villaNumber.VillaNo }, response);

            }
            catch (Exception ex)
            {

                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return response;
        }




        [HttpDelete("{id:int}", Name = "DeleteVillaNumber")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> DeleteVillaNumber(int id)
        {
            try
            {
                if(id == 0)
                {
                    return BadRequest();
                }
                var villanum = await villaNumberRepository.GetAsync(v => v.VillaNo == id);
                if (villanum == null)
                {
                    return NotFound();
                }
                await villaNumberRepository.RemoveAsync(villanum);
                response.StatusCode = HttpStatusCode.NoContent;
                response.IsSuccess = true;
                return Ok(response);    
            }
            catch (Exception ex)
            {

                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return response;
        }




        [HttpPut("{id:int}", Name = "UpdateVillaNumber")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateVillaNumber(int id, [FromBody] VillaNumberUpdateDto villaNumberUpdateDto)
        {
            try
            {
                if (villaNumberUpdateDto == null || id != villaNumberUpdateDto.VillaNo )
                {
                    return BadRequest();
                }
                if (await villaRepository.GetAsync(v => v.Id == villaNumberUpdateDto.VillaId) == null)
                {
                    ModelState.AddModelError("CustomError", "Villa Id is invalid");
                    return BadRequest(ModelState);
                }
                VillaNumber villaNumber = mapper.Map<VillaNumber>(villaNumberUpdateDto);
                await villaNumberRepository.UpdateAsync(villaNumber);
                response.StatusCode = HttpStatusCode.NoContent;
                response.IsSuccess= true;
                return Ok(response);
            }
            catch (Exception ex)
            {

                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return response;
        }
    }
}
