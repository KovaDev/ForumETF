using ForumETF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Net;
using PagedList;
using ForumETF.ViewModels;
using System.IO;

namespace ForumETF.Controllers
{
    [RoutePrefix("Post")]
    public class PostController : Controller
    {
        private AppDbContext db = null;
        private UserManager<AppUser> manager = null;

        public PostController ()
	    {
            this.db = new AppDbContext();
            this.manager = new UserManager<AppUser>(new UserStore<AppUser>(db));
    	}

        [HttpGet]
        public ActionResult Create()
        {
            CreatePostViewModel viewModel = new CreatePostViewModel
            {
                Categories = new SelectList(CategoriesDropdownList(), "Value", "Text")
            };
           
            return View(viewModel);
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)] // na ovaj nacin ce akcija prihvatati i druge vrijednosti osim modela MOZDA :D
        public async Task<ActionResult> Create(CreatePostViewModel model)
        {
            var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId());
            ICollection<Tag> tagList = new List<Tag>();
            ICollection<PostAttachment> attachments = new List<PostAttachment>();

            foreach (var file in model.Files)
            {
                if (file.ContentLength > 0)
                {
                    var filename = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Uploads/Attachments"), filename);
                    file.SaveAs(path);
                    attachments.Add(new PostAttachment
                    {
                        FilePath = path,
                        FileName = filename
                    });
                }
            }

            if (!String.IsNullOrEmpty(model.Tags) && !String.IsNullOrWhiteSpace(model.Tags))
            {
                string[] tagNames = model.Tags.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string tagName in tagNames)
                {
                    tagList.Add(GetTag(tagName));
                }
            }
            
            Category cat = await db.Categories.FindAsync(model.SelectedId);
            
            var post = new Post
            {
                Title = model.Title,
                Content = model.Content,
                Votes = model.Votes,
                User = currentUser,
                Category = cat,
                Tags = tagList,
                Attachments = attachments
            };

            if (ModelState.IsValid)
            {
                db.Posts.Add(post);
                await db.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }
            
            return View(post);
        }

        [HttpGet]
        [Route("Details/{postId:int}")]
        public async Task<ActionResult> Details(int? postId)
        {
            if (postId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Post post = await db.Posts.FindAsync(postId);

            PostDetailsViewModel viewModel = new PostDetailsViewModel
            {
                PostID = post.PostId,
                Title = post.Title,
                Content = post.Content,
                Votes = post.Votes,
                CreatedAt = post.CreatedAt,
                User = post.User,
                Tags = post.Tags.ToList(),
                Comments = post.Comments.ToList(),
                Answers = post.Answers.ToList(),
                Attachments = post.Attachments.ToList()
            };

            if (post == null)
            {
                //return HttpNotFound();
                return View("404");
            }

            return View(viewModel);
        }

        //[Route("Tags/{tagName}")]
        public ActionResult GetPostsByTag(string tagName, int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            Tag tag = db.Tags.Where(t => t.TagName == tagName).SingleOrDefault();

            var posts = db.Posts.Where(p => p.Tags.Select(t => t.TagName).Contains(tagName))
                .OrderByDescending(p => p.CreatedAt)
                .ToPagedList(pageNumber, pageSize);

            ViewBag.Tag = tagName;
            
            return View("PostsByTag", posts);
        }

        public ActionResult GetPostsByCategory(string categoryName, int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var category = db.Categories.Where(c => c.CategoryName == categoryName).SingleOrDefault();

            var posts = db.Posts.Where(p => p.Category.CategoryName == categoryName)
                .OrderByDescending(p => p.CreatedAt)
                .ToPagedList(pageNumber, pageSize);

            ViewBag.Category = categoryName;

            return View("PostsByCategory", posts);
        }

        public FileStreamResult GetFile(string filename, int? postId)
        {
            PostAttachment file = db.PostAttachments.Where(a => a.Post.PostId == postId && a.FileName == filename).SingleOrDefault();

            string path = file.FilePath;
            string mime = MimeMapping.GetMimeMapping(file.FileName);

            return File(new FileStream(path, FileMode.Open), mime, file.FileName);
        }

        private Tag GetTag(string tagName)
        {
            return db.Tags.Where(t => t.TagName == tagName).FirstOrDefault() ?? new Tag { TagName = tagName };
        }

        private List<SelectListItem> CategoriesDropdownList()
        {
            // 1. NACIN
            //var categories = db.Categories.ToList();

            //List<SelectListItem> list = new List<SelectListItem>();

            //foreach (var c in categories)
            //{
            //    list.Add(new SelectListItem { Text = c.CategoryName, Value = c.CategoryId.ToString() });
            //}

            //return list;

            // 2. NACIN
            //var list = db.Categories.Select(c => new SelectListItem
            //    {
            //        Value = c.CategoryId.ToString(),
            //        Text = c.CategoryName
            //    });

            //return list.ToList();

            // 3 NACIN
            var list = from c in db.Categories
                       select new SelectListItem
                       {
                           Value = c.CategoryId.ToString(),
                           Text = c.CategoryName
                       };

            return list.ToList();

        }

    }
}