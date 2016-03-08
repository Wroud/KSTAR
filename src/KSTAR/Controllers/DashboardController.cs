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

namespace KSTAR.Controllers
{
    [Authorize]
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
            return View(_context.ApplicationUser.ToList());
        }
        public IActionResult RolesList(string id)
        {
            if (id == null)
            {
                return View(_context.ApplicationRole.OrderByDescending(r => r.Priority).ToList());
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
