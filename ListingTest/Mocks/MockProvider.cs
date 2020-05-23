﻿using Listing.Model;
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
                Pet pet1 = new Pet();
                pet1.Name = "abc";
                pet1.Type = "Cat";

                PetCarer petCarer1 = new PetCarer();
                petCarer1.Name = "xyz";
                petCarer1.age = 30;
                petCarer1.Gender = "female";

                PetViewModel petViewModel1 = new PetViewModel();

                petViewModel1.Gender = "female";
                petViewModel1.Names = new List<String> { "Garfield" };

                List<PetViewModel> petViewModelList = new List<PetViewModel>();
                petViewModelList.Add(petViewModel1);

                return petViewModelList;
            }
        }
    }
}