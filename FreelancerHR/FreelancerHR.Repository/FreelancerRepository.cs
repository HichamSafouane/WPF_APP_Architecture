using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelancerHR.Repository.Contract;
using System.ComponentModel.Composition;

namespace FreelancerHR.Repository
{
    [Export(typeof(IFreelancerRepository))]
    public class FreelancerRepository : IFreelancerRepository
    {
        IDataFactory DBContext;

        [ImportingConstructor]
        public FreelancerRepository(IDataFactory dbContext)
        {
            if( dbContext == null )
            {
                throw new ArgumentNullException("dbContext");
            }

            DBContext = dbContext;
        }

        public List<Model.Freelancer> GetAllFreelancers()
        {
            return DBContext.DbContext.Freelancer.ToList();
        }

        public Model.Freelancer GetFreelancerByID(int ID)
        {
            return DBContext.DbContext.Freelancer.SingleOrDefault(f => f.FreelancerID == ID);
        }
    }
}
