using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SampleTrackingUi.Services;

namespace SampleTrackingUi.Controllers
{
    public class IgtRepositoryController : Controller
    {
        public IgtRepositoryController(IMapper mapper, ISampleTrackingApi sampleTrackingApi)
        {
        }
    }
}