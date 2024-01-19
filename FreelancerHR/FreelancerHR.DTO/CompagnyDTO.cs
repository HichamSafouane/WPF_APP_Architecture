using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancerHR.DTO
{
    public class CompagnyDTO
    {
        public int CompagnyID { get; set; }
        public string Name { get; set; }
        public int AddressID { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public int PostalCode { get; set; }
        public string EmailAddress { get; set; }
    }
}
