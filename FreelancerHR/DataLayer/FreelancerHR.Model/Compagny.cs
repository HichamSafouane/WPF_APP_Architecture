//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FreelancerHR.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Compagny
    {
        public Compagny()
        {
            this.HiringRequest = new HashSet<HiringRequest>();
        }
    
        public int CompagnyID { get; set; }
        public string Name { get; set; }
        public int AddressID { get; set; }
        public string EmailAddress { get; set; }
    
        public virtual Address Address { get; set; }
        public virtual ICollection<HiringRequest> HiringRequest { get; set; }
    }
}
