using Listing.Model;
using Listing.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ListingTest.Mocks
{
    public static class MockProvider
    {
        public static IEnumerable<PetViewModel> PetViewModelMock
        {
            get
            {
                PetViewModel petViewModel1 = new PetViewModel
                {
                    Gender = "female",
                    Names = new List<String> { "Garfield" }
                };

                List<PetViewModel> petViewModelList = new List<PetViewModel>
                {
                    petViewModel1
                };

                return petViewModelList;
            }
        }
    }
}
