using System.Collections.Generic;
using Xunit;
using Listing.Controllers;
using Listing.ViewModel;
using ListingTest.Mocks;
using Moq;
using Listing.Services;
using System.Threading.Tasks;

namespace ListingTest
{
    public class PetControllerShould
    {
        [Fact]
        public void ReturnResultWithCorrectModelWhenCallingAction()
        {
            Mock<IPetService> petServiceMock = new Mock<IPetService>();
            petServiceMock.Setup(m => m.GetPets()).Returns(
                Task.FromResult( MockProvider.PetViewModelMock));
            PetController petController = new PetController(petServiceMock.Object);

            var result = petController.Get();

            Assert.IsType<List<PetViewModel>>(result.Result);

        }


       
    }
}
