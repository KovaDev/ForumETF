using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForumETF.Controllers
{
    public class TagController : Controller
    {
        // GET: Tag
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Create()
        { 
            // Metoda koja treba da obradi ajax poziv. Prvo treba da provjeri da li 
            // tag koji je unesen postoji u bazi. Ako postoji, onda ga samo treba upisati
            // u input field, a ako ne postoji, onda ga kreirati u bazi.

            return null;
        }
    }
}