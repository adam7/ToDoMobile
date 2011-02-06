using System.Linq;
using System.Web.Mvc;
using ToDoMobile.Web.Models;

namespace ToDoMobile.Web.Controllers
{
	public class ToDoItemController : Controller
	{
		//
		// GET: /ToDoItem/

		public ActionResult Index()
		{
            var toDoItems = MvcApplication.CurrentSession.Query<ToDoItem>();

			return View(toDoItems);
		}

		public ActionResult Details(string id)
		{
			return View();
		}

        [HttpGet]
		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Create(ToDoItem toDoItem)
		{
            if(ModelState.IsValid)
            {
                MvcApplication.CurrentSession.Store(toDoItem);
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
			return View(MvcApplication.CurrentSession.Query<ToDoItem>().First(item => item.Id == id));
		}

		[HttpPost]
        public ActionResult Edit(ToDoItem toDoItem)
        {
            if (ModelState.IsValid)
            {
                MvcApplication.CurrentSession.Store(toDoItem);
                return RedirectToAction("Index");
            }
            else
            {
                return View(toDoItem);
            }
        }

		public ActionResult Delete(int id)
		{
			return View();
		}

		[HttpPost]
		public ActionResult Delete(ToDoItem toDoItem)
		{
			try
			{
				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}
	}
}

