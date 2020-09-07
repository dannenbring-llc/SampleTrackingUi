using SampleTrackingUi.ApiModels.Scans;
using SampleTrackingUi.Models.Scans;
using System.Collections.Generic;

namespace SampleTrackingUi.ViewModels.SearchViewModels
{
    public class FindByKbViewModel
    {
        public FindByKbViewModel()
        {
            ScanData = new Scan();
            SearchResults = new List<SearchResultBySampleApi>();
            TrayMapData = new List<TrayMapDataApi>();
        }

        public string TrayDescription { get; set; }
        public string KbNumber { get; set; }
        public string PatientName { get; set; }
        public string LogDate { get; set; }
        public Scan ScanData { get; set; }
        public List<SearchResultBySampleApi> SearchResults { get; set; }
        public List<TrayMapDataApi> TrayMapData { get; set; }
    }
}
