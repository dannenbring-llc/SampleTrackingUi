using Microsoft.AspNetCore.Mvc;
using SampleTrackingUi.ApiModels.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleTrackingUi.Services
{
    public interface IReportService
    {
        //Task PrintClearMini(string logNumber, string patId);
        Task PrintClearMini(IEnumerable<ClearMiniParameter> parameters);
    }
}
