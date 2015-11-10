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
        private string _repoPath;
        private PrefixViewer _prefixViewer;
        public HomeController()
        {
            _repoPath = AppDomain.CurrentDomain.BaseDirectory + "../Input.txt";
            _prefixViewer = new PrefixViewer(new FileRepository(_repoPath));
        }

        public ActionResult Index()
        {
            
            return View(_prefixViewer.GetPrefixies());
        }

        public ActionResult RemovePrefix(string id)
        {
            _prefixViewer.RemovePrefix(id);
            return PartialView(_prefixViewer.GetPrefixies());
        }

    }
}
