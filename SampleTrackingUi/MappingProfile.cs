using AutoMapper;
using SampleTrackingUi.ApiModels.Administration;
using SampleTrackingUi.ApiModels.Samples;
using SampleTrackingUi.ApiModels.Scans;
using SampleTrackingUi.ApiModels.Sessions;
using SampleTrackingUi.ApiModels.Storage;
using SampleTrackingUi.Models.Administration;
using SampleTrackingUi.Models.Samples;
using SampleTrackingUi.Models.Scans;
using SampleTrackingUi.Models.Sessions;
using SampleTrackingUi.Models.Storage;

namespace SampleTrackingUi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserApi, UserModel>();
            CreateMap<ApplicationRoleApi, ApplicationRoleModel>();
            CreateMap<SubTypeApi, SubType>();
            CreateMap<UserAddUpdateModel, UserAddUpdateApi>();
            CreateMap<Tray, TrayApi>();
            CreateMap<Freezer, FreezerApi>();
            CreateMap<FreezerApi, Freezer>();
            CreateMap<SessionApi, Session>();
            CreateMap<TrayMissingAliquotApi, TrayMissingAliquout>();
            CreateMap<TrayMapSerumApi, TrayMapSerum>();
            CreateMap<TrayColumnApi, TrayColumn>();
            CreateMap<ScanApi, Models.Scans.Scan>();
            CreateMap<ScanDetailApi, ScanDetail>();
            CreateMap<TrayMapDataApi, TrayMapData>();
            CreateMap<TrayOrphanedAliquotsApi, TrayOrphanedAliquots>();
            CreateMap<TrayRelocateAliquotsApi, TrayRelocateAliquots>();
            CreateMap<TrayUnknownAliquotApi, TrayUnknownAliquot>();
            CreateMap<SampleAliquotApi, SampleAliquot>();
            CreateMap<SampleApi, Sample>();
            CreateMap<TrayApi, Tray>();
            CreateMap<OpenSessionApi, OpenSession>();
            CreateMap<CloseSessionApi, CloseSession>();
            CreateMap<DrawerApi, Drawer>();
            CreateMap<DrawerSlotApi, DrawerSlot>();
            CreateMap<FreezerApi, Freezer>();
            CreateMap<FreezerMapApi, FreezerMap>();
            CreateMap<TrayDataApi, TrayData>();
            CreateMap<TrayLocationApi, TrayLocation>();
        }
    }
}
