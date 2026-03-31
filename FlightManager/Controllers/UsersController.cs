using FlightManager.Data.Models;
using FlightManager.Web.Mappers;
using FlightManager.Web.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightManager.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;

        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        // GET: Users
        public async Task<IActionResult> Index(
            string? filterEmail,
            string? filterUserName,
            string? filterFirstName,
            string? filterLastName,
            int pageSize = 10,
            int currentPage = 1)
        {
            // Clamp page size to allowed values
            if (pageSize != 10 && pageSize != 25 && pageSize != 50)
                pageSize = 10;

            var query = _userManager.Users.AsQueryable();

            // Apply filters
            if (!string.IsNullOrWhiteSpace(filterEmail))
                query = query.Where(u => u.Email != null && u.Email.Contains(filterEmail));

            if (!string.IsNullOrWhiteSpace(filterUserName))
                query = query.Where(u => u.UserName != null && u.UserName.Contains(filterUserName));

            if (!string.IsNullOrWhiteSpace(filterFirstName))
                query = query.Where(u => u.FirstName.Contains(filterFirstName));

            if (!string.IsNullOrWhiteSpace(filterLastName))
                query = query.Where(u => u.LastName.Contains(filterLastName));

            int totalCount = await query.CountAsync();

            var users = await query
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var viewModel = new UserListViewModel
            {
                FilterEmail = filterEmail,
                FilterUserName = filterUserName,
                FilterFirstName = filterFirstName,
                FilterLastName = filterLastName,
                PageSize = pageSize,
                CurrentPage = currentPage,
                TotalCount = totalCount,
                Users = UserMapper.ToIndexViewModelList(users)
            };

            return View(viewModel);
        }
    }
}