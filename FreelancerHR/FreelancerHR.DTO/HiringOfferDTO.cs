using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancerHR.DTO
{
    public class HiringOfferDTO
    {

        public int HiringBusinessID { get; set; }
        public string Compagny { get; set; }
        public string CompagnyEmail { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public int RequiredSkill { get; set; }
        public string Comments { get; set; }
        public System.DateTime CreationDate { get; set; }

        public int Discount { get; set; }
        public int EmployeeInChargeID { get; set; }
        public int HiringRequestID { get; set; }
        public IEnumerable<HiringOfferEmployeeDTO> HiredEmployeeIDs { get; set; }
    
    }
}
