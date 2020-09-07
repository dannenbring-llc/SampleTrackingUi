using SampleTrackingUi.Models.Samples;
using SampleTrackingUi.Models.Sessions;

namespace SampleTrackingUi.ViewModels.Aliquots
{
    public class MoveAliquotsViewModel
    {
        public MoveAliquotsViewModel()
        {
            Session = new Session();
            Tray = new Tray();
        }

        public Session Session { get; set; }
        public Tray Tray { get; set; }
        public bool TrayExists { get; set; }
        public bool CreateTray { get; set; }
        public string ErrorMessage { get; set; }

    }
}
