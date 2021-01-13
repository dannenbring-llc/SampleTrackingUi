using SampleTrackingUi.ApiModels.Printers;
using SampleTrackingUi.Entities.Printers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleTrackingUi.ViewModels.Reports
{
    public class ClearMiniViewModel
    {
        public ClearMiniViewModel()
        {
            Labels = new List<Label>();
            var label = new Label();
            Labels.Add(label);
            Printers = new List<PrinterApi>();
            Copies = 1;
        }

        public List<Label> Labels { get; set; }
        public bool ShowReportButton { get; set; }
        public List<PrinterApi> Printers { get; set; }
        public string PrinterName { get; set; }
        public int Copies { get; set; }
    }
}
