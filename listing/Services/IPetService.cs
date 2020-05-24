using Listing.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Listing.Services
{
    public interface IPetService
    {
        Task<IEnumerable<PetViewModel>> GetPets(string petType);
    }
}
