using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ForumETF.Models;
using ForumETF.Repositories;
using ForumETF.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;

namespace ForumETF.Controllers
{
    [RoutePrefix("Post")]
    public class PostController : Controller
    {
        private readonly AppDbContext _db;
        private readonly UserManager<AppUser> _manager;
        private readonly IPostRepository _repo;

        public PostController ()
	    {
            _db = new AppDbContext();
            _manager = new UserManager<AppUser>(new UserStore<AppUser>(_db));
            _repo = new PostRepository(_db);
    	}

        [HttpGet]
        public ActionResult Create()
        {
            CreatePostViewModel viewModel = new CreatePostViewModel
            {
                Categories = new SelectList(_repo.PopulateCategoriesDropdown(), "Value", "Text")
            };
           
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreatePostViewModel model)
        {
            var currentUser = await GetLoggedInUser();

            if (ModelState.IsValid)
            {
                _repo.Create(model, GetPostAttachments(model.Files), currentUser);
                _repo.SavePost();

                return RedirectToAction("Index", "Home");
            }

            model.Categories = new SelectList(_repo.PopulateCategoriesDropdown(), "Value", "Text");

            return View(model);
        }

        [HttpGet]
        [Route("Details/{postId:int}")]
        public async Task<ActionResult> Details(int? postId)
        {
            if (postId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var post = _repo.GetPostById(postId);

            var viewModel = CreateDetailsViewModel(post);

            if (post == null)
            {
                //return HttpNotFound();
                return View("404");
            }

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Delete(int postId)
        {
            Post post = _db.Posts.Find(postId);

            _db.Posts.Remove(post);
            _db.SaveChanges();

            var postCount = _db.Posts.Count(p => p.User.UserName == User.Identity.Name);
          
            //return RedirectToAction("Profile", "User");
            return Json(new { num_of_posts = postCount });
        }

        //[Route("Tags/{tagName}")]
        public ActionResult GetPostsByTag(string tagName, int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var posts = _db.Posts.Where(p => p.Tags.Select(t => t.TagName).Contains(tagName))
                .OrderByDescending(p => p.CreatedAt)
                .ToPagedList(pageNumber, pageSize);

            ViewBag.Tag = tagName;
            
            return View("PostsByTag", posts);
        }

        public ActionResult GetPostsByCategory(string categoryName, int? page)
        {
            ViewBag.Category = categoryName;

            return View("PostsByCategory", _repo.GetPostsByCategory(categoryName, page));
        }

        public PartialViewResult MostPopularPosts(int? page)
        {
            return PartialView("_Content");
        }

        public PartialViewResult NewestPosts()
        {
            var posts = _db.Posts.OrderByDescending(p => p.CreatedAt).ToList();

            return PartialView("_Newest", posts);
        }

        public PartialViewResult UnansweredPosts()
        {
            var posts = _db.Posts.Where(p => p.Answers.Count == 0).OrderByDescending(p => p.CreatedAt).ToList();

            return PartialView("_Unanswered", posts);
        }

        /// <summary>
        /// Pomocna metoda koja omogucava download attachmenta
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="postId"></param>
        /// <returns></returns>
        public FileStreamResult GetFile(string filename, int? postId)
        {
            PostAttachment file = _db.PostAttachments.SingleOrDefault(a => a.Post.PostId == postId && a.FileName == filename);

            string path = file.FilePath;
            string mime = MimeMapping.GetMimeMapping(file.FileName);

            return File(new FileStream(path, FileMode.Open), mime, file.FileName);
        }

        /// <summary>
        /// Pomocna metoda koja vraca novi Tag objekat ili vec postojeci Tag objekat, na osnovu parametra
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        private Tag GetTag(string tagName)
        {
            return _repo.GetTag(tagName);
        }

        /// <summary>
        /// Pomocna metoda koja obradjuje upload fajlova
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        private List<PostAttachment> GetPostAttachments(IEnumerable<HttpPostedFileBase> files)
        {
            List<PostAttachment> attachments = new List<PostAttachment>();

            foreach (var file in files)
            {
                if (file != null && file.ContentLength != 0)
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

            return attachments;
        }

        /// <summary>
        /// Pomocna metoda koja asinhrono vraca objekat aktivnog korisnik
        /// </summary>
        /// <returns></returns>
        private async Task<AppUser> GetLoggedInUser()
        {
            return await _manager.FindByIdAsync(User.Identity.GetUserId());
        }

        /// <summary>
        /// Pomocna metoda koja kreira instancu PostDetailsViewModel klase na osnovu parametra post
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        private PostDetailsViewModel CreateDetailsViewModel(Post post)
        {
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

            return viewModel;
        }

    }
}