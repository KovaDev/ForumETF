using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ForumETF.Models;

namespace ForumETF.Repositories
{
    public class PostRepository
    {
        private AppDbContext db = null;

        public PostRepository()
        {
            this.db = new AppDbContext();
        }

        
    }
}