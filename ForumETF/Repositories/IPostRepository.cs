using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumETF.Models;
using ForumETF.ViewModels;
using PagedList;

namespace ForumETF.Repositories
{
    public interface IPostRepository
    {
        void Create();
        List<Post> GetAllPosts();
        PostDetailsViewModel GetPostDetails(int postId);
        IPagedList<Post> GetMostPopularPosts(int? page);
    }
}
