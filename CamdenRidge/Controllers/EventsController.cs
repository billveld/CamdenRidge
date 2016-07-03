using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using CamdenRidge.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace CamdenRidge.Controllers
{
    public class EventsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public EventsController()
        {
        }

        public EventsController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        // GET: Events
        [Authorize(Roles = "Admin, Board Member, AECC Member")]
        public ActionResult Index()
        {
            return View(db.Events.ToList());
        }

        // GET: Events/Details/5
        [Authorize(Roles = "Admin, Board Member, AECC Member")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: Events/Create
        [Authorize(Roles = "Admin, Board Member, AECC Member")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Board Member, AECC Member")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Date,Description,Display,BoardAndCommiteeOnly,Body,Location")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(@event);
        }

        // GET: Events/Edit/5
        [Authorize(Roles = "Admin, Board Member, AECC Member")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Board Member, AECC Member")]
        public ActionResult Edit([Bind(Include = "ID,Name,Date,Description,Display,BoardAndCommiteeOnly,Body,Location")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(@event);
        }

        // GET: Events/Delete/5
        [Authorize(Roles = "Admin, Board Member, AECC Member")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [Authorize(Roles = "Admin, Board Member, AECC Member")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        

        [Authorize]
        public ActionResult Events()
        {
            var model = new EventViewModel();
            var userId = User.Identity.GetUserId();
            model.ShowAdminControls = UserManager.IsInRole(userId, "Admin") || UserManager.IsInRole(userId, "Board Member") || UserManager.IsInRole(userId, "AECC Member");
            DateTime now = DateTime.Now.AddDays(-1);
            model.Events = db.Events.Where(x => x.Date > now && x.Display == true && (x.BoardAndCommmitteeOnly == false || model.ShowAdminControls == true)).ToList();
            
            return View(model);
        }

        public ActionResult ViewEvent(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        
        public ActionResult NextEvent()
        {
            var @event = db.Events.Where(x => x.Date > DateTime.Now && x.BoardAndCommmitteeOnly == false).OrderBy(x=> x.Date).FirstOrDefault();
            if (@event == null)
            {
                @event = new Event();
                @event.ID = 0;
            }
            return View(@event);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
