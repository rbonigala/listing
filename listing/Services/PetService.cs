using Listing.Model;
using Listing.ViewModel;
using Microsoft.Extensions.Configuration;
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
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        public PetService(IConfiguration configuration)
        {
            //_clientFactory = clientFactory;
            _configuration = configuration;
        }

        public async Task<IEnumerable<PetViewModel>> GetPets()
        {
            string uri;

            try
            {
                uri = _configuration["AppSettings:PetsDataUri"];
            }
            catch(Exception e)
            {
                throw new Exception("Uri Missing in the configuration");
            }
            List<PetViewModel> petViewModelList = new List<PetViewModel>();
            try
            {
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
