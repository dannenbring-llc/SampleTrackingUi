using SampleTrackingUi.Models.Sessions;
using System.Collections.Generic;

namespace SampleTrackingUi.ViewModels.Aliquots
{
    public class DeleteAliquotViewModel
    {
        public DeleteAliquotViewModel()
        {
            Session = new Session();
        }

        public Session Session { get; set; }
        public List<string> AliquotsDelete { get; set; }
        public bool TrayExists { get; set; }
        public bool CreateTray { get; set; }
        public string ErrorMessage { get; set; }

    }
}
