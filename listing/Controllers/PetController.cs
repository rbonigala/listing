using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Listing.Model;
using Listing.Services;
using Listing.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Listing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    { 
        IPetService _petService;
        public PetController(IPetService petService)
        {
            _petService = petService;
        }


        [HttpGet]
        public async Task<IEnumerable<PetViewModel>> Get()
        {            
            IEnumerable<PetViewModel> petCarerList = new List<PetViewModel>();
            petCarerList = await _petService.GetPets();

            return petCarerList;
        }
    }
}