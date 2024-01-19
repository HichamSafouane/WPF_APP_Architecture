using AutoMapper;
using FreelancerHR.DTO;
using FreelancerHR.Service.Contract;
using FreelancerHR.Model;
using FreelancerHR.Repository.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancerHR.Service
{
    [Export(typeof(ICompagnyService))]
    public class CompagnyService : ICompagnyService
    {
        CompositionContainer Container;
        private IGenericRepository<Compagny> compagnyRepository;
        private IGenericRepository<Address> addressRepsoitory;
        private IUnitOfWork unitOfWork;

        static int cout = 0;

        [ImportingConstructor]
        public CompagnyService( IUnitOfWork unitOfWork, IGenericRepository<Compagny> compagnyRepository, IGenericRepository<Address> addressRepsoitory)
        {
            this.addressRepsoitory = addressRepsoitory;
            this.compagnyRepository = compagnyRepository;
            this.unitOfWork = unitOfWork;
            cout++;
        }

       

        public IEnumerable<CompagnyDTO> GetAllCompagnies()
        {
            IEnumerable<CompagnyDTO> companiesDTO = Mapper.Map<IEnumerable<Compagny>, IEnumerable<CompagnyDTO>>(this.compagnyRepository.Get());
            return companiesDTO;
        }

        public CompagnyDTO GetCompagnyByID(int ID)
        {
            var compagny = this.compagnyRepository.Get(x => x.CompagnyID == ID, null, "Address").FirstOrDefault();
            if(compagny == null)
            {
                return null;
            }

            CompagnyDTO compagnyDTO = Mapper.Map<Compagny, CompagnyDTO>(compagny);
            return compagnyDTO;
        }


        public void Update(CompagnyDTO compagnyDTO)
        {
            Compagny compagny = Mapper.Map<CompagnyDTO, Compagny>(compagnyDTO);
            this.compagnyRepository.Update(compagny);
            this.addressRepsoitory.Update(compagny.Address);
            this.unitOfWork.Commit();
        }
    }
}
