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
    
    public partial class HiringRequest
    {
        public HiringRequest()
        {
            this.HiringOffer1 = new HashSet<HiringOffer>();
        }
    
        public int HiringBusinessID { get; set; }
        public int CompagnyID { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public int RequiredSkill { get; set; }
        public string Comments { get; set; }
        public System.DateTime CreationDate { get; set; }
    
        public virtual Compagny Compagny { get; set; }
        public virtual ICollection<HiringOffer> HiringOffer1 { get; set; }
    }
}
