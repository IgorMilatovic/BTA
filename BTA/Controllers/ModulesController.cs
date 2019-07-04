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
using BTA.ViewModels;

namespace BTA.Controllers
{
    public class ModulesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Modules
        public async Task<ActionResult> Index()
        {
            return PartialView(await db.Modules.ToListAsync());
        }

        // GET: Modules/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Module module = await db.Modules.FindAsync(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            var citiesAll = db.Cities.Select(c => c.Country).Distinct().ToList();
            ViewBag.Country = new SelectList(citiesAll);
            //ViewBag.Country = new SelectList(from c in citiesAll select new { c.Country , countryName = c.Country }, "Country", "countryName");
            var categories = db.Modules.Where(m => m.Active == true && m.ModuleId == id).Select(m => m.Categories).ToList();

            return View(module);
        }

        // GET: Modules/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Modules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ModuleId,ModuleName,Active")] Module module)
        {
            if (ModelState.IsValid)
            {
                db.Modules.Add(module);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(module);
        }

        // GET: Modules/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Module module = await db.Modules.FindAsync(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            return View(module);
        }

        // POST: Modules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ModuleId,ModuleName,Active")] Module module)
        {
            if (ModelState.IsValid)
            {
                db.Entry(module).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(module);
        }

        // GET: Modules/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Module module = await db.Modules.FindAsync(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            return View(module);
        }

        // POST: Modules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Module module = await db.Modules.FindAsync(id);
            db.Modules.Remove(module);
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

        ///CUSTOM METHODS///
        public JsonResult PopulateCities(string value)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var citiesByCountry = db.Cities.Where(c => c.Country == value).Select(c => c).ToList();

            return new JsonResult { Data = citiesByCountry, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult NavBar()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var test = db.Modules.Where(m => m.Active == true).ToListAsync();
            return new JsonResult { Data = test, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}
