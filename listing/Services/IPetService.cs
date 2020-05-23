using Listing.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listing.Services
{
    public interface IPetService
    {
        Task<IEnumerable<PetViewModel>> GetPets();
    }
}
