using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancerHR.DTO
{
    public class FreelancerDTO
    {
        public int FreelancerID { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public int SkillID { get; set; }
        public string Address { get; set; }
        public string Skill { get; set; }
        public byte[] Photo { get; set; }
    }
}
