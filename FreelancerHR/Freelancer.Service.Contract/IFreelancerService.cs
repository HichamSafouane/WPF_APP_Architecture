using FreelancerHR.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancerHR.Service.Contract
{
    public interface IFreelancerService
    {
        IEnumerable<FreelancerDTO> GetAllFreelancers();
        FreelancerDTO GetFreelancerByID(int ID);
    }
}
