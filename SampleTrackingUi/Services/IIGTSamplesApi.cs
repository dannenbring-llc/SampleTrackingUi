using SampleTrackingUi.ApiModels.Samples;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleTrackingUi.Services
{
    public interface IIGTSamplesApi
    {
        Task<List<SubTypeApi>> GetSubTypesAsync();
        Task<SampleApi> GetSampleAsync(string kbNumber);
        Task<SampleApi> GetPatientsAsync(string prefix);
    }
}
