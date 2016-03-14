using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using KSTAR.Models;
using KSTAR.Managers;
using System.Collections.Generic;

namespace KSTAR.Controllers
{
    public class ForumController : Controller
    {
        private ApplicationDbContext _context;
        private ForumManager _forumManager;

        public ForumController(ApplicationDbContext context, ForumManager forumManager)
        {
            _context = context;
            _forumManager = forumManager;
        }

        // GET: FGroups
        public IActionResult Index()
        {
            return View(_forumManager.GetGroupWithRelated().ToList());
        }

        public IActionResult Subject(string subject)
        {
            return View(_forumManager.GetTopicWithRelated().Where(t => t.Subject.Title == subject).OrderByDescending(t => t.Active).ToList());
        }

        public IActionResult Topic(int id)
        {
            var ti = _context.ForumTopic.Include(t => t.Post).Include(t => t.User).Include(u => u.User.ForumPost).Include(u => u.User.ForumTopic).Include(u => u.User.ForumUser).Single(s => s.ID == id);
            ti.ViewCount++;
            _forumManager.Update(ti);
            return View(ti);
        }
    }
}
