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
using Microsoft.AspNet.Mvc.Rendering;
using KSTAR.Managers;

namespace KSTAR.Controllers
{
    public struct UserWithRole
    {
        public ApplicationUser User;
        public ApplicationRole Role;
    }
    [Authorize("NotBanned")]
    public partial class DashboardController : Controller
    {
        private ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ForumManager _forumManager;
        public DashboardController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, ApplicationDbContext context, ForumManager forumManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _forumManager = forumManager;
        }
        [Authorize("Dashboard")]
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> UpToAdmin()
        {
            var admin = await _context.ApplicationRole.SingleAsync(r => r.Name == "Administrator");
            if (_context.UserRoles.Where(u => u.RoleId == admin.Id).Count() == 0)
            {
                var uid = User.GetUserId();
                var user = await _context.ApplicationUser.SingleOrDefaultAsync(u => u.Id == uid);
                if (user != null)
                {
                    var result = await _userManager.AddToRoleAsync(user, admin.Name);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
