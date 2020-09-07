using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleTrackingUi.ApiModels.Samples;

namespace SampleTrackingUi.ViewModels.SearchViewModels
{
    public class PatientLookupViewModel
    {
        public PatientLookupViewModel()
        {
            Sample = new SampleApi();;
        }

        public SampleApi Sample { get; set; }
    }
}
