using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumETF.ViewModels;
using ForumETF.Models;

namespace ForumETF.Repositories
{
    interface IUserRepository
    {
        AppUser GetUser(string username);
    }
}
