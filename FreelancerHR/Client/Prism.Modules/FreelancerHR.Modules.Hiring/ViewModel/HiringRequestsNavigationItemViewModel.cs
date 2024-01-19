﻿using FreelancerHR.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancerHR.Modules.Hiring.ViewModel
{
    [Export]
    class HiringRequestsNavigationItemViewModel: NavigationItemViewModel
    {

        public HiringRequestsNavigationItemViewModel()
            : base(ViewNames.HiringMainView)
        {

        }
    }
}
