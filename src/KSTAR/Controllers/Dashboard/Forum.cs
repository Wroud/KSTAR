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
    public partial class DashboardController : Controller
    {
        [Authorize("Dashboard")]
        public IActionResult ForumListGroup()
        {
            return View(_forumManager.GetGroupWithSubjects().ToList());
        }
        [Authorize("Dashboard")]
        public IActionResult ForumAddGroup()
        {
            return View();
        }
        [Authorize("Dashboard")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForumAddGroup(FGroup model)
        {
            if (ModelState.IsValid)
            {
                await _forumManager.AddAsync(model);
                return RedirectToAction(nameof(ForumListGroup));
            }
            return View(model);
        }
        [Authorize("Dashboard")]
        public IActionResult ForumEditGroup(int? id)
        {
            FGroup fGroup = _forumManager.GetGroup(id ?? 0);
            if (fGroup == null)
            {
                return HttpNotFound();
            }
            return View(fGroup);
        }
        [Authorize("Dashboard")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForumEditGroup(FGroup fGroup)
        {
            if (ModelState.IsValid)
            {
                await _forumManager.UpdateAsync(fGroup);
                return RedirectToAction(nameof(ForumListGroup));
            }
            return View(fGroup);
        }
        [Authorize("Dashboard")]
        public async Task<IActionResult> ForumDeleteGroup(int id)
        {
            await _forumManager.DeleteGroupAsync(id);
            return RedirectToAction(nameof(ForumListGroup));
        }
        [Authorize("Dashboard")]
        public IActionResult ForumListSubject()
        {
            return View(_forumManager.GetSubjectWithTopics().ToList());
        }
        [Authorize("Dashboard")]
        public IActionResult ForumAddSubject()
        {
            ViewBag.Groups = new SelectList(_forumManager.GetGroupList(), "ID", "Title");
            return View();
        }
        [Authorize("Dashboard")]
        [HttpPost]
        public async Task<IActionResult> ForumAddSubject(FSubject fSubject)
        {
            if (ModelState.IsValid)
            {
                await _forumManager.AddAsync(fSubject);
                return RedirectToAction(nameof(ForumListSubject));
            }
            ViewBag.Groups = new SelectList(_forumManager.GetGroupList(), "ID", "Title");
            return View(fSubject);
        }
        [Authorize("Dashboard")]
        public IActionResult ForumEditSubject(int? id)
        {
            var fSubject = _forumManager.GetSubject(id ?? 0);
            if (fSubject == null)
            {
                return HttpNotFound();
            }
            ViewBag.Groups = new SelectList(_forumManager.GetGroupList(), "ID", "Title");
            return View(fSubject);
        }
        [Authorize("Dashboard")]
        [HttpPost]
        public async Task<IActionResult> ForumEditSubject(FSubject fSubject)
        {

            if (ModelState.IsValid)
            {
                await _forumManager.UpdateAsync(fSubject);
                return RedirectToAction(nameof(ForumListSubject));
            }
            ViewBag.Groups = new SelectList(_forumManager.GetGroupList(), "ID", "Title");
            return View(fSubject);
        }
        [Authorize("Dashboard")]
        public async Task<IActionResult> ForumDeleteSubject(int id)
        {
            await _forumManager.DeleteSubjectAsync(id);
            return RedirectToAction(nameof(ForumListSubject));
        }

        [Authorize("Dashboard")]
        public IActionResult ForumListTopic()
        {
            return View(_forumManager.GetTopicWithRelated().ToList());
        }
        [Authorize("Dashboard")]
        public IActionResult ForumAddTopic()
        {
            ViewBag.Subjects = new SelectList(_forumManager.GetSubjectList(), "ID", "Title");
            return View();
        }
        [Authorize("Dashboard")]
        [HttpPost]
        public async Task<IActionResult> ForumAddTopic(FTopic fTopic)
        {
            if (ModelState.IsValid)
            {
                fTopic.UserID = User.GetUserId();
                fTopic.Date = DateTime.Now;
                await _forumManager.AddAsync(fTopic);
                return RedirectToAction(nameof(ForumListTopic));
            }
            ViewBag.Subjects = new SelectList(_forumManager.GetSubjectList(), "ID", "Title");
            return View(fTopic);
        }
        [Authorize("Dashboard")]
        public IActionResult ForumEditTopic(int? id)
        {
            var fTopic = _forumManager.GetTopic(id ?? 0);
            if (fTopic == null)
            {
                return HttpNotFound();
            }
            ViewBag.Subjects = new SelectList(_forumManager.GetSubjectList(), "ID", "Title");
            return View(fTopic);
        }
        [Authorize("Dashboard")]
        [HttpPost]
        public async Task<IActionResult> ForumEditTopic(FTopic fTopic)
        {

            if (ModelState.IsValid)
            {
                await _forumManager.UpdateAsync(fTopic);
                return RedirectToAction(nameof(ForumListTopic));
            }
            ViewBag.Subjects = new SelectList(_forumManager.GetSubjectList(), "ID", "Title");
            return View(fTopic);
        }
        [Authorize("Dashboard")]
        public async Task<IActionResult> ForumDeleteTopic(int id)
        {
            await _forumManager.DeleteTopicAsync(id);
            return RedirectToAction(nameof(ForumListTopic));
        }
    }
}
