using FreelancerHR.Repository.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FreelancerHR.DTO;
using FreelancerHR.Model;
using FreelancerHR.Service.Contract;

namespace FreelancerHR.Service
{
    [Export(typeof(IHiringOfferService))]
    class HiringOfferService : IHiringOfferService
    {
        private IGenericRepository<HiringOffer> hiringRepository;
        private IGenericRepository<Freelancer> freelancerGenericRepository;
        private IUnitOfWork unitOfWork;

        static int cout = 0;

        [ImportingConstructor]
        public HiringOfferService( IUnitOfWork unitOfWork, IGenericRepository<HiringOffer> hiringRepository,
            IGenericRepository<Freelancer> freelancerGenericRepository)
        {
            this.hiringRepository = hiringRepository;
            this.freelancerGenericRepository = freelancerGenericRepository;
            this.unitOfWork = unitOfWork;
            cout++;
        }


        public IEnumerable<HiringOfferDTO> GetAllHiringOffers()
        {
            var re = this.hiringRepository.Get(null, null, "Compagny,HiringOfferEmployee");
            IEnumerable<HiringOfferDTO> hiringOffersDTO =
                Mapper.Map<IEnumerable<HiringOffer>, IEnumerable<HiringOfferDTO>>(re);

            return hiringOffersDTO;
        }

        public IEnumerable<FreelancerDTO> GetFeelancersInOffer(IEnumerable<int> hiringOfferEmployeeDTOs)
        {
            var re = this.freelancerGenericRepository.Get(
                t => hiringOfferEmployeeDTOs.Contains(t.FreelancerID)
                );
            IEnumerable<FreelancerDTO> freelancerDTOs =
                Mapper.Map<IEnumerable<Freelancer>, IEnumerable<FreelancerDTO>>(re);
            return freelancerDTOs;

        }


        ~HiringOfferService()
        {

        }
    }
}
