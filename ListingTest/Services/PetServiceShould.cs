using System;
using System.Collections.Generic;
using System.Text;
using MyTested.AspNetCore.Mvc;
using Listing.Controllers;
using Listing.Model;
using Xunit;
using System.Net.Http;
using Moq;
using Listing.Services;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace ListingTest
{
    public class PetServiceShould
    {
        [Fact]        
        public void ReturnExceptionWhenNoUrlSettingIsAvailable()
        {
            Mock<IConfiguration> _configuration = new Mock<IConfiguration>();
            _configuration.SetupGet(x => x[It.Is<string>(s => s == "AppSettings:InvalidAppSetting")]).Returns("http://localhost:6000/Valid");
            PetService petService = new PetService(_configuration.Object);            
            Assert.ThrowsAsync<Exception>(() => petService.GetPets());
        }

        [Fact]
        public async void ReturnDataWhenApiIsAvailable()
        {
            Mock<IConfiguration> _configuration = new Mock<IConfiguration>();
            _configuration.SetupGet(x => x[It.Is<string>(s => s == "AppSettings:PetsDataUri")]).Returns("http://localhost:6000/Valid");
            PetService petService = new PetService(_configuration.Object);
            var pets = await petService.GetPets();
        }

        [Fact]
        public async void ReturnDataForJustMaleWhenApiIsAvailable()
        {
            Mock<IConfiguration> _configuration = new Mock<IConfiguration>();
            _configuration.SetupGet(x => x[It.Is<string>(s => s == "AppSettings:PetsDataUri")]).Returns("http://localhost:6000/SingleMale");
            PetService petService = new PetService(_configuration.Object);
            var pets = await petService.GetPets();
            Assert.Single(pets);
            Assert.Single(pets.ToList()[0].Names);
        }

        [Fact]
        public async void ReturnEmptyListForNoPetAvailable()
        {
            Mock<IConfiguration> _configuration = new Mock<IConfiguration>();
            _configuration.SetupGet(x => x[It.Is<string>(s => s == "AppSettings:PetsDataUri")]).Returns("http://localhost:6000/SingleMaleNoPets");
            PetService petService = new PetService(_configuration.Object);
            var pets = await petService.GetPets();
            Assert.Empty(pets);
        }

        [Fact]
        public async void ReturnEmptyListForWhenNoCatAvailable()
        {
            Mock<IConfiguration> _configuration = new Mock<IConfiguration>();
            _configuration.SetupGet(x => x[It.Is<string>(s => s == "AppSettings:PetsDataUri")]).Returns("http://localhost:6000/SingleMaleSingleDog");
            PetService petService = new PetService(_configuration.Object);
            var pets = await petService.GetPets();
            Assert.Empty(pets);
        }

    }
}
