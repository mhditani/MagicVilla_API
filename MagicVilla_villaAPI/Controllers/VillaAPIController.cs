using AutoMapper;
using MagicVilla_villaAPI.Data;
using MagicVilla_villaAPI.Models;
using MagicVilla_villaAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MagicVilla_villaAPI.Controllers
{

    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public VillaAPIController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }




        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDto>>> GetVillas()
        {
            IEnumerable<Villa> villaList  =await context.Villas.ToListAsync();
            return Ok(mapper.Map<List<VillaDto>>(villaList));

            //var villas = await  context.Villas.ToListAsync();
            //return Ok(villas);        
        }

        [HttpGet("{id:int}", Name ="GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(400)]
        public async Task<ActionResult<VillaDto>> GetVilla(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var villa = await context.Villas.FirstOrDefaultAsync(v => v.Id == id);
            if(villa == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<VillaDto>(villa));


        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaDto>> CreateVilla([FromBody]VillaCreateDto villaCreateDto)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            if( await context.Villas.FirstOrDefaultAsync(v => v.Name.ToLower() == villaCreateDto.Name.ToLower()) != null)
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

            Villa model  = mapper.Map<Villa>(villaCreateDto);

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
             await context.Villas.AddAsync(model);
            await context.SaveChangesAsync();

            return CreatedAtRoute("GetVilla", new {id = model.Id},model);
        }

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var villa =await context.Villas.FirstOrDefaultAsync(x => x.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            context.Villas.Remove(villa);
           await context.SaveChangesAsync();
            return NoContent(); 
        }


        [HttpPut("{id:int}", Name ="UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDto villaUpdateDto)
        {
            if(villaUpdateDto == null || id != villaUpdateDto.Id)
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
            context.Villas.Update(villa);
            await context.SaveChangesAsync();
            return NoContent();
        }


        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType (StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialVilla(int id,  JsonPatchDocument<VillaUpdateDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
            var villa = await context.Villas.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id);
            
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

            Villa model = mapper.Map<Villa> (villaUpdateDto); 

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
            context.Villas.Update(model);
            await context.SaveChangesAsync();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }

    }
}
