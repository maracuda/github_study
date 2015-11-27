using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using PrefixLibrary.Models;
using PrefixLibrary.Repository;
using PrefixLibrary.Services;

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
        /// <summary>
        /// Вывод главного представления
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Выводит содержимое репозитория
        /// </summary>
        /// <returns>Список строк для таблицы</returns>
        public JsonResult ViewPrefixes()
        {
            return Json(_prefixManipulator.PrefixViewList, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Выводит содержимое покрывающего набора
        /// </summary>
        /// <returns>Список вершин и потомков</returns>
        public JsonResult ViewSpanningPrefixes()
        {
            return Json(_prefixManipulator.SpanningList(), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Метод удаления подсети
        /// </summary>
        /// <param name="id">Строковый идентификатор префикса</param>
        public void RemovePrefix(string id)
        {
            _prefixManipulator.RemovePrefix(id);
        }
        /// <summary>
        /// Метод добавления подсети
        /// </summary>
        /// <param name="prefix_view">Модель представления префикса</param>
        public void AddPrefix(PrefixViewModel prefix_view)
        {
            _prefixManipulator.AddPrefix(prefix_view);
        }
        /// <summary>
        /// Метод изменения префикса
        /// </summary>
        /// <param name="json_string">Пара префиксов: старый, новый</param>
        public void UpdatePrefix(string json_string)
        {
            var prefix_pair = JsonConvert.DeserializeObject<List<PrefixViewModel>>(json_string);
            _prefixManipulator.UpdatePrefix(prefix_pair.First(), prefix_pair.Last());
        }

        public string ValidatePrefixString(PrefixViewModel prefix_view_model)
        {
            return PrefixManipulator.ValidatePrefixString(prefix_view_model.PrefixString);
        }
    }
}