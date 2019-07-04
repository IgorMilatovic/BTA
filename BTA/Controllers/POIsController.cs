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
using System.Configuration;
using System.IO;

namespace BTA.Controllers
{
    public class POIsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: POIs
        public async Task<ActionResult> Index()
        {
            var pOIs = db.POIs.Include(p => p.Category).Include(p => p.City);
            return View(await pOIs.ToListAsync());
        }

        public async Task<ActionResult> PoiLoader(long catid, long cityid)
        {
            var city = db.Cities.Where(c => c.CityId == cityid).Select(c => c.CityName).FirstOrDefault();
            var category = db.Categories.Where(c => c.CategoryId == catid).Select(c => c.CategoryName).FirstOrDefault();
            ViewBag.City = city;
            ViewBag.Category = category;
            var pOIs = db.POIs.Include(p => p.Category).Include(p => p.City).Where(p => p.Category.CategoryId == catid).Where(p => p.City.CityId == cityid);
            return View(await pOIs.ToListAsync());
        }

        public ActionResult RecentPOIs(long id)
        {
            var recentPOIs = db.Categories.Where(c => c.Module == id).Join(db.POIs, c => c.CategoryId, p => p.CategoryId, (c, p) => p)
                                          .ToList().OrderByDescending(p => p.PoiId).Take(4);

            return PartialView(recentPOIs);
        }

        public ActionResult RecentPoisByCategory(long catId, long cityId)
        {
            var pois = db.POIs.Where(p => p.CategoryId == catId && p.CityId == cityId).Select(p => p).ToList().Take(3);

            return PartialView(pois);
        }

        // GET: POIs/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POI pOI = await db.POIs.FindAsync(id);
            if (pOI == null)
            {
                return HttpNotFound();
            }
            return View(pOI);
        }

        // GET: POIs/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            ViewBag.CityId = new SelectList(db.Cities, "CityId", "CityName");
            return View();
        }

        // POST: POIs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PoiId,CityId,Name,Address,Website,PoiImg,Rating,Lon,Lat,Phone,Email,CategoryId,POIDescription,Active")] POI pOI, string catMark, HttpPostedFileBase imageFile, string website)
        {
            catMark = Convert.ToString(catMark);

            if (catMark == "classicPoi")
            {
                var web = pOI.Website;
                var city = db.Cities.Where(c => c.CityId == pOI.CityId).Select(c => c.CityName).FirstOrDefault();
                var cleanURl = "";
                var category = db.Categories.Where(c => c.CategoryId == pOI.CategoryId).Select(c => c.CategoryName).FirstOrDefault();
                var moduleId = db.Categories.Where(c => c.CategoryId == pOI.CategoryId).Select(c => c.Module).FirstOrDefault();
                var module = db.Modules.Where(m => m.ModuleId == moduleId).Select(m => m.ModuleName).FirstOrDefault();

                if (category == "Food & Entertainment" || module == "Accomodation")
                {
                    if (web.Contains("aid="))
                    {
                        var firstIndex = web.IndexOf("aid=");
                        var lastIndex = web.IndexOf("aid=") + 11;
                        var substring = web.Substring(firstIndex, lastIndex);
                        cleanURl = web.Replace(substring, "");
                    }
                    else
                    {
                        cleanURl = web;
                    }

                    var url = Uri.EscapeDataString(cleanURl);
                    var ogKey = Environment.ExpandEnvironmentVariables(
                            ConfigurationManager.AppSettings["OpenGraphAPI"]);
                    var requestUrl = "https://opengraph.io/api/1.1/site/" + url + "?app_id=" + ogKey;
                    dynamic ogResults = new Uri(requestUrl).GetDynamicJsonObject();

                    pOI.Name = Convert.ToString(ogResults.hybridGraph.title);
                    if (cleanURl.Contains("booking")) { pOI.Name = pOI.Name.Split(',')[0]; }
                    else if (cleanURl.Contains("airbnb")) { pOI.Name = pOI.Name.Split('-')[0] + ", " + pOI.Name.Split('-')[1] + ", " + city; }

                    pOI.POIDescription = Convert.ToString(ogResults.hybridGraph.description);
                    pOI.PoiImg = Convert.ToString(ogResults.hybridGraph.image);

                    string gcUrl = "https://maps.googleapis.com/maps/api/geocode/json?sensor=true&address=";
                    string gcKey = Environment.ExpandEnvironmentVariables(
                            ConfigurationManager.AppSettings["GoogleAPI"]);

                    string key = "&key=" + gcKey;

                    dynamic googleResults = new Uri(gcUrl + pOI.Name + key).GetDynamicJsonObject();
                    pOI.Lon = Convert.ToDouble(googleResults.results[0].geometry.location.lng);
                    pOI.Lat = Convert.ToDouble(googleResults.results[0].geometry.location.lat);
                    pOI.Address = Convert.ToString(googleResults.results[0].formatted_address);
                } // This is where opengraph code ends

                pOI.Active = true;

                if (ModelState.IsValid)
                {
                    db.POIs.Add(pOI);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }

            if (catMark == "transportPoi")
            {   
                pOI.Rating = 0;
                pOI.Active = true;
                pOI.Address = null;
                pOI.Website = website;
               
                if (imageFile != null)
                {
                    string extension = Path.GetExtension(imageFile.FileName);
                    string photoName = Convert.ToString("trans_" + pOI.Name.Replace(" ", "_"));
                    string fileName = photoName + extension;
                    pOI.PoiImg = "/Assets/Images/POIs/" + fileName;
                    fileName = Path.Combine(Server.MapPath("/Assets/Images/POIs/"), fileName);
                    pOI.ImageFile = imageFile;
                    pOI.ImageFile.SaveAs(fileName);
                }
                else
                {
                    pOI.ImageFile = null;
                }

                if (ModelState.IsValid)
                {
                    db.POIs.Add(pOI);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", pOI.Category);
            ViewBag.CityId = new SelectList(db.Cities, "CityId", "CityName", pOI.City);
            return View(pOI);
        }

        // GET: POIs/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POI pOI = await db.POIs.FindAsync(id);
            if (pOI == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", pOI.CategoryId);
            ViewBag.CityId = new SelectList(db.Cities, "CityId", "CityName", pOI.CityId);
            return View(pOI);
        }

        // POST: POIs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PoiId,CityId,Name,Address,Website,PoiImg,Rating,Lon,Lat,Phone,Email,CategoryId,POIDescription,Active")] POI pOI)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pOI).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", pOI.CategoryId);
            ViewBag.CityId = new SelectList(db.Cities, "CityId", "CityName", pOI.CityId);
            return View(pOI);
        }

        // GET: POIs/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POI pOI = await db.POIs.FindAsync(id);
            if (pOI == null)
            {
                return HttpNotFound();
            }
            return View(pOI);
        }

        // POST: POIs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            POI pOI = await db.POIs.FindAsync(id);
            db.POIs.Remove(pOI);
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
