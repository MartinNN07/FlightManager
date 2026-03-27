using FlightManager.Data.Models;
using FlightManager.Web.ViewModels.Account;

namespace FlightManager.Web.Mappers
{
    public static class AccountMapper
    {

        public static User ToModel(RegisterViewModel viewModel)
        {
            return new User
            {
                UserName    = viewModel.Email,
                Email       = viewModel.Email,
                FirstName   = viewModel.FirstName,
                LastName    = viewModel.LastName,
                EGN         = viewModel.EGN,
                PhoneNumber = viewModel.PhoneNumber,
                Address     = viewModel.Address
            };
        }

        public static User ToModel(ResetPasswordViewModel viewModel, User existing)
        {
            existing.Email    = viewModel.Email;
            existing.UserName = viewModel.Email;
            return existing;
        }

        public static LoginViewModel ToLoginViewModel(string? returnUrl = null)
        {
            return new LoginViewModel
            {
                ReturnUrl = returnUrl
            };
        }

        public static ResetPasswordViewModel ToResetPasswordViewModel(string code, string email)
        {
            return new ResetPasswordViewModel
            {
                Code  = code,
                Email = email
            };
        }
    }
}
