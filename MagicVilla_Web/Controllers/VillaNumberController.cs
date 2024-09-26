using AutoMapper;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.DTO;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MagicVilla_Web.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IVillaNumberService villaNumberService;
        private readonly IMapper mapper;

        public VillaNumberController(IVillaNumberService villaNumberService, IMapper mapper)
        {
            this.villaNumberService = villaNumberService;
            this.mapper = mapper;
        }






        public async Task<IActionResult> IndexVillaNumber()
        {
            List<VillaNumberDto> list = new ();
            var response = await villaNumberService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<VillaNumberDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
    }
}
