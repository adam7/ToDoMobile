using System.Collections.Generic;
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
			IEnumerable<ToDoItem> items = new List<ToDoItem>
					{
						new ToDoItem { Id = "1", Name = "Test item 1", Complete = false },
						new ToDoItem { Id = "2", Name = "Test item 2", Complete = true },
						new ToDoItem { Id = "3", Name = "Test item 3", Complete = false },
						new ToDoItem { Id = "4", Name = "Test item 4", Complete = false },
						new ToDoItem { Id = "5", Name = "Test item 5", Complete = true },
					};

			return View(items);
		}

		public ActionResult Details(string id)
		{
			return View();
		}

		//
		// GET: /ToDoItem/Create

		public ActionResult Create()
		{
			return View();
		}

		//
		// POST: /ToDoItem/Create

		[HttpPost]
		public ActionResult Create(FormCollection collection)
		{
			try
			{
				// TODO: Add insert logic here

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		//
		// GET: /ToDoItem/Edit/5

		public ActionResult Edit(int id)
		{
			return View();
		}

		//
		// POST: /ToDoItem/Edit/5

		[HttpPost]
		public ActionResult Edit(int id, FormCollection collection)
		{
			try
			{
				// TODO: Add update logic here

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		//
		// GET: /ToDoItem/Delete/5

		public ActionResult Delete(int id)
		{
			return View();
		}

		//
		// POST: /ToDoItem/Delete/5

		[HttpPost]
		public ActionResult Delete(int id, FormCollection collection)
		{
			try
			{
				// TODO: Add delete logic here

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}
	}
}

