using AutoMapper;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.DTO;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MagicVilla_Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly IVillaService villaService;
        private readonly IMapper mapper;

        public VillaController(IVillaService villaService, IMapper mapper)
        {
            this.villaService = villaService;
            this.mapper = mapper;
        }





        public async Task<IActionResult> IndexVilla()
        {
            List<VillaDto> list = new ();

            var response = await villaService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Result));
            }

            return View(list);
        }



        public IActionResult CreateVilla()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVilla(VillaCreateDto model)
        {
            if (ModelState.IsValid)
            {
                var response = await villaService.CreateAsync<APIResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexVilla));
                }
            }
            return View(model);
        }





        public async Task<IActionResult> UpdateVilla(int villaId)
        {
            var response = await villaService.GetAsync<APIResponse>(villaId);
            if (response != null && response.IsSuccess)
            {
                VillaDto model = JsonConvert.DeserializeObject<VillaDto>(Convert.ToString(response.Result));
                return View(mapper.Map<VillaUpdateDto>(model));
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> UpdateVilla(VillaUpdateDto model)
        {
            if (ModelState.IsValid)
            {
                var response = await villaService.UpdateAsync<APIResponse>(model);
                if(response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexVilla));
                }
            }
            return View(model);
        }





        public async Task<IActionResult> DeleteVilla(int villaId)
        {
            var response = await villaService.GetAsync<APIResponse>(villaId);
            if(response != null && response.IsSuccess)
            {
                VillaDto model = JsonConvert.DeserializeObject<VillaDto>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVilla(VillaDto model)
        {

            var response = await villaService.DeleteAsync<APIResponse>(model.Id);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(IndexVilla));
            }
            return View(model);
        }
    }
}
