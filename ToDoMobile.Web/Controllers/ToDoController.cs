using System.Linq;
using System.Web.Mvc;
using ToDoMobile.Web.Models;

namespace ToDoMobile.Web.Controllers
{
	public class ToDoController : Controller
	{
        public ActionResult Index()
		{
            var toDoItems = MvcApplication.CurrentSession.Query<ToDo>().OrderBy(todo => todo.Priority);

			return View(toDoItems);
		}

        [HttpGet]
		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Create(ToDo toDoItem)
		{
            if(ModelState.IsValid)
            {
                MvcApplication.CurrentSession.Store(toDoItem);
                MvcApplication.CurrentSession.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
		}

        [HttpGet]
		public ActionResult Edit(string id)
		{
			return View(MvcApplication.CurrentSession.Load<ToDo>(id));
		}

		[HttpPost]
        public ActionResult Edit(ToDo toDoItem)
        {
            if (ModelState.IsValid)
            {
                MvcApplication.CurrentSession.Store(toDoItem);
                MvcApplication.CurrentSession.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(toDoItem);
            }
        }
	}
}

