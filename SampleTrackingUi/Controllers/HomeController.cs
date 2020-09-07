using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleTrackingUi.Models;
using SampleTrackingUi.Models.Sessions;
using SampleTrackingUi.Models.Storage;
using SampleTrackingUi.Services;
using SampleTrackingUi.ViewModels.HomeViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SampleTrackingUi.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IMapper _mapper { get; }
        public ISampleTrackingApi _sampleTrackingApi { get; }
        private readonly int UserId;

        public HomeController(IMapper mapper, ISampleTrackingApi sampleTrackingApi)
        {
            _mapper = mapper;
            _sampleTrackingApi = sampleTrackingApi;
        }

        public async Task<IActionResult> Index(HomeViewModel viewModel)
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var session = await _sampleTrackingApi.GetSessionAsync(userId);
            viewModel.Session = _mapper.Map<Session>(session);
            //viewModel.Freezers = _mapper.Map<List<Freezer>>(await _sampleTrackingApi.GetFreezersAsync());
            viewModel.Freezers = _mapper.Map<List<Freezer>>(await _sampleTrackingApi.GetFreezersAsync()).OrderBy(f => f.Id).ToList();
            //viewModel.FreezerMap = _mapper.Map<FreezerMap>(await _sampleTrackingApi.GetFreezerMapAsync(1, null, null));


            return View(viewModel);
        }


        public IActionResult About()
        {
            //var sample = SampleTrackingApi.GetUsers();
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Register(RegisterModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await userManager.FindByNameAsync(model.UserName);

        //        if (user == null)
        //        {
        //            user = new User
        //            {
        //                Id = Guid.NewGuid().ToString(),
        //                NetUser = model.UserName
        //            };

        //            var result = await userManager.CreateAsync(user, model.Password);
        //        }
        //        return View("Index");
        //    }

        //    return View();
        //}
    }
}
