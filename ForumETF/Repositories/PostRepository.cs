using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ForumETF.Models;
using PagedList;

namespace ForumETF.Repositories
{
    public class PostRepository : IPostRepository
    {
        private AppDbContext db = null;

        public PostRepository()
        {
            this.db = new AppDbContext();
        }

        public void Create()
        {
            throw new NotImplementedException();
        }

        public List<Post> GetAllPosts()
        {
            throw new NotImplementedException();
        }

        public ViewModels.PostDetailsViewModel GetPostDetails(int postId)
        {
            throw new NotImplementedException();
        }

        public IPagedList<Post> GetMostPopularPosts(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return db.Posts.OrderByDescending(p => p.Votes).ToPagedList(pageNumber, pageSize);
        }
    }
}