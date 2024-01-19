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
    [Export(typeof(IUnitOfWork))]
    public class UnitOfWork : IUnitOfWork
    {
        IDataFactory dataFactory;

        public IDataFactory DataFactory
        {
            get { return dataFactory; }
        }

        [ImportingConstructor]
        public UnitOfWork(IDataFactory dataFactory)
        {
            if (dataFactory == null)
            {
                throw new ArgumentNullException("dataFactory");
            }

            this.dataFactory = dataFactory;
        }

        public void Commit()
        {
            this.dataFactory.DbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.dataFactory.DbContext != null)
                {
                    this.dataFactory.DbContext.Dispose();
                }
            }
        }


    }
}
