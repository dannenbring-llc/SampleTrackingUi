using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Internal;
using SampleTrackingUi.Services;
using SampleTrackingUi.ViewModels.Administration;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;

namespace SampleTrackingUi.Validators.Administration
{
    public class AddUserValidator : AbstractValidator<AddUserViewModel>
    {
        private readonly ISampleTrackingApi _sampleTrackingApi;

        public AddUserValidator(ISampleTrackingApi sampleTrackingApi)
        {
            _sampleTrackingApi = sampleTrackingApi;
            RuleFor(x => x.User.Name).NotEmpty();
            RuleFor(x => x.User.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.User.RoleId).NotEmpty();
            RuleFor(x => x.User.PasswordHash).NotEmpty();
            RuleFor(x => x.User.Status).NotEmpty();
            RuleFor(x => x.User.Email).MustAsync(IsUniqueEmail).WithMessage("Email exists already.");
        }

        private async Task<bool> IsUniqueEmail(string emailAddress, CancellationToken cancellationToken)
        {
            var users = await _sampleTrackingApi.GetUsersAsync();
            return users.All(e => e.Email != emailAddress);
        }
    }
}
