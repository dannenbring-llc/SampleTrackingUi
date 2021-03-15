using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SampleTrackingUi.ApiModels.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SampleTrackingUi.Services
{
    public class ReportService : IReportService
    {
        private readonly HttpClient client;
        private string _baseAddress;

        public ReportService(IConfiguration configuration)
        {
            _baseAddress = configuration.GetSection("Api").GetSection("ReportsBaseAddress").Value;
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-api-key", configuration.GetSection("x-api-key").Value);
        }

        public async Task PrintClearMini(IEnumerable<ClearMiniParameter> parameters)
        {
            //HttpResponseMessage response = await client.PostAsJsonAsync($"{_baseAddress}/Test", parameters.ToList()).ConfigureAwait(false);
            //return;

            HttpResponseMessage response = await client.GetAsync($"{_baseAddress}/ClearMini?patId={parameters.First().PatId}&lognumber={parameters.First().LogNumber}");
            return;
        }
    }
}
