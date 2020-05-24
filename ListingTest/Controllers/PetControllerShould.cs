using System.Collections.Generic;
using Xunit;
using Listing.Controllers;
using Listing.ViewModel;
using ListingTest.Mocks;
using Moq;
using Listing.Services;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ListingTest
{
    public class PetControllerShould
    {
        [Fact]
        public void ReturnResultWithCorrectModelWhenCallingAction()
        {
            Mock<IPetService> petServiceMock = new Mock<IPetService>();
            Mock<ILogger<PetController>> petControllerLoggerMock = new Mock<ILogger<PetController>>();
            petServiceMock.Setup(m => m.GetPets(It.IsAny<string>())).Returns(
                Task.FromResult( MockProvider.PetViewModelMock));
            PetController petController = new PetController(petServiceMock.Object, petControllerLoggerMock.Object);

            var result = petController.Get();

            Assert.IsType<List<PetViewModel>>(result.Result);

        }


       
    }
}
