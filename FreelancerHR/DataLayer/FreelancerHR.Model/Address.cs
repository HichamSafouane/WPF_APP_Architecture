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
    
    public partial class Address
    {
        public Address()
        {
            this.Freelancer = new HashSet<Freelancer>();
            this.Compagny = new HashSet<Compagny>();
        }
    
        public int AddressID { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public int PostalCode { get; set; }
    
        public virtual ICollection<Freelancer> Freelancer { get; set; }
        public virtual ICollection<Compagny> Compagny { get; set; }
    }
}
