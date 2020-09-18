using SampleTrackingUi.ApiModels.Administration;
using SampleTrackingUi.ApiModels.Samples;
using SampleTrackingUi.ApiModels.Scans;
using SampleTrackingUi.ApiModels.Sessions;
using SampleTrackingUi.ApiModels.Storage;
using SampleTrackingUi.ViewModels.Samples;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SubTypeApi = SampleTrackingUi.ApiModels.Samples.SubTypeApi;

namespace SampleTrackingUi.Services
{
    public interface ISampleTrackingApi
    {
        Task EditUserAsync(UserAddUpdateApi userModel);
        Task<List<ApplicationRoleApi>> GetApplicationRolesAsync();
        Task<List<UserApi>> GetUsersAsync();
        Task AddUserAsync(UserAddUpdateApi user);
        Task DeleteUserAsync(int id, int updateUserId);
        Task<List<SubTypeApi>> GetSubTypesAsync();
        Task AddSampleAliquot(SelectSampleViewModel viewModel);
        Task<SessionApi> GetSessionAsync(int userId);
        Task<List<SessionApi>> GetSessionsAsync();
        Task<SessionApi> AddSessionAsync(int userId);
        Task<SessionApi> UpdateSessionAsync(int userId, char statusId, int sessionId);
        Task<CloseSessionApi> CloseSession(CloseSessionApi closeSessionApi);
        Task<CloseSessionApi> CloseSessionDelete(CloseSessionApi closeSessionApi);
        Task<OpenSessionApi> OpenSessionAsync(OpenSessionApi openSession);
        Task<List<TrayApi>> GetTraysAsync();
        Task<TrayApi> GetTrayAsync(string trayName);
        Task<TrayApi> GetTrayAsync(int trayId);
        Task DeleteTrayDataAsync(TrayApi tray);
        Task DeleteTrayAsync(TrayApi tray);
        Task DeleteAliquotAsync(SampleAliquotApi aliquot);
        Task<List<TrayDataApi>> GetTrayDataAsync(string trayDescription);
        Task<TrayLocationApi> GetTrayLocationAsync(int trayId);
        Task<TrayLocationApi> UpdateTrayLocationAsync(TrayLocationApi trayLocation);
        Task<ScanApi> GetScanAsync(int userId);
        Task<ScanApi> GetScanByTrayAsync(int trayId);
        Task<List<TrayMissingAliquotApi>> GetMissingAliquotsAsync(int sessionId);
        Task<List<TrayRelocateAliquotsApi>> GetRelocatedAliquotsAsync(int sessionId);
        Task<List<TrayOrphanedAliquotsApi>> GetOrphanedAliquotsAsync(int sessionId);
        Task<List<TrayUnknownAliquotApi>> GetUnknownAliquotsAsync(int sessionId);
        Task<List<FreezerApi>> GetFreezersAsync();
        Task<FreezerApi> GetFreezerAsync(int freezerId);
        Task<DrawerApi> GetDrawerAsync(int freezerId, int drawerId);
        Task<List<DrawerApi>> GetDrawersAsync(int freezerId);
        Task<List<DrawerApi>> GetDrawersFreeAsync(int freezerId);
        Task<DrawerApi> GetDrawerByDescriptionAsync(string drawerDescription);
        Task<DrawerApi> GetDrawerByIdAsync(int drawerId);
        Task<List<DrawerSlotApi>> GetDrawerSlotsAsync(int drawerId);
        Task<List<DrawerSlotApi>> GetDrawerSlotsFreeAsync(int drawerId, string trayDescription);
        Task<DrawerSlotApi> GetDrawerSlotByIdAsync(int drawerSlotId);
        Task<SampleAliquotApi> GetAliquotAsync(string aliquotId);
        Task<List<SampleAliquotApi>> GetAliquotsAsync(int sessionId);
        Task<List<TrayMapDataApi>> GetTrayMapDataAsync(string sampleId, string trayDescription, string patientName, DateTime logDate);
        Task<List<TrayMapSerumApi>> GetTrayMapSerumAsync(int trayId);
        Task<List<SearchResultBySampleApi>> GetSerumTrayMapBySampleAsync(string sampleId, string trayDescription, string patientName, DateTime logDate);
        Task<FreezerMapApi> GetFreezerMapAsync(int freezerId, string drawerDescription, string slot);
        Task<List<TrayMapDataApi>> GetTrayMapStaleSamplesAsync(DateTime logDate);
        Task<List<SearchResultBySampleApi>> GetSerumTrayMapStaleSamplesAsync(DateTime logDate);
        Task<AliquotInformationApi> GetAliquotInformation(string aliquotId);
    }
}
