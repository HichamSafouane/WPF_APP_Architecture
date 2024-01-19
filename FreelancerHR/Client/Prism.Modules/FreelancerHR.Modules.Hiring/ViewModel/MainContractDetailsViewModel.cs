﻿using FreelancerHR.DTO;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancerHR.Modules.Hiring.ViewModel
{
    [Export]
    public class MainContractDetailsViewModel : BindableBase
    {
        
        private HiringOfferDTO currentHiringOffer;

        [ImportingConstructor]
        public MainContractDetailsViewModel()
        {

        }

        public HiringOfferDTO CurrentHiringOffer
        {
            get 
            { 
                return currentHiringOffer; 
            }
            set 
            { 
                SetProperty(ref currentHiringOffer, value);
            }
        }
    }
}
