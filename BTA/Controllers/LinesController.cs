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
    public class LinesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Lines
        public async Task<ActionResult> Index()
        {
            var lines = db.Lines.Include(l => l.City).Include(l => l.City1).Include(l => l.POI);
            return View(await lines.ToListAsync());
        }

        public ActionResult LinesList(string source, string destination)
        {
            var lines = db.Lines.Where(l => l.SourceCity == db.Cities.Where(c => c.CityName == source).Select(c => c.CityId).FirstOrDefault() &&
                                 l.DestCity == db.Cities.Where(c => c.CityName == destination).Select(c => c.CityId).FirstOrDefault())
                                .Select(l => l).ToList();

            List<POI> lineProviders = new List<POI>();

            foreach(var it in lines)
            {
                lineProviders.Add(db.POIs.Where(p => p.PoiId == it.PoiId).Select(p => p).FirstOrDefault());
            }

            ViewBag.Providers = lineProviders;

            return PartialView(lines);
        }


        // GET: Lines/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Line line = await db.Lines.FindAsync(id);
            if (line == null)
            {
                return HttpNotFound();
            }
            return View(line);
        }

        // GET: Lines/Create
        public ActionResult Create()
        {
            var pois = db.POIs.Where(p => p.Transport == true).Select(p => p).ToList();
            ViewBag.SourceCity = new SelectList(db.Cities, "CityId", "CityName");
            ViewBag.DestCity = new SelectList(db.Cities, "CityId", "CityName");
            ViewBag.PoiId = new SelectList(pois, "PoiId", "Name");
            return View();
        }

        // POST: Lines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "LineId,PoiId,SourceCity,DestCity,Active")] Line line)
        {
            line.Active = true;

            if (ModelState.IsValid)
            {
                db.Lines.Add(line);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.SourceCity = new SelectList(db.Cities, "CityId", "CityName", line.SourceCity);
            ViewBag.DestCity = new SelectList(db.Cities, "CityId", "CityName", line.DestCity);
            ViewBag.PoiId = new SelectList(db.POIs, "PoiId", "Name", line.PoiId);
            
            return View(line);
        }

        // GET: Lines/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Line line = await db.Lines.FindAsync(id);
            if (line == null)
            {
                return HttpNotFound();
            }
            ViewBag.SourceCity = new SelectList(db.Cities, "CityId", "CityName", line.SourceCity);
            ViewBag.DestCity = new SelectList(db.Cities, "CityId", "CityName", line.DestCity);
            ViewBag.PoiId = new SelectList(db.POIs, "PoiId", "Name", line.PoiId);
            return View(line);
        }

        // POST: Lines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "LineId,PoiId,SourceCity,DestCity,Active")] Line line)
        {
            if (ModelState.IsValid)
            {
                db.Entry(line).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.SourceCity = new SelectList(db.Cities, "CityId", "CityName", line.SourceCity);
            ViewBag.DestCity = new SelectList(db.Cities, "CityId", "CityName", line.DestCity);
            ViewBag.PoiId = new SelectList(db.POIs, "PoiId", "Name", line.PoiId);
            return View(line);
        }

        // GET: Lines/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Line line = await db.Lines.FindAsync(id);
            if (line == null)
            {
                return HttpNotFound();
            }
            return View(line);
        }

        // POST: Lines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Line line = await db.Lines.FindAsync(id);
            db.Lines.Remove(line);
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
