using AutoMapper;
using FreelancerHR.DTO;
using FreelancerHR.Model;
using FreelancerHR.Repository.Contract;
using FreelancerHR.Service.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancerHR.Service
{
    [Export(typeof(IFreelancerService))]
    public class FreelancerService : IFreelancerService
    {
        CompositionContainer Container;

        private IGenericRepository<Freelancer> FreelancerRepository;
        private IGenericRepository<Address> AddressRepository;
        private IUnitOfWork UnitOfWork;

        [ImportingConstructor]
        public FreelancerService(IUnitOfWork unitOfWork, IGenericRepository<Freelancer> freelancerRepository, IGenericRepository<Address> addressRepository)
        {
            this.UnitOfWork = unitOfWork;

            freelancerRepository.DataFactory = unitOfWork.DataFactory;
            addressRepository.DataFactory = unitOfWork.DataFactory;

            this.FreelancerRepository = freelancerRepository;
            this.AddressRepository = addressRepository;
        }

        public IEnumerable<FreelancerDTO> GetAllFreelancers()
        {
            IEnumerable<FreelancerDTO> freelancersDTO = Mapper.Map<IEnumerable<Freelancer>, IEnumerable<FreelancerDTO>>(this.FreelancerRepository.Get(null,null,"Skill"));
            return freelancersDTO;
        }

        public FreelancerDTO GetFreelancerByID(int ID)
        {
            FreelancerDTO companiesDTO = Mapper.Map<Freelancer, FreelancerDTO>(this.FreelancerRepository.GetByID(ID));
            return companiesDTO;
        }


        public void Save(FreelancerDTO freelancerDTO, AddressDTO address)
        {
            this.FreelancerRepository.Insert(new Freelancer());
            this.AddressRepository.Insert(new Address());
            this.UnitOfWork.Commit();
        }
    }
}
