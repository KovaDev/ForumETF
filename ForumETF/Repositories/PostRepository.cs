using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ForumETF.Models;
using ForumETF.ViewModels;
using PagedList;

namespace ForumETF.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext _db;

        public PostRepository(AppDbContext context)
        {
            _db = context;
        }

        public void Create(CreatePostViewModel model, List<PostAttachment> attachments, AppUser user)
        {
            var post = CreatePostObject(model, attachments, user);
            _db.Posts.Add(post);
        }

        public List<Post> GetAllPosts()
        {
            return _db.Posts.ToList();
        }

        public PostDetailsViewModel GetPostDetails(int? postId)
        {
            return null;
        }

        public IPagedList<Post> GetMostPopularPosts(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return _db.Posts.OrderByDescending(p => p.Votes).ToPagedList(pageNumber, pageSize);
        }

        public List<SelectListItem> PopulateCategoriesDropdown()
        {
            var list = from c in _db.Categories
                       select new SelectListItem
                       {
                           Value = c.CategoryId.ToString(),
                           Text = c.CategoryName
                       };

            return list.ToList();
        }

        public Tag GetTag(string tagName)
        {
            return _db.Tags.FirstOrDefault(t => t.TagName == tagName) ?? new Tag { TagName = tagName };
        }

        public FileStreamResult GetFile(string filename, int? postId)
        {
            var file = _db.PostAttachments.SingleOrDefault(a => a.Post.PostId == postId && a.FileName == filename);

            string path = file.FilePath;
            string mime = MimeMapping.GetMimeMapping(file.FileName);

            return null;
            //return File(new FileStream(path, FileMode.Open), mime, file.FileName);
        }

        public IPagedList<Post> GetPostsByTag(string categoryName, int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var category = _db.Categories.SingleOrDefault(c => c.CategoryName == categoryName);

            var posts = _db.Posts.Where(p => p.Category.CategoryName == categoryName)
                .OrderByDescending(p => p.CreatedAt)
                .ToPagedList(pageNumber, pageSize);

            return posts;
        }


        public Post CreatePostObject(CreatePostViewModel model, List<PostAttachment> attachments, AppUser user)
        {
            string decodedContent = GetDecodedHtml(model.Content);

            var post = new Post
            {
                Title = model.Title,
                Content = decodedContent,
                Votes = 0,
                User = user,
                Category = GetCategoryById(model.SelectedId),
                Tags = GetListOfTags(model.Tags),
                Attachments = attachments
            };

            return post;
        }

        public void SavePost()
        {
            _db.SaveChanges();
        }

        private List<Tag> GetListOfTags(string tags)
        {
            List<Tag> tagList = new List<Tag>();

            if (!String.IsNullOrEmpty(tags) && !String.IsNullOrWhiteSpace(tags))
            {
                string[] tagNames = tags.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string tagName in tagNames)
                {
                    tagList.Add(this.GetTag(tagName));
                }
            }

            return tagList;
        }

        private Category GetCategoryById(int id)
        {
            return _db.Categories.Find(id);
        }

        private string GetDecodedHtml(string content)
        {
            string newContent = "";

            if (content != null)
            {
                newContent = WebUtility.HtmlDecode(content);
            }
            else
            {
                newContent = "";
            }

            return newContent;
        }

        public Post CreatePost(CreatePostViewModel model)
        {
            throw new NotImplementedException();
        }




        public Post GetPostById(int? postId)
        {
            return _db.Posts.Find(postId);
        }


        public IPagedList<Post> GetPostsByCategory(string categoryName, int? page)
        {
            throw new NotImplementedException();
        }


        public List<Post> GetTop10Posts()
        {
            return _db.Posts.OrderByDescending(p => p.Votes).Take(10).ToList();
        }
    }
}