using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BTA.Models;

namespace BTA.Controllers
{
    public class TravelersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Travelers
        public async Task<ActionResult> Index()
        {
            return View(await db.Travelers.ToListAsync());
        }

        // GET: Travelers/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Traveler traveler = await db.Travelers.FindAsync(id);
            if (traveler == null)
            {
                return HttpNotFound();
            }
            return View(traveler);
        }

        // GET: Travelers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Travelers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "UserId,IdentityId,Active,ImgUrl,FullName")] Traveler traveler)
        {
            if (ModelState.IsValid)
            {
                db.Travelers.Add(traveler);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(traveler);
        }

        // GET: Travelers/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Traveler traveler = await db.Travelers.FindAsync(id);
            if (traveler == null)
            {
                return HttpNotFound();
            }
            return View(traveler);
        }

        // POST: Travelers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UserId,IdentityId,Active,ImgUrl,FullName")] Traveler traveler)
        {
            if (ModelState.IsValid)
            {
                db.Entry(traveler).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(traveler);
        }

        // GET: Travelers/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Traveler traveler = await db.Travelers.FindAsync(id);
            if (traveler == null)
            {
                return HttpNotFound();
            }
            return View(traveler);
        }

        // POST: Travelers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Traveler traveler = await db.Travelers.FindAsync(id);
            db.Travelers.Remove(traveler);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
