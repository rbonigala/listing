using Listing.Constants;
using Listing.ViewModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Listing.Services
{
    public class PetService : IPetService
    {
        static readonly HttpClient client = new HttpClient();
        private readonly IConfiguration _configuration;
        private readonly ILogger<PetService> _logger;

        public PetService(IConfiguration configuration, ILogger<PetService> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<IEnumerable<PetViewModel>> GetPets()
        {
            string uri;

            try
            {
                _logger.LogDebug("Reading the Uri from the appsettings file");
                uri = _configuration["AppSettings:PetsDataUri"];
            }
            catch(Exception ex)
            {
                _logger.LogError(ErrorMessages.CONFIG_READING);
                throw new Exception("Uri Missing in the configuration");
            }
            List<PetViewModel> petViewModelList = new List<PetViewModel>();
            try
            {
                _logger.LogInformation("Reading data from the {datauri}", uri);
                string responseBody = await client.GetStringAsync(uri);

                var data = JsonConvert.DeserializeObject<List<PetDataModel>>(responseBody);
                Console.WriteLine(data);
                
                var query = from carerandpet in data
                            where (carerandpet.Pets != null)
                            from pets in carerandpet.Pets
                            where pets.Type == "Cat"
                            select new { Gender = carerandpet.Gender, Pets = pets };

                petViewModelList = query.GroupBy(
                        pet => pet.Gender,
                        pet => pet.Pets,
                        (gender, pets) => new PetViewModel
                        {
                            Gender = gender,
                            Names = pets.Select(x => x.Name).ToList()
                        }).ToList();
                
            }
            catch (Exception e)
            {
                throw;
            }
            
            
            return petViewModelList;
        }
    }
}
