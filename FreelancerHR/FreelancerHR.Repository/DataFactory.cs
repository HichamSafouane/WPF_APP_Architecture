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
    [Export(typeof(IDataFactory))]
    public class DataFactory : IDataFactory
    {

        FreelancerHiringEntities _DBContext;

        static int counter = 0;
        int Counter = 0;

        public FreelancerHiringEntities DbContext
        {
            get { return _DBContext; }
        }

        public DataFactory()
        {
            Counter = ++counter;

            _DBContext = new FreelancerHiringEntities();
            _DBContext.Configuration.LazyLoadingEnabled = false;
            _DBContext.Configuration.ProxyCreationEnabled = false;
        }

        ~DataFactory()
        {
            
        }
    }
}
