using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using StudyProjectOne.Models;
using StudyProjectOne.Repository;
using StudyProjectOne.Services;

namespace StudyProjectOne.Controllers
{
    public class HomeController : Controller
    {
        private readonly PrefixManipulator _prefixManipulator;

        public HomeController()
        {
            var repo_path = AppDomain.CurrentDomain.BaseDirectory + "../Input.txt";
            _prefixManipulator = new PrefixManipulator(new FileRepository(repo_path));
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ViewPrefixes()
        {
            return Json(_prefixManipulator.PrefixViewList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ViewSpanningPrefixes()
        {
            return Json(_prefixManipulator.SpanningList(), JsonRequestBehavior.AllowGet);
        }

        public void RemovePrefix(string id)
        {
            _prefixManipulator.RemovePrefix(id);
        }

        public void AddPrefix(PrefixViewModel prefix_view)
        {
            _prefixManipulator.AddPrefix(prefix_view);
        }

        public void UpdatePrefix(string json_string)
        {
            var prefix_pair = JsonConvert.DeserializeObject<List<PrefixViewModel>>(json_string);
            _prefixManipulator.UpdatePrefix(prefix_pair.First(), prefix_pair.Last());
        }

    }
}
