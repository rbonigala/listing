using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Listing.Model;
using Listing.Services;
using Listing.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Listing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        IPetService _petService;
        ILogger<PetController> _logger;
        public PetController(IPetService petService, ILogger<PetController> logger)
        {
            _petService = petService;
            _logger = logger;
        }


        [HttpGet]
        public async Task<IEnumerable<PetViewModel>> Get(string petType="")
        {
            IEnumerable<PetViewModel> petCarerList = new List<PetViewModel>();
            petCarerList = await _petService.GetPets(petType);
            return petCarerList;
        }
    }
}