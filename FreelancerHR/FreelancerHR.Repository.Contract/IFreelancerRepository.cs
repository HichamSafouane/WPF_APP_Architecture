using FreelancerHR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancerHR.Repository.Contract
{
    public interface IFreelancerRepository
    {
        List<Freelancer> GetAllFreelancers();
        Freelancer GetFreelancerByID(int ID);
    }
}
