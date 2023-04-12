using BSMediator.Core.Entities;
using BSMediator.Core.Filter;
using BSMediator.Core.Models;
using BSMediator.Core.Services.Opreater;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NFC.Core.Enums;
using System.Threading.Tasks;

namespace BSMediator.FrontEnd.Controllers
{
    [AllowUserFilter((int)ActionEnum.UsersList)]

    public class UserAdminController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IUserService _userService;
        public UserAdminController(IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor=httpContextAccessor;
            _userService = userService;
        }

        // GET: UsersAdmin
        public async Task<IActionResult> Index()
        {
            return View(await _userService.GetAll());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userService.GetById(id.Value);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                await _userService.AddUser(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userService.GetUserModelById(id.Value);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserModel user)
        {
            int userId = int.Parse(_httpContextAccessor.HttpContext.Session.GetString("UserId"));

            var userAdd = await _userService.UpdateUser(user, id, userId);
            if (userAdd == null)
                return View(user);

            return RedirectToAction(nameof(Index));
        }

    }
}
