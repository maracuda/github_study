using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudyProjectOne.Models;
using StudyProjectOne.Repository;
using StudyProjectOne.Services;

namespace StudyProjectOne.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            var prefix_viewer = new PrefixViewer(new FileRepository(AppDomain.CurrentDomain.BaseDirectory + "../Input.txt"));
            return View(prefix_viewer.GetPrefixies());
        }

    }
}
