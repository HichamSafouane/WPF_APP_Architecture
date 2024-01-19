using FreelancerHR.Model;
using FreelancerHR.Repository.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancerHR.Repository
{
    //TODO: Remove Entity Framework reference see C:\Users\safouahn\Downloads\pocwebapi_34c227409f74\Application\Application.Repository\AddressRepository.cs
    [Export(typeof(ICompagnyRepository))]
    public class CompagnyRepository : ICompagnyRepository
    {
        IDataFactory DBContext;

        //public CompagnyRepository()
        //{

        //}

      //  [import]

        [ImportingConstructor]
        public CompagnyRepository(IDataFactory dbContext)
        {
            if( dbContext == null )
            {
                throw new ArgumentNullException("dbContext");
            }

            DBContext = dbContext;
        }

        public List<Compagny> GetAllCompagny()
        {
            //using(FreelancerHiringEntities dbContext = new FreelancerHiringEntities())
            //{
            //    return dbContext.Compagny.ToList();
            //}

           // using (FreelancerHiringEntities dbContext = new FreelancerHiringEntities())
            {
                return DBContext.DbContext.Compagny.ToList();
            }
        }

        public Compagny GetCompagnyByID(int ID)
        {
            return DBContext.DbContext.Compagny.FirstOrDefault(d => d.CompagnyID == ID);
        }
    }
}
