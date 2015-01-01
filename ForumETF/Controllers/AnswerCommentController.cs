using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForumETF.Controllers
{
    public class AnswerCommentController : Controller
    {
        // GET: AnswerComment
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public PartialViewResult Create()
        {
            return null;
        }

    }
}