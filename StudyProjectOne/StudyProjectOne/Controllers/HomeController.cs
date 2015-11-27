using System;
using System.Web.Mvc;
using PrefixLibrary.Models;
using PrefixLibrary.Repository;
using PrefixLibrary.Services;

namespace StudyProjectOne.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<PrefixView> _repository;

        public HomeController()
        {
            var repo_path = AppDomain.CurrentDomain.BaseDirectory + "../Input.txt";
            _repository = new FileRepository(repo_path);
        }

        /// <summary>
        ///     Вывод главного представления
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///     Выводит содержимое репозитория
        /// </summary>
        /// <returns>Список строк для таблицы</returns>
        public JsonResult ViewPrefixes()
        {
            return Json(_repository.ReadAll(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///     Выводит содержимое покрывающего набора
        /// </summary>
        /// <returns>Список вершин и потомков</returns>
        public JsonResult ViewSpanningPrefixes()
        {
            var spanning_tree_builder = new SpanningTreeBuilder(_repository.ReadAll());
            return Json(spanning_tree_builder.SpanningList(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///     Метод удаления подсети
        /// </summary>
        /// <param name="id">Строковый идентификатор префикса</param>
        public void RemovePrefix(string id)
        {
            _repository.Delete(id);
        }

        /// <summary>
        ///     Метод добавления подсети
        /// </summary>
        /// <param name="prefix_view">Модель представления префикса</param>
        public void AddPrefix(PrefixView prefix_view)
        {
            _repository.Add(new PrefixView(prefix_view.Id, prefix_view.PrefixString));
        }

        /// <summary>
        ///     Метод изменения префикса
        /// </summary>
        /// <param name="prefix_view">Пара префиксов: старый, новый</param>
        public void UpdatePrefix(PrefixView prefix_view)
        {
            _repository.Update(prefix_view.Id, new PrefixView(prefix_view.Id, prefix_view.PrefixString));
        }

        /// <summary>
        ///     Валидация входных данных при добавлении нового элемента
        /// </summary>
        /// <param name="prefix_view_model"></param>
        /// <returns></returns>
        public string ValidatePrefixStringForAdding(PrefixView prefix_view_model)
        {
            //Или исопользовать статический UniqueValidator, но засылать туда коллекцию префиксов
            //Или писать отдельный валидатор, который принимает репозиторий. Или так криво приводить... Подумать...
            var validator = (IPrefixValidator) _repository;

            var format_validation_result = PrefixValidator.ValidatePrefixString(prefix_view_model.PrefixString);
            var unique_validation_result =
                validator.IsPrefixIdUnique(prefix_view_model.Id) +
                validator.IsPrefixStringUnique(prefix_view_model.PrefixString);

            return format_validation_result == ""
                ? unique_validation_result == ""
                    ? ""
                    : unique_validation_result
                : format_validation_result;
        }

        public string ValidatePrefixStringForEditing(PrefixView prefix_view_model)
        {
            //Или исопользовать статический UniqueValidator, но засылать туда коллекцию префиксов
            //Или писать отдельный валидатор, который принимает репозиторий. Или так криво приводить... Подумать...
            var validator = (IPrefixValidator)_repository;

            var format_validation_result = PrefixValidator.ValidatePrefixString(prefix_view_model.PrefixString);
            var unique_validation_result =validator.IsPrefixStringUnique(prefix_view_model.PrefixString);

            return format_validation_result == ""
                ? unique_validation_result == ""
                    ? ""
                    : unique_validation_result
                : format_validation_result;
        }
    }
}