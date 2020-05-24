using System.Collections.Generic;
using System.Threading.Tasks;
using Listing.Services;
using Listing.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Listing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        readonly IPetService _petService;
        readonly ILogger<PetController> _logger;
        public PetController(IPetService petService, ILogger<PetController> logger)
        {
            _petService = petService;
            _logger = logger;
        }


        [HttpGet]
        public async Task<IEnumerable<PetViewModel>> Get(string petType="")
        {
            _logger.LogInformation("Processing Request for {typeofpet}", petType);
            IEnumerable<PetViewModel> petCarerList = new List<PetViewModel>();
            petCarerList = await _petService.GetPets(petType);
            return petCarerList;
        }
    }
}