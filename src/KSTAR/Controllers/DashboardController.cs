using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using KSTAR.Models;
using Microsoft.AspNet.Authorization;
using KSTAR.ViewModels.Account;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using System.Security.Claims;

namespace KSTAR.Controllers
{
    public struct UserWithRole
    {
        public ApplicationUser User;
        public ApplicationRole Role;
    }
    [Authorize("NotBanned"), Authorize("Dashboard")]
    public class DashboardController : Controller
    {
        private ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public DashboardController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UsersList()
        {
            ViewBag.Context = _context;
            ViewBag.Roles = _context.ApplicationRole.ToList();
            return View(_context.ApplicationUser.Include(c => c.Roles).ToList());
        }
        public IActionResult RolesList(string id)
        {
            if (id == null)
            {
                return View(_context.ApplicationRole.OrderByDescending(r => r.Priority).Include(r => r.Users).ToList());
            }
            else
            {
                ViewData["Title"] = (from role in _context.ApplicationRole where role.Id == id select role.Name).FirstOrDefault();
                return View("RoleMembers", (from user in _context.ApplicationUser where (from role in _context.UserRoles where role.RoleId == id select role.UserId).Contains(user.Id) select user).ToList());
            }
        }
        public IActionResult AddUser()
        {
            return View();
        }
        // POST: FGroups/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var auser = new ApplicationUser { UserName = model.Name, Email = model.Email, PhoneNumber = model.Phone };
                var result = await _userManager.CreateAsync(auser, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(UsersList));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }
        public IActionResult AddRole()
        {
            return View();
        }
        // POST: FGroups/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(AddRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var auser = new ApplicationRole { Name = model.Name };
                var result = await _roleManager.CreateAsync(auser);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(RolesList));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }
        public async Task<IActionResult> AddUserToRole(string id, string uid)
        {
            var role = await _context.ApplicationRole.SingleOrDefaultAsync(r => r.Id == id);
            var user = await _context.ApplicationUser.SingleOrDefaultAsync(u => u.Id == uid);
            if (role != null && user != null)
            {
                var result = await _userManager.AddToRoleAsync(user, role.Name);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(UsersList));
                }
            }
            return RedirectToAction(nameof(UsersList), new { ErrorMessage = "Не удалось добавить пользователя в группу" });
        }
        public async Task<IActionResult> RoleAddClaim(string id, string name)
        {
            var role = await _context.ApplicationRole.Include(r => r.Claims).SingleOrDefaultAsync(r => r.Id == id);
            if (role != null && role.Claims.Where(c => c.ClaimType == name && c.ClaimValue == "true").Count() == 0)
            {
                var result = await _roleManager.AddClaimAsync(role, new Claim(name, "true"));
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(RolesList), new { id = id });
                }
            }
            return RedirectToAction(nameof(RolesList), new { id = id, ErrorMessage = "Не добавить правило" });
        }
        public async Task<IActionResult> RemoveUserFromRole(string id, string uid)
        {
            var role = await _context.ApplicationRole.SingleOrDefaultAsync(r => r.Id == id);
            var user = await _context.ApplicationUser.SingleOrDefaultAsync(u => u.Id == uid);
            if (role != null && user != null)
            {
                var result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(UsersList));
                }
            }
            return RedirectToAction(nameof(UsersList), new { ErrorMessage = "Не удалось исключить пользователя из группы" });
        }
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
