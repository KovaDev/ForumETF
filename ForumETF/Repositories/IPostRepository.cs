using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using ForumETF.Models;
using ForumETF.ViewModels;
using PagedList;

namespace ForumETF.Repositories
{
    public interface IPostRepository
    {
        // CRUD operations
        void Create(CreatePostViewModel model, List<PostAttachment> attachments, AppUser user);
        List<Post> GetAllPosts();
        Post GetPostById(int? postId);
        PostDetailsViewModel GetPostDetails(int? postId);
        IPagedList<Post> GetMostPopularPosts(int? page);
        List<Post> GetTop10Posts(); 
        void SavePost();

        // helper methods
        Tag GetTag(string tagName);
        List<SelectListItem> PopulateCategoriesDropdown();
        FileStreamResult GetFile(string filename, int? postId);
        IPagedList<Post> GetPostsByCategory(string categoryName, int? page);
        IPagedList<Post> GetPostsByTag(string tagName, int? page);
        Post CreatePost(CreatePostViewModel model);
        
    }
}
