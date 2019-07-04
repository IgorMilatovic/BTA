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
using Microsoft.AspNet.Identity;
using System.Security.Principal;

namespace BTA.Controllers
{
    public class CommentsController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        public ApplicationDbContext db = new ApplicationDbContext();

        // GET: Comments
        public async Task<ActionResult> Index(long? itemId, string table)
        {
            if (table == "POIs") table = "POI";
            var comments = db.Comments.Where(c => c.ParentId == itemId && c.TableName == table).Select(c => c).ToListAsync();
            return PartialView(await comments);
        }

        public ActionResult CityCommentsList(string cityName)
        {
            var cityId = db.Cities.Where(c => c.CityName == cityName).Select(c => c.CityId).FirstOrDefault();
            var comments = db.Comments.Where(c => c.ParentId == cityId && c.TableName == "City").Select(c => c).ToList();

            if (comments.Count() == 0)
            {
                ViewBag.NoComments = $"There are no comments for {cityName}";
            }

            return PartialView(comments);
        }

        // GET: Comments/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = await db.Comments.FindAsync(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Comments/Create
        public ActionResult Create()
        {
            //ViewBag.Traveler = new SelectList(db.Travelers, "UserId", "IdentityId");
            return PartialView();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CommentId,ParentId,Traveler,Title,Date,Text,Rating,Active,TableName")] Comment comment, long? parentId, string tableName)
        {
            var identityUser = User.Identity.GetUserId();
            comment.Traveler = db.Travelers.Where(t => t.IdentityId == identityUser).Select(t => t.UserId).FirstOrDefault();
            comment.Date = DateTime.Now;
            comment.Active = true;
            comment.TableName = tableName;
            comment.ParentId = parentId;
            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                await db.SaveChangesAsync();
                //return RedirectToAction("Index");

                if (tableName.Last() == 'y')
                {
                   var pluralTableName = tableName.Replace(tableName.Last(), 'i');
                   pluralTableName += "es";
                }

                return RedirectToAction($"Details/{parentId}", $"{tableName}s");
            }

            ViewBag.Traveler = new SelectList(db.Travelers, "UserId", "IdentityId", comment.Traveler);
            return View(comment);
        }

        // GET: Comments/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = await db.Comments.FindAsync(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.Traveler = new SelectList(db.Travelers, "UserId", "IdentityId", comment.Traveler);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CommentId,ParentId,Traveler,Title,Date,Text,Rating,Active,TableName")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Traveler = new SelectList(db.Travelers, "UserId", "IdentityId", comment.Traveler);
            return View(comment);
        }

        // GET: Comments/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = await db.Comments.FindAsync(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Comment comment = await db.Comments.FindAsync(id);
            db.Comments.Remove(comment);
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
