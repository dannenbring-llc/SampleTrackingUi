using SampleTrackingUi.ApiModels.Samples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleTrackingUi.ViewModels.SearchViewModels
{
    public class AliquotLookupViewModel
    {
        public AliquotLookupViewModel()
        {
            AliquotInformation = new AliquotInformationApi();
        }

        public AliquotInformationApi AliquotInformation { get; set; }
    }
}
