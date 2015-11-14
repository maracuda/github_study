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

        /* public ActionResult RemovePrefix(string id)
         {
             _prefixViewer.RemovePrefix(id);
             return PartialView(_prefixViewer.GetPrefixies());
         }*/

        public JsonResult ViewPrefix()
        {
            return Json(_prefixViewer.GetPrefixies().Select(s => new { Id = s.Id, Network = s.ToString() }), JsonRequestBehavior.AllowGet);
        }
        public void RemovePrefix(string id)
        {
            _prefixViewer.RemovePrefix(id);
        }

        public void AddPrefix(RepositoryEntity prefix)
        {
            _prefixViewer.AddPrefix(new Prefix(prefix.Id, prefix.Network));
        }

        public void UpdatePrefix(string json_string)
        {
            var prefix_pair = JsonConvert.DeserializeObject<List<RepositoryEntity>>(json_string);


            _prefixViewer.UpdatePrefix(new Prefix(prefix_pair.First().Id, prefix_pair.First().Network),
                 new Prefix(prefix_pair.Last().Id, prefix_pair.Last().Network));
        }

    }
}
