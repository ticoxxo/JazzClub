using JazzClub.Models.DataLayer;
using JazzClub.Models.DomainModels;
using Microsoft.AspNetCore.Mvc;

namespace JazzClub.Controllers
{
	public class CourseController : Controller
	{
		private Repository<Course> courses {  get; set; }

		private Repository<Day> days { get; set; }


		public CourseController(JazzClubContext ctx)
		{
			courses = new Repository<Course>(ctx);
			days = new Repository<Day>(ctx);
		}

		public RedirectToActionResult Index() => RedirectToAction("Index", "Home");

		[HttpGet]
		public ViewResult Add()
		{
			this.LoadViewBag("Add");
			return View("AddEdit", new Course());
		}

		public IActionResult Add(Course c, string[] scheduledDays)
		//public async Task<IActionResult> Add(Course c, string[] scheduledDays)
		{
			bool isAdd = c.CourseId == 0;

			if (ModelState.IsValid)
			{
				if (isAdd)
				{
					foreach (var dayId in scheduledDays)
					{
						Day d = days.Get(int.Parse(dayId));
						c.Days.Add(d);
					}
					courses.Insert(c);
					
				}
				else
				{
					courses.Update(c);
				}
				courses.Save();
				return RedirectToAction("Index", "Home");
			}
			else
			{
				string operation = (isAdd) ? "Add" : "Edit";
				this.LoadViewBag(operation);
				return View("AddEdit", c);
			}
		}


		private void LoadViewBag(string operation)
		{
			ViewBag.Days = days.List(new QueryOptions<Day>
			{
				OrderBy = d => d.DayId
			});

			ViewBag.Operation = operation;
		}
	}
}
