using FreelancerHR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancerHR.Repository.Contract
{
    public interface ICompagnyRepository
    {
        System.Collections.Generic.List<Compagny> GetAllCompagny();
        Compagny GetCompagnyByID(int ID);
    }
}
