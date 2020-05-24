using Listing.Model;
using System.Collections.Generic;

namespace Listing.ViewModel
{
    public class PetDataModel
    {
        //public PetCarer Carer { get; set; }
        public string Name { get; set; }

        public string Gender { get; set; }

        public int Age { get; set; }

        public List<Pet> Pets { get; set; }
    }
}
