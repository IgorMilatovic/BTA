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
    public class CitiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cities
        public async Task<ActionResult> Index()
        {
            return View(await db.Cities.ToListAsync());
        }

        // GET: Cities/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = await db.Cities.FindAsync(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // GET: Cities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(City city)
        {
           var cityName = city.CityName;

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Ssl3
                    | System.Net.SecurityProtocolType.Tls
                    | System.Net.SecurityProtocolType.Tls11
                    | System.Net.SecurityProtocolType.Tls12;

            //google geocoding api
            string gcUrl = "https://maps.googleapis.com/maps/api/geocode/json?sensor=true&address=";
            string apiKey = Environment.ExpandEnvironmentVariables(
                    ConfigurationManager.AppSettings["GoogleAPI"]);

            string key = "&key=" + apiKey;

            dynamic googleResults = new Uri(gcUrl + cityName + key).GetDynamicJsonObject();

            //opendatasoft api - population 
            string odUrl = "https://public.opendatasoft.com/api/records/1.0/search/?dataset=worldcitiespop&sort=population&facet=city&refine.city=" + cityName.ToLower();
            dynamic populationResults = new Uri(odUrl).GetDynamicJsonObject();

            city.CityName = Convert.ToString(googleResults.results[0].address_components[0].long_name);
            city.Country = Convert.ToString(googleResults.results[0].address_components[googleResults.results[0].address_components.Length - 1].long_name);
            city.Lat = Convert.ToDouble(googleResults.results[0].geometry.location.lat);
            city.Lon = Convert.ToDouble(googleResults.results[0].geometry.location.lng);
            city.Population = Convert.ToInt32(populationResults.records[0].fields.population);
            city.Active = true;

            if (ModelState.IsValid)
            {
                db.Cities.Add(city);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(city);
        }

        // GET: Cities/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = await db.Cities.FindAsync(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // POST: Cities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CityId,CityName,Country,Lon,Lat,Population,ImgUrl,Active")] City city, HttpPostedFileBase imageFile)
        {
            if (imageFile != null)
            {
                string extension = Path.GetExtension(imageFile.FileName);
                string photoName = city.CityName;
                string fileName = photoName + extension;
                city.ImgUrl = "~/Assets/Images/Cities/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Assets/Images/Cities/"), fileName);
                city.ImageFile = imageFile;
                city.ImageFile.SaveAs(fileName);
            }
            else
            {
                city.ImageFile = null;
            }

            if (ModelState.IsValid)
            {
                db.Entry(city).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(city);
        }

        // GET: Cities/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = await db.Cities.FindAsync(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            City city = await db.Cities.FindAsync(id);
            db.Cities.Remove(city);
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

        public static SelectList GetCountries()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            return new SelectList(db.Cities.Select(c => c.Country.Distinct()));
        }
    }
}
