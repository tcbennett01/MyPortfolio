using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using tbPortfolio.Models;
using PagedList.Mvc;
using PagedList;

namespace tbPortfolio.Controllers
{
    [RequireHttps]
    public class BlogPostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BlogPosts
        public ActionResult Index(int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            var blogList = db.Posts.AsQueryable();

            // Filter needed for published?
            return View(blogList.OrderByDescending(p => p.Created).ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult Index(string searchStr, int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            var blogList = db.Posts.Where(p => p.Body.Contains(searchStr))
                .Union(db.Posts.Where(p => p.Title.Contains(searchStr)))
                .Union(db.Posts.Where(p => p.Comments.Any(c => c.Body.Contains(searchStr))))
                .Union(db.Posts.Where(p => p.Comments.Any(c => c.Author.DisplayName.Contains(searchStr))))
                .Union(db.Posts.Where(p => p.Comments.Any(c => c.Author.FirstName.Contains(searchStr))))
                .Union(db.Posts.Where(p => p.Comments.Any(c => c.Author.LastName.Contains(searchStr))))
                .Union(db.Posts.Where(p => p.Comments.Any(c => c.Author.UserName.Contains(searchStr))))
                .Union(db.Posts.Where(p => p.Comments.Any(c => c.Author.Email.Contains(searchStr))))
                .Union(db.Posts.Where(p => p.Comments.Any(c => c.UpdateReason.Contains(searchStr))));

            return View(blogList.OrderByDescending(p => p.Created).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Details(string Slug)
        {
            if (String.IsNullOrWhiteSpace(Slug))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.Posts.FirstOrDefault(p => p.Slug == Slug);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // GET: BlogPosts/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: BlogPosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,Created,Updated,Title,Slug,Body,MediaURL,Published")] BlogPost blogPost, HttpPostedFileBase image)
        {
            if (image != null && image.ContentLength > 0)
            {
                //check the file name to make sure its an image
                var ext = Path.GetExtension(image.FileName).ToLower();
                if (ext != ".png" && ext != ".jpg" && ext != ".jpeg" && ext != ".gif" && ext != ".bmp")
                    ModelState.AddModelError("image", "Invalid Format.");
            }

            // Merged from Slug
            if (ModelState.IsValid)
            {
                var Slug = StringUtilities.URLFriendly(blogPost.Title);
                if (String.IsNullOrWhiteSpace(Slug))
                {
                    ModelState.AddModelError("Title", "Invalid title.");
                    return View(blogPost);
                }
                if (db.Posts.Any (p=>p.Slug == Slug))
                {
                    ModelState.AddModelError("Title", "the title must be unique.");
                    return View(blogPost);
                }

                // Merged from Media URL document
                if (image != null)
                {
                    //relative server path
                    var filePath = "/Uploads/";
                    // path on physical drive on server
                    var absPath = Server.MapPath("~" + filePath);
                    // media url for relative path
                    blogPost.MediaURL = filePath + image.FileName;
                    //save image
                    image.SaveAs(Path.Combine(absPath, image.FileName));
                }
                //////////////////////////////////

                blogPost.Created = DateTime.Now;  //Auto populate blog post create date
                db.Posts.Add(blogPost);
                blogPost.Slug = Slug;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blogPost);
        }

        // GET: BlogPosts/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.Posts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // POST: BlogPosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,Created,Updated,Title,Slug,Body,MediaURL,Published")] BlogPost blogPost)
        {
            if (ModelState.IsValid)
            {
                var Slug = StringUtilities.URLFriendly(blogPost.Title);
                blogPost.Slug = Slug;

                db.Entry(blogPost).State = EntityState.Modified;
                blogPost.Updated = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blogPost);
        }

        // GET: BlogPosts/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.Posts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // POST: BlogPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogPost blogPost = db.Posts.Find(id);
            db.Posts.Remove(blogPost);
            db.SaveChanges();
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
