using JazzClub.Models.DataLayer;
using JazzClub.Models.DomainModels;
using Microsoft.AspNetCore.Mvc;
using JazzClub.Models.Helpers;
using System.Linq;

namespace JazzClub.Controllers
{
    public class HomeController : Controller
    {
       
        private Repository<Course> courses { get; set; }

        private Repository<Day> days { get; set; }
      
        public HomeController(JazzClubContext context)
        {
            courses = new Repository<Course>(context);
            days = new Repository<Day>(context);
        }

        public ViewResult Index(int id)
        {

            int DayId;

            var dayOptions = new QueryOptions<Day>
            {
                OrderBy = d => d.DayId
            };

            
            var classOptions = new QueryOptions<Course>
            {
                Includes = "Days",
                
                OrderBy = c => c.CourseId
            };

            if(id == 0)
            {
                DayId = EnumsHelper.GetIntDaysEnum(DateTime.Now.DayOfWeek.ToString());
            }
            else
            {
                DayId = id;
            }

            //classOptions.Where = x => x.Status == 0;
            classOptions.Where = y => y.Days.Any(x => x.DayId == DayId) && y.Status == 0;
            var daysList = days.List(dayOptions);
            
            var courseList = courses.List(classOptions);

            
            ViewBag.Day = daysList;
            ViewBag.Id = id;
            ViewBag.DayId = DayId;



			return View(courseList);
        }

        [HttpGet]
        public ViewResult Add()
        {
            return View("Add", new Course());
        }

        [HttpPost]
        public IActionResult Add(Course course)
        {
            if (ModelState.IsValid)
            {
                courses.Insert(course);
                courses.Save();
                return RedirectToAction("Index");
            }
            else
            {
                return View(course);
            }
        }

    }
}
