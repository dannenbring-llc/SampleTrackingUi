using Microsoft.Extensions.Configuration;
using SampleTrackingUi.ApiModels.Administration;
using SampleTrackingUi.ApiModels.Printers;
using SampleTrackingUi.ApiModels.Samples;
using SampleTrackingUi.ApiModels.Scans;
using SampleTrackingUi.ApiModels.Sessions;
using SampleTrackingUi.ApiModels.Storage;
using SampleTrackingUi.ViewModels.Samples;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using SubTypeApi = SampleTrackingUi.ApiModels.Samples.SubTypeApi;

namespace SampleTrackingUi.Services
{
    public class SampleTrackingApi : ISampleTrackingApi
    {
        private readonly HttpClient client = new HttpClient();
        private readonly string _baseAddress;

        public SampleTrackingApi(IConfiguration configuration)
        {
            _baseAddress = configuration.GetSection("Api").GetSection("BaseAddress").Value;
        }

        public async Task<List<UserApi>> GetUsersAsync()
        {
            List<UserApi> users = null;
            HttpResponseMessage response = await client.GetAsync($"{_baseAddress}/Administration/Users");
            if (response.IsSuccessStatusCode)
            {
                users = await response.Content.ReadAsAsync<List<UserApi>>();
            }
            return users;
        }

        public async Task<List<ApplicationRoleApi>> GetApplicationRolesAsync()
        {
            List<ApplicationRoleApi> applicationRoles = null;
            HttpResponseMessage response = await client.GetAsync($"{_baseAddress}/Administration/ApplicationRoles");
            if (response.IsSuccessStatusCode)
            {
                applicationRoles = await response.Content.ReadAsAsync<List<ApplicationRoleApi>>();
            }
            return applicationRoles;
        }

        public async Task EditUserAsync(UserAddUpdateApi user)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync($"{_baseAddress}/Administration/Users", user).ConfigureAwait(false);
            await VerifySuccess(response);
        }

        public async Task AddUserAsync(UserAddUpdateApi user)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync($"{_baseAddress}/Administration/Users", user).ConfigureAwait(false);
            await VerifySuccess(response);
        }

        public async Task DeleteUserAsync(int id, int updateUserId)
        {
            //HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Delete, ToString());
            //requestMessage.Content = new StringContent("{\"updateUserId\":"+updateUserId+"}", Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.DeleteAsync($"{_baseAddress}/Administration/User/{id}/{updateUserId}").ConfigureAwait(false);
            await VerifySuccess(response);
        }

        public TResult WaitForTaskResult<TResult>(Task<TResult> task)
        {
            try
            {
                return task.Result;
            }
            catch (AggregateException ex)
            {
                // we are expecting this to contain exactly one inner exception
                if (ex.InnerException != null)
                {
                    throw ex.InnerException;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task VerifySuccess(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return;
            }

            if (response.Content == null)
            {
                throw new Exception("Response content is unavailable.");
            }

            string content = await response.Content.ReadAsStringAsync();

            // for status 500 always throw Exception
            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                throw new Exception("Raw response was  " + content);
            }
        }

        public async Task<List<SubTypeApi>> GetSubTypesAsync()
        {
            List<SubTypeApi> subTypes = null;
            HttpResponseMessage response = await client.GetAsync($"{_baseAddress}/Samples/GetSubTypes");
            if (response.IsSuccessStatusCode)
            {
                subTypes = await response.Content.ReadAsAsync<List<SubTypeApi>>();
            }
            return subTypes;
        }

        public async Task<SessionApi> GetSessionAsync(int userId)
        {
            SessionApi results = null;
            HttpResponseMessage response = await client.GetAsync($"{_baseAddress}/Sessions/Session/{userId}");
            if (response.IsSuccessStatusCode)
            {
                results = await response.Content.ReadAsAsync<SessionApi>();
            }
            return results;
        }
        public async Task<List<SessionApi>> GetSessionsAsync()
        {
            List<SessionApi> results = null;
            HttpResponseMessage response = await client.GetAsync($"{_baseAddress}/Sessions/Sessions");
            if (response.IsSuccessStatusCode)
            {
                results = await response.Content.ReadAsAsync<List<SessionApi>>();
            }
            return results;
        }

        public async Task<SessionApi> AddSessionAsync(int userId)
        {
            SessionApi results = null;
            SessionApi session = null;
            session.UserId = userId;
            session.StatusId = 'A';
            HttpResponseMessage response = await client.PostAsJsonAsync($"{_baseAddress}/Sessions/Session", session).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                results = await response.Content.ReadAsAsync<SessionApi>();
            }
            return results;
        }

        public async Task<SessionApi> UpdateSessionAsync(int userId, char statusId, int sessionId)
        {
            var session = new SessionApi();
            session.UserId = userId;
            session.StatusId = statusId;
            session.SessionId = sessionId;
            HttpResponseMessage response = await client.PostAsJsonAsync($"{_baseAddress}/Sessions/Session/Update", session).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                session = await response.Content.ReadAsAsync<SessionApi>();
            }
            return session;
        }

        public async Task<OpenSessionApi> OpenSessionAsync(OpenSessionApi openSession)
        {
            OpenSessionApi results = null;
            HttpResponseMessage response = await client.PostAsJsonAsync($"{_baseAddress}/Sessions/Session/Open", openSession).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                results = await response.Content.ReadAsAsync<OpenSessionApi>();
            }
            return results;
        }

        public async Task<CloseSessionApi> CloseSession(CloseSessionApi closeSessionApi)
        {
            CloseSessionApi results = null;
            HttpResponseMessage response = await client.PostAsJsonAsync($"{_baseAddress}/Sessions/Session/Close", closeSessionApi).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                results = await response.Content.ReadAsAsync<CloseSessionApi>();
            }
            return results;
        }

        public async Task<CloseSessionApi> CloseSessionDelete(CloseSessionApi closeSessionApi)
        {
            CloseSessionApi results = null;
            HttpResponseMessage response = await client.PostAsJsonAsync($"{_baseAddress}/Sessions/SessionDelete/Close", closeSessionApi).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                results = await response.Content.ReadAsAsync<CloseSessionApi>();
            }
            return results;
        }

        public async Task AddSampleAliquot(SelectSampleViewModel viewModel)
        {
            foreach (var scan in viewModel.ScanList)
            {
                var sampleAliquot = new ApiModels.Samples.SampleAliquotApi();
                sampleAliquot.SubTypeId = viewModel.SubTypeId;
                sampleAliquot.AliquotId = scan.ReScanId;
                sampleAliquot.SampleId = viewModel.Sample.KbNumber;
                sampleAliquot.StatusId = "S";
                sampleAliquot.SessionId = viewModel.Session.SessionId;
                sampleAliquot.CreatedBy = viewModel.UserId;
                sampleAliquot.CreatedDate = DateTime.Now;
                sampleAliquot.ModifydBy = viewModel.UserId;
                sampleAliquot.ModifyDateTime = DateTime.Now;
                HttpResponseMessage response = await client.PostAsJsonAsync($"{_baseAddress}/Samples/SampleAliquot", sampleAliquot).ConfigureAwait(false);
                await VerifySuccess(response);
            }
        }

        public async Task<List<TrayApi>> GetTraysAsync()
        {
            List<TrayApi> trays = null;
            HttpResponseMessage response = await client.GetAsync($"{_baseAddress}/Samples/Trays");
            if (response.IsSuccessStatusCode)
            {
                trays = await response.Content.ReadAsAsync<List<TrayApi>>();
            }
            return trays;
        }

        public async Task<List<TrayDataApi>> GetTrayDataAsync(string trayDescription)
        {
            List<TrayDataApi> trays = null;
            HttpResponseMessage response = await client.GetAsync($"{_baseAddress}/Storage/TrayData/{trayDescription}");
            if (response.IsSuccessStatusCode)
            {
                trays = await response.Content.ReadAsAsync<List<TrayDataApi>>();
            }
            return trays;
        }

        public async Task<TrayApi> GetTrayAsync(string trayName)
        {
            TrayApi tray = null;
            HttpResponseMessage response = await client.GetAsync($"{_baseAddress}/Samples/TrayByName/{trayName}");
            if (response.IsSuccessStatusCode)
            {
                tray = await response.Content.ReadAsAsync<TrayApi>();
            }
            return tray;
        }

        public async Task<TrayApi> GetTrayAsync(int trayId)
        {
            TrayApi tray = null;
            HttpResponseMessage response = await client.GetAsync($"{_baseAddress}/Samples/TrayById/{trayId}");
            if (response.IsSuccessStatusCode)
            {
                tray = await response.Content.ReadAsAsync<TrayApi>();
            }
            return tray;
        }

        public async Task DeleteTrayDataAsync(TrayApi tray)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync($"{_baseAddress}/Storage/DeleteTrayData", tray).ConfigureAwait(false);
        }

        public async Task DeleteTrayAsync(TrayApi tray)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync($"{_baseAddress}/Storage/DeleteTray", tray).ConfigureAwait(false);
        }

        public async Task DeleteAliquotAsync(SampleAliquotApi aliquot)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync($"{_baseAddress}/Storage/Aliquot/Delete", aliquot).ConfigureAwait(false);
        }

        public async Task<TrayLocationApi> UpdateTrayLocationAsync(TrayLocationApi trayLocation)
        {
            TrayLocationApi results = null;
            HttpResponseMessage response = await client.PostAsJsonAsync($"{_baseAddress}/Storage/TrayLocation", trayLocation).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                results = await response.Content.ReadAsAsync<TrayLocationApi>();
            }
            return results;
        }

        public async Task<TrayLocationApi> GetTrayLocationAsync(int trayId)
        {
            TrayLocationApi trayLocation = null;
            HttpResponseMessage response = await client.GetAsync($"{_baseAddress}/Storage/TrayLocation/{trayId}");
            if (response.IsSuccessStatusCode)
            {
                trayLocation = await response.Content.ReadAsAsync<TrayLocationApi>();
            }
            return trayLocation;
        }

        public async Task<ScanApi> GetScanAsync(int userId)
        {
            ScanApi scan = null;
            HttpResponseMessage response = await client.GetAsync($"{_baseAddress}/Scans/ScanData/{userId}");
            if (response.IsSuccessStatusCode)
            {
                scan = await response.Content.ReadAsAsync<ScanApi>();
            }
            return scan;
        }

        public async Task<ScanApi> GetScanByTrayAsync(int trayId)
        {
            ScanApi scan = null;
            HttpResponseMessage response = await client.GetAsync($"{_baseAddress}/Scans/ScanDataByTray/{trayId}");
            if (response.IsSuccessStatusCode)
            {
                scan = await response.Content.ReadAsAsync<ScanApi>();
            }
            return scan;
        }

        public async Task<List<TrayMissingAliquotApi>> GetMissingAliquotsAsync(int sessionId)
        {
            List<TrayMissingAliquotApi> scan = null;
            HttpResponseMessage response = await client.GetAsync($"{_baseAddress}/Scans/MissingAliquots/{sessionId}");
            if (response.IsSuccessStatusCode)
            {
                scan = await response.Content.ReadAsAsync<List<TrayMissingAliquotApi>>();
            }
            return scan;
        }

        public async Task<List<TrayRelocateAliquotsApi>> GetRelocatedAliquotsAsync(int sessionId)
        {
            List<TrayRelocateAliquotsApi> scan = null;
            HttpResponseMessage response = await client.GetAsync($"{_baseAddress}/Scans/RelocatedAliquots/{sessionId}");
            if (response.IsSuccessStatusCode)
            {
                scan = await response.Content.ReadAsAsync<List<TrayRelocateAliquotsApi>>();
            }
            return scan;
        }

        public async Task<List<TrayOrphanedAliquotsApi>> GetOrphanedAliquotsAsync(int sessionId)
        {
            List<TrayOrphanedAliquotsApi> scan = null;
            HttpResponseMessage response = await client.GetAsync($"{_baseAddress}/Scans/OrphanedAliquots/{sessionId}");
            if (response.IsSuccessStatusCode)
            {
                scan = await response.Content.ReadAsAsync<List<TrayOrphanedAliquotsApi>>();
            }
            return scan;
        }

        public async Task<List<TrayUnknownAliquotApi>> GetUnknownAliquotsAsync(int sessionId)
        {
            List<TrayUnknownAliquotApi> scan = null;
            HttpResponseMessage response = await client.GetAsync($"{_baseAddress}/Scans/UnknownAliquots/{sessionId}");
            if (response.IsSuccessStatusCode)
            {
                scan = await response.Content.ReadAsAsync<List<TrayUnknownAliquotApi>>();
            }
            return scan;
        }

        public async Task<List<FreezerApi>> GetFreezersAsync()
        {
            List<FreezerApi> freezers = null;
            HttpResponseMessage response = await client.GetAsync($"{_baseAddress}/Storage/Freezers");
            if (response.IsSuccessStatusCode)
            {
                freezers = await response.Content.ReadAsAsync<List<FreezerApi>>();
            }
            return freezers;
        }

        public async Task<FreezerApi> GetFreezerAsync(int freezerId)
        {
            FreezerApi freezer = null;
            var response = await client.GetAsync($"{_baseAddress}/Storage/Freezer/{freezerId}");
            if (response.IsSuccessStatusCode)
            {
                freezer = await response.Content.ReadAsAsync<FreezerApi>();
            }
            return freezer;
        }

        public async Task<List<DrawerApi>> GetDrawersAsync(int freezerId)
        {
            List<DrawerApi> drawers = null;
            HttpResponseMessage response = await client.GetAsync($"{_baseAddress}/Storage/Drawers/{freezerId}");
            if (response.IsSuccessStatusCode)
            {
                drawers = await response.Content.ReadAsAsync<List<DrawerApi>>();
            }
            return drawers;
        }

        public async Task<List<DrawerApi>> GetDrawersFreeAsync(int freezerId)
        {
            List<DrawerApi> drawers = null;
            HttpResponseMessage response = await client.GetAsync($"{_baseAddress}/Storage/DrawersFree/{freezerId}");
            if (response.IsSuccessStatusCode)
            {
                drawers = await response.Content.ReadAsAsync<List<DrawerApi>>();
            }
            return drawers;
        }
        public async Task<DrawerApi> GetDrawerByDescriptionAsync(string drawerDescription)
        {
            DrawerApi drawer = null;
            HttpResponseMessage response = await client.GetAsync($"{_baseAddress}/Storage/DrawerByDescription/{drawerDescription}");
            if (response.IsSuccessStatusCode)
            {
                drawer = await response.Content.ReadAsAsync<DrawerApi>();
            }
            return drawer;
        }

        public async Task<DrawerApi> GetDrawerByIdAsync(int drawerId)
        {
            DrawerApi drawer = null;
            HttpResponseMessage response = await client.GetAsync($"{_baseAddress}/Storage/DrawerById/{drawerId}");
            if (response.IsSuccessStatusCode)
            {
                drawer = await response.Content.ReadAsAsync<DrawerApi>();
            }
            return drawer;
        }


        public async Task<DrawerApi> GetDrawerAsync(int freezerId, int drawerId)
        {
            DrawerApi drawer = null;
            HttpResponseMessage response = await client.GetAsync($"{_baseAddress}/Storage/Drawer/{freezerId}/{drawerId}");
            if (response.IsSuccessStatusCode)
            {
                drawer = await response.Content.ReadAsAsync<DrawerApi>();
            }
            return drawer;
        }

        public async Task<List<DrawerSlotApi>> GetDrawerSlotsAsync(int drawerId)
        {
            List<DrawerSlotApi> drawerSlots = null;
            HttpResponseMessage response = await client.GetAsync($"{_baseAddress}/Storage/DrawerSlots/{drawerId}");
            if (response.IsSuccessStatusCode)
            {
                drawerSlots = await response.Content.ReadAsAsync<List<DrawerSlotApi>>();
            }
            return drawerSlots;
        }

        public async Task<List<DrawerSlotApi>> GetDrawerSlotsFreeAsync(int drawerId, string trayDescription)
        {
            List<DrawerSlotApi> drawerSlots = null;
            HttpResponseMessage response = await client.GetAsync($"{_baseAddress}/Storage/DrawerSlotsFree/{drawerId}/{trayDescription}");
            if (response.IsSuccessStatusCode)
            {
                drawerSlots = await response.Content.ReadAsAsync<List<DrawerSlotApi>>();
            }
            return drawerSlots;
        }

        public async Task<DrawerSlotApi> GetDrawerSlotByIdAsync(int drawerSlotId)
        {
            DrawerSlotApi drawerSlot = null;
            HttpResponseMessage response = await client.GetAsync($"{_baseAddress}/Storage/DrawerSlotById/{drawerSlotId}");
            if (response.IsSuccessStatusCode)
            {
                drawerSlot = await response.Content.ReadAsAsync<DrawerSlotApi>();
            }
            return drawerSlot;
        }

        public async Task<SampleAliquotApi> GetAliquotAsync(string aliquotId)
        {
            SampleAliquotApi aliquot = null;
            HttpResponseMessage response = await client.GetAsync($"{_baseAddress}/Samples/Aliquot/{aliquotId}");
            if (response.IsSuccessStatusCode)
            {
                aliquot = await response.Content.ReadAsAsync<SampleAliquotApi>();
            }
            return aliquot;
        }

        public async Task<List<SampleAliquotApi>> GetAliquotsAsync(int sessionId)
        {
            List<SampleAliquotApi> aliquots = null;
            HttpResponseMessage response = await client.GetAsync($"{_baseAddress}/Samples/Aliquots/{sessionId}");
            if (response.IsSuccessStatusCode)
            {
                aliquots = await response.Content.ReadAsAsync<List<SampleAliquotApi>>();
            }
            return aliquots;
        }

        public async Task<List<TrayMapDataApi>> GetTrayMapDataAsync(string sampleId, string trayDescription, string patientName, DateTime logDate)
        {
            List<TrayMapDataApi> trayMapData = null;

            var query = HttpUtility.ParseQueryString(string.Empty);
            query["sampleId"] = sampleId;
            query["trayDescription"] = trayDescription;
            query["patientName"] = patientName;
            query["logDate"] = logDate.ToShortDateString();
            var queryString = query.ToString();

            HttpResponseMessage response = await client.GetAsync($"{_baseAddress}/Scans/TrayMapData/?{queryString}");
            if (response.IsSuccessStatusCode)
            {
                trayMapData = await response.Content.ReadAsAsync<List<TrayMapDataApi>>();
            }
            return trayMapData;
        }

        public async Task<List<TrayMapDataApi>> GetTrayMapStaleSamplesAsync(DateTime logDate)
        {
            List<TrayMapDataApi> trayMapData = null;

            var query = HttpUtility.ParseQueryString(string.Empty);
            query["logDate"] = logDate.ToShortDateString();
            var queryString = query.ToString();

            HttpResponseMessage response = await client.GetAsync($"{_baseAddress}/Scans/TrayMapData/Stale/?{queryString}");
            if (response.IsSuccessStatusCode)
            {
                trayMapData = await response.Content.ReadAsAsync<List<TrayMapDataApi>>();
            }
            return trayMapData;
        }

        public async Task<List<TrayMapSerumApi>> GetTrayMapSerumAsync(int trayId)
        {
            List<TrayMapSerumApi> trayMap = null;
            HttpResponseMessage response = await client.GetAsync($"{_baseAddress}/Scans/TrayMap/Serum/{trayId}");
            if (response.IsSuccessStatusCode)
            {
                trayMap = await response.Content.ReadAsAsync<List<TrayMapSerumApi>>();
            }
            return trayMap;
        }

        public async Task<List<SearchResultBySampleApi>> GetSerumTrayMapBySampleAsync(string sampleId, string trayDescription, string patientName, DateTime logDate)
        {
            List<SearchResultBySampleApi> trayMap = null;

            var query = HttpUtility.ParseQueryString(string.Empty);
            query["sampleId"] = sampleId;
            query["trayDescription"] = trayDescription;
            query["patientName"] = patientName;
            query["logDate"] = logDate.ToShortDateString();
            var queryString = query.ToString();

            HttpResponseMessage response = await client.GetAsync($"{_baseAddress}/Scans/TrayMap/Serum?{queryString}");
            if (response.IsSuccessStatusCode)
            {
                trayMap = await response.Content.ReadAsAsync<List<SearchResultBySampleApi>>();
            }
            return trayMap;
        }

        public async Task<List<SearchResultBySampleApi>> GetSerumTrayMapStaleSamplesAsync(DateTime logDate)
        {
            List<SearchResultBySampleApi> trayMap = null;

            var query = HttpUtility.ParseQueryString(string.Empty);
            query["logDate"] = logDate.ToShortDateString();
            var queryString = query.ToString();

            HttpResponseMessage response = await client.GetAsync($"{_baseAddress}/Scans/TrayMap/Serum/Stale?{queryString}");
            if (response.IsSuccessStatusCode)
            {
                trayMap = await response.Content.ReadAsAsync<List<SearchResultBySampleApi>>();
            }
            return trayMap;
        }

        public async Task<FreezerMapApi> GetFreezerMapAsync(int freezerId, string drawerDescription, string slot)
        {
            FreezerMapApi freezerMap = null;

            var query = HttpUtility.ParseQueryString(string.Empty);
            query["sampleId"] = freezerId.ToString();
            query["trayDescription"] = drawerDescription;
            query["patientName"] = slot;
            var queryString = query.ToString();

            HttpResponseMessage response = await client.GetAsync($"{_baseAddress}/Storage/FreezerMap?{queryString}");
            if (response.IsSuccessStatusCode)
            {
                freezerMap = await response.Content.ReadAsAsync<FreezerMapApi>();
            }
            return freezerMap;
        }


        public async Task<AliquotInformationApi> GetAliquotInformation(string aliquotId)
        {
            AliquotInformationApi aliquot = null;
            HttpResponseMessage response = await client.GetAsync($"{_baseAddress}/Samples/AliquotInformation/{aliquotId}");
            if (response.IsSuccessStatusCode)
            {
                aliquot = await response.Content.ReadAsAsync<AliquotInformationApi>();
            }
            return aliquot;
        }

        public async Task<List<PrinterApi>> GetPrinters()
        {
            var printers = new List<PrinterApi>();
            HttpResponseMessage response = await client.GetAsync($"{_baseAddress}/Printers");
            if (response.IsSuccessStatusCode)
            {
                printers = await response.Content.ReadAsAsync<List<PrinterApi>>();

            }
            return printers;
        }

        public async Task<List<RackApi>> GetRacks()
        {
            var racks = new List<RackApi>();
            HttpResponseMessage response = await client.GetAsync($"{_baseAddress}/Storage/Racks");
            if (response.IsSuccessStatusCode)
            {
                racks = await response.Content.ReadAsAsync<List<RackApi>>();

            }
            return racks;
        }

        public async Task<TrayLocationApi> UpdateSampleRackAsync(string sample, string rack)
        {
            TrayLocationApi results = null;
            HttpResponseMessage response = await client.PostAsJsonAsync($"{_baseAddress}/Storage/Rack/{sample}/{rack}", sample).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                results = await response.Content.ReadAsAsync<TrayLocationApi>();
            }
            return results;
        }
    }
}
