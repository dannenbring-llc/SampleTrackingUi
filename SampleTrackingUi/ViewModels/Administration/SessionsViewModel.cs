using SampleTrackingUi.Models.Sessions;
using System.Collections.Generic;

namespace SampleTrackingUi.ViewModels.Administration
{
    public class SessionsViewModel
    {
        public SessionsViewModel()
        { }

        public string PageTitle { get; set; }

        public List<Session> Sessions { get; set; }

    }
}
