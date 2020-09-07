using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SampleTrackingUi.ApiModels.Administration;
using SampleTrackingUi.ApiModels.Sessions;
using SampleTrackingUi.Models.Administration;
using SampleTrackingUi.Models.Sessions;
using SampleTrackingUi.Services;
using SampleTrackingUi.Validators.Administration;
using SampleTrackingUi.ViewModels;
using SampleTrackingUi.ViewModels.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationRoleApi = SampleTrackingUi.ApiModels.Administration.ApplicationRoleApi;

namespace SampleTrackingUi.Controllers
{
    [Route("Administration")]
    public class AdministrationController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISampleTrackingApi _sampleTrackingApi;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AdministrationController> _logger;

        public AdministrationController(IMapper mapper, ISampleTrackingApi sampleTrackingApi, UserManager<ApplicationUser> userManager, ILogger<AdministrationController> logger)
        {
            _mapper = mapper;
            _sampleTrackingApi = sampleTrackingApi;
            _userManager = userManager;
            _logger = logger;
        }

        [Route("Users")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            UsersViewModel viewModel = new UsersViewModel();
            List<ApplicationRoleApi> applicationRole = _mapper.Map<List<ApplicationRoleApi>>(await _sampleTrackingApi.GetApplicationRolesAsync());
            List<UserModel> users = _mapper.Map<List<UserApi>, List<UserModel>>(await _sampleTrackingApi.GetUsersAsync());
            viewModel.Users = users;

            return View(viewModel);
        }

        [Route("AddUser")]
        [HttpGet]
        public async Task<IActionResult> AddUser(AddUserViewModel viewModel)
        {
            List<ApplicationRoleModel> applicationRoles = _mapper.Map<List<ApplicationRoleApi>, List<ApplicationRoleModel>>(await _sampleTrackingApi.GetApplicationRolesAsync());
            viewModel.ApplicationRoles = applicationRoles;
            return View(viewModel);
        }


        [Route("EditUser")]
        [HttpGet]
        public async Task<IActionResult> EditUser(int Id)
        {
            EditUserViewModel viewModel = new EditUserViewModel();
            List<ApplicationRoleModel> applicationRoles = _mapper.Map<List<ApplicationRoleApi>, List<ApplicationRoleModel>>(await _sampleTrackingApi.GetApplicationRolesAsync());
            UserModel user = _mapper.Map<List<UserApi>, List<UserModel>>(await _sampleTrackingApi.GetUsersAsync()).First(u => u.Id == Id);

            viewModel.ApplicationRoles = applicationRoles;
            viewModel.User = user;
            return View(viewModel);
        }

        [Route("EditUser")]
        [HttpPost]
        public async Task<IActionResult> EditUser(AddUserViewModel viewModel)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(viewModel.User.Email);
            if (user != null)
            {
                string code = await _userManager.GeneratePasswordResetTokenAsync(user);
                IdentityResult result = await _userManager.ResetPasswordAsync(user, code, viewModel.User.PasswordHash);

                if (result.Succeeded)
                {
                    //                    return RedirectToAction(nameof(ResetPasswordConfirmation));
                }
            }

            UserAddUpdateApi data = _mapper.Map<UserAddUpdateApi>(viewModel.User);
            await _sampleTrackingApi.EditUserAsync(data);
            return RedirectToAction("Users");
        }

        [Route("AddUser")]
        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserViewModel viewModel, int i)
        {
            var validator = new AddUserValidator(_sampleTrackingApi);
            var modelValidation = await validator.ValidateAsync(viewModel);

            if (!modelValidation.IsValid)
            {
                foreach (var error in modelValidation.Errors)
                {
                    viewModel.ErrorList.Add(error.ErrorMessage);
                }

                List<ApplicationRoleModel> applicationRoles = _mapper.Map<List<ApplicationRoleApi>, List<ApplicationRoleModel>>(await _sampleTrackingApi.GetApplicationRolesAsync());
                viewModel.ApplicationRoles = applicationRoles;

                return View(viewModel);
            }

            UserAddUpdateApi user = _mapper.Map<UserAddUpdateApi>(viewModel.User);
                        
            ApplicationUser UserExists = await _userManager.FindByEmailAsync(viewModel.User.Email);

            user.UpdatedByUser = Convert.ToInt32(User.Claims.FirstOrDefault()?.Value);
            if (UserExists != null)
            {
                return RedirectToAction("AddUser", user);
            }

            try
            {
                await _sampleTrackingApi.AddUserAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add user");
            }

            ApplicationUser updateUserPassword = await _userManager.FindByEmailAsync(viewModel.User.Email);
            if (user != null)
            {
                string code = await _userManager.GeneratePasswordResetTokenAsync(updateUserPassword);
                IdentityResult result = await _userManager.ResetPasswordAsync(updateUserPassword, code, viewModel.User.PasswordHash);

                if (result.Succeeded)
                {
                    //                    return RedirectToAction(nameof(ResetPasswordConfirmation));
                }
            }

            return RedirectToAction("Users");
        }

        [Route("DeleteUser/{Id}")]
        [HttpGet]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _sampleTrackingApi.DeleteUserAsync(id, Convert.ToInt32(User.Claims.FirstOrDefault()?.Value));
            return RedirectToAction("Users");
        }

        [Route("Sessions")]
        [HttpGet]
        public async Task<IActionResult> Sessions()
        {
            var viewModel = new SessionsViewModel();
            viewModel.Sessions = _mapper.Map<List<Session>>(await _sampleTrackingApi.GetSessionsAsync());

            return View(viewModel);
        }

        [Route("DeleteSession")]
        [HttpGet]
        public async Task<IActionResult> DeleteSession(CloseSession viewModel)
        {
            var closeSessionVm = new CloseSessionApi
            {
                SessionId = viewModel.SessionId,
                UserId = viewModel.UserId,
                DrawerSlot = 0,
                CloseDate = DateTime.Now
            };

            await _sampleTrackingApi.CloseSessionDelete(closeSessionVm);

            return RedirectToAction("Sessions");
        }
    }
}