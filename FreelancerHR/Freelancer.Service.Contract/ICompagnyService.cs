using FreelancerHR.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancerHR.Service.Contract
{
    public interface ICompagnyService
    {
        IEnumerable<CompagnyDTO> GetAllCompagnies();
        CompagnyDTO GetCompagnyByID(int ID);
        void Update(CompagnyDTO compagnyDTO);
    }
}
