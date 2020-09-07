using Microsoft.Extensions.Configuration;
using SampleTrackingUi.ApiModels.Samples;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SampleTrackingUi.Services
{
    public class IGTSamplesApi : IIGTSamplesApi
    {
        private static readonly HttpClient Client = new HttpClient();
        private static string _baseAddress;

        public IGTSamplesApi(IConfiguration configuration)
        {
            _baseAddress = configuration.GetSection("Api").GetSection("BaseAddress").Value;

        }

        public async Task<List<SubTypeApi>> GetSubTypesAsync()
        {
            List<SubTypeApi> subTypes = null;
            var response = await Client.GetAsync($"{_baseAddress}/Samples/GetSubTypes");
            if (response.IsSuccessStatusCode)
            {
                subTypes = await response.Content.ReadAsAsync<List<SubTypeApi>>();
            }
            return subTypes;
        }

        public async Task<SampleApi> GetSampleAsync(string kbNumber)
        {
            SampleApi sample = null;
            var response = await Client.GetAsync($"{_baseAddress}/GetSample/{kbNumber}");
            if (response.IsSuccessStatusCode)
            {
                sample = await response.Content.ReadAsAsync<SampleApi>();
            }
            return sample;
        }
        public async Task<SampleApi> GetPatientsAsync(string prefix)
        {
            SampleApi sample = null;
            var response = await Client.GetAsync($"{_baseAddress}/IGT/GetPatients/{prefix}");
            if (response.IsSuccessStatusCode)
            {
                sample = await response.Content.ReadAsAsync<SampleApi>();
            }
            return sample;
        }

    }
}
