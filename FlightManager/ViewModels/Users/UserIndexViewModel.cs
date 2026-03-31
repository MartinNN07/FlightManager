using System.ComponentModel.DataAnnotations;

namespace FlightManager.Web.ViewModels.User
{
    public class UserIndexViewModel
    {
        // --- Single user data ---
        public string Id { get; set; } = string.Empty;

        [Display(Name = "Потребителско име")]
        public string UserName { get; set; } = string.Empty;

        [Display(Name = "Имейл")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Собствено име")]
        public string FirstName { get; set; } = string.Empty;

        [Display(Name = "Фамилия")]
        public string LastName { get; set; } = string.Empty;

        [Display(Name = "ЕГН")]
        public string EGN { get; set; } = string.Empty;

        [Display(Name = "Адрес")]
        public string? Address { get; set; }

        [Display(Name = "Телефонен номер")]
        public string? PhoneNumber { get; set; }
    }

    public class UserListViewModel
    {
        // --- Filter parameters ---
        public string? FilterEmail { get; set; }
        public string? FilterUserName { get; set; }
        public string? FilterFirstName { get; set; }
        public string? FilterLastName { get; set; }

        // --- Pagination ---
        public int PageSize { get; set; } = 10;
        public int CurrentPage { get; set; } = 1;
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

        // --- Data ---
        public IEnumerable<UserIndexViewModel> Users { get; set; } = new List<UserIndexViewModel>();
    }
}