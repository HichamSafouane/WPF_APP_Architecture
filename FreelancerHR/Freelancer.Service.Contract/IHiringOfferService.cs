using FreelancerHR.DTO;
using System.Collections.Generic;
namespace FreelancerHR.Service.Contract
{
    public interface IHiringOfferService
    {
        IEnumerable<HiringOfferDTO> GetAllHiringOffers();
        IEnumerable<FreelancerDTO> GetFeelancersInOffer(IEnumerable<int> hiringOfferEmployeeDTOs);
    }
}