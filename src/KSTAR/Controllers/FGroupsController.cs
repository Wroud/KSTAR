using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using KSTAR.Models;

namespace KSTAR.Controllers
{
    public class FGroupsController : Controller
    {
        private ApplicationDbContext _context;

        public FGroupsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: FGroups
        public IActionResult Index()
        {
            return View(_context.Groups.ToList());
        }

        // GET: FGroups/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            FGroup fGroup = _context.Groups.Single(m => m.ID == id);
            if (fGroup == null)
            {
                return HttpNotFound();
            }

            return View(fGroup);
        }

        // GET: FGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FGroups/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FGroup fGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Groups.Add(fGroup);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fGroup);
        }

        // GET: FGroups/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            FGroup fGroup = _context.Groups.Single(m => m.ID == id);
            if (fGroup == null)
            {
                return HttpNotFound();
            }
            return View(fGroup);
        }

        // POST: FGroups/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(FGroup fGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Update(fGroup);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fGroup);
        }

        // GET: FGroups/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            FGroup fGroup = _context.Groups.Single(m => m.ID == id);
            if (fGroup == null)
            {
                return HttpNotFound();
            }

            return View(fGroup);
        }

        // POST: FGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            FGroup fGroup = _context.Groups.Single(m => m.ID == id);
            _context.Groups.Remove(fGroup);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
