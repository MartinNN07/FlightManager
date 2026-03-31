using FlightManager.Data.Models;
using FlightManager.Web.ViewModels.User;

namespace FlightManager.Web.Mappers
{
    public class UserMapper
    {
        public static UserIndexViewModel ToIndexViewModel(User user)
        {
            return new UserIndexViewModel
            {
                Id = user.Id,
                UserName = user.UserName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                FirstName = user.FirstName,
                LastName = user.LastName,
                EGN = user.EGN,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber
            };
        }

        public static IEnumerable<UserIndexViewModel> ToIndexViewModelList(IEnumerable<User> users)
        {
            return users.Select(ToIndexViewModel);
        }
    }
}