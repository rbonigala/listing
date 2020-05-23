using System;
using Xunit;
using Moq;
using Listing.Services;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace ListingTest
{
    public class PetServiceShould
    {
        [Fact]        
        public void ReturnExceptionWhenNoUrlSettingIsAvailable()
        {
            Mock<IConfiguration> _configuration = new Mock<IConfiguration>();
            _configuration.SetupGet(x => x[It.Is<string>(s => s == "AppSettings:InvalidAppSetting")]).Returns("http://localhost:6000/Valid");
            Mock<ILogger<PetService>> mockLogger = new Mock<ILogger<PetService>>();
            PetService petService = new PetService(_configuration.Object, mockLogger.Object);            
            Assert.ThrowsAsync<Exception>(() => petService.GetPets());
        }

        [Fact]
        public async void ReturnDataWhenApiIsAvailable()
        {
            Mock<IConfiguration> _configuration = new Mock<IConfiguration>();
            _configuration.SetupGet(x => x[It.Is<string>(s => s == "AppSettings:PetsDataUri")]).Returns("http://localhost:6000/Valid");
            Mock<ILogger<PetService>> mockLogger = new Mock<ILogger<PetService>>();
            PetService petService = new PetService(_configuration.Object, mockLogger.Object);
            var pets = await petService.GetPets();
        }

        [Fact]
        public async void ReturnDataForJustMaleWhenApiIsAvailable()
        {
            Mock<IConfiguration> _configuration = new Mock<IConfiguration>();
            _configuration.SetupGet(x => x[It.Is<string>(s => s == "AppSettings:PetsDataUri")]).Returns("http://localhost:6000/SingleMale");
            Mock<ILogger<PetService>> mockLogger = new Mock<ILogger<PetService>>();
            PetService petService = new PetService(_configuration.Object, mockLogger.Object);
            var pets = await petService.GetPets();
            Assert.Single(pets);
            Assert.Single(pets.ToList()[0].Names);
        }

        [Fact]
        public async void ReturnEmptyListForNoPetAvailable()
        {
            Mock<IConfiguration> _configuration = new Mock<IConfiguration>();
            _configuration.SetupGet(x => x[It.Is<string>(s => s == "AppSettings:PetsDataUri")]).Returns("http://localhost:6000/SingleMaleNoPets");
            Mock<ILogger<PetService>> mockLogger = new Mock<ILogger<PetService>>();
            PetService petService = new PetService(_configuration.Object, mockLogger.Object);
            var pets = await petService.GetPets();
            Assert.Empty(pets);
        }

        [Fact]
        public async void ReturnEmptyListForWhenNoCatAvailable()
        {
            Mock<IConfiguration> _configuration = new Mock<IConfiguration>();
            _configuration.SetupGet(x => x[It.Is<string>(s => s == "AppSettings:PetsDataUri")]).Returns("http://localhost:6000/SingleMaleSingleDog");
            Mock<ILogger<PetService>> mockLogger = new Mock<ILogger<PetService>>();
            PetService petService = new PetService(_configuration.Object, mockLogger.Object);
            var pets = await petService.GetPets();
            Assert.Empty(pets);
        }

    }
}
