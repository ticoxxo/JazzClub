using Azure;
using JazzClub.Model.DomainsModels;
using JazzClub.Models.DataLayer;
using JazzClub.Models.DomainModels;
using JazzClub.Models.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Docs.Samples;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace JazzClub.Controllers
{
	public class CourseController : Controller
	{
		
		private readonly JazzClubContext _context;

		public CourseController(JazzClubContext ctx)
		{
			//courses = new Repository<Course>(ctx);
			//days = new Repository<Day>(ctx);
            //courseDays= new Repository<CourseDay>(ctx);
			_context = ctx;

        }

		//public RedirectToActionResult Index() => RedirectToAction("Index", "Home");

		public async Task<IActionResult> Home(int id)
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

			if (id == 0)
			{
				DayId = EnumsHelper.GetIntDaysEnum(DateTime.Now.DayOfWeek.ToString());
			}
			else
			{
				DayId = id;
			}

			//classOptions.Where = x => x.Status == 0;
			classOptions.Where = y => y.Days.Any(x => x.DayId == DayId) && y.Status == 0;
			var daysList = await _context.Days
				.OrderBy(d => d.DayId)
				.ToListAsync();

			var courseDaysList = await _context.CourseDays
				.Where(d => d.DaysDayId == DayId && d.status == 0)
				.ToListAsync();

			var courseList = await _context.Courses
				.Where( y => courseDaysList.Select(x => x.CoursesCourseId).Contains(y.CourseId))
				.OrderBy(d => d.CourseId)
				.ToListAsync();


			ViewBag.DayList = daysList;
			ViewBag.Id = id;
			ViewBag.DayId = DayId;
			return View(courseList);
		}

		[HttpGet]
		public async Task<ViewResult> Add()
		{
			var dayList = await GetDayList("Add", 0);
			ViewBag.DayList = dayList;
			ViewBag.Operation = "Add";
			return View( new Course());
		}

		[HttpPost]
		public async Task<IActionResult> Add(Course c, string[] scheduledDays)
		//public async Task<IActionResult> Add(Course c, string[] scheduledDays)
		{
			bool isAdd = c.CourseId == 0;

			if (ModelState.IsValid)
			{
				if (isAdd)
				{
					foreach (var dayId in scheduledDays)
					{
						Day d = await _context.Days.FindAsync(int.Parse(dayId)) ?? new Day();
							//days.GetAsync(int.Parse(dayId)) ?? new Day();
						c.Days.Add(d);
					}
					_context.Courses.Add(c);

				}
				else
				{
					_context.Courses.Update(c);
					//courses.Update(c);
				}
				await _context.SaveChangesAsync();
				return RedirectToAction("Home",c);
			}
			else
			{
				
				var dayList = await GetDayList("Add", 0);
				ViewBag.DayList = dayList;
				ViewBag.Operation = "Add";
				return View("Add", c);
			}
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
		

			
			var dayList = await GetDayList("Edit", id);

			var course = await _context.Courses.FindAsync(id);
			ViewBag.DayList = dayList;
			ViewBag.Operation = "Edit";
			return View(course);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Course c, string[] newSchedule)
		{
			if(ModelState.IsValid)
			{
				

				var oldSchedule = await _context.CourseDays.
					Where(d => d.CoursesCourseId == c.CourseId && d.status == 0).
					ToListAsync();

				HashSet<int> newIds = new HashSet<int>();

				foreach (var old in newSchedule)
				{
					int id = Int32.Parse(old);
					newIds.Add(id);
					if (!oldSchedule.Any(dia => dia.DaysDayId == id))
					{

						Day dia = await _context.Days.FindAsync(id) ?? new Day();
						c.Days.Add(dia);
					} 
				}
				
				var toUpdateDays = oldSchedule.Select(m => m.DaysDayId).Where(d => !newIds.Contains(d));
				if(toUpdateDays.Count() > 0)
				{
					foreach (int updateDay in toUpdateDays)
					{
						CourseDay diaNew = await _context.CourseDays
							.Where(d => d.CoursesCourseId == c.CourseId && d.DaysDayId == updateDay && d.status == 0)
							.FirstAsync();
						diaNew.status = 1;
						_context.CourseDays.Update(diaNew);
					}
				}
				
				_context.Courses.Update(c);
				await _context.SaveChangesAsync();
				return RedirectToAction("Home");
			} else
			{
				return View(c);
			}
			
		}

		[HttpGet]
		public async Task<ViewResult> Detail(int id)
		{
			

			var course = await _context.Courses
				.Include(c => c.Students)
				.Where(x => x.CourseId == id)
				.FirstOrDefaultAsync();

			await this.GetSelectListStudents(course);

			
			return View(course);
		}

        //[Route("Course/Detail/{course.CourseId}")]
        [HttpPost]
		public async Task<IActionResult> EnrollStudent(Course course)
		{
			
			
			if (course != null)
			{
				CourseStudent courseStudent = new CourseStudent();
				courseStudent.StudentId = course.SelectedStudentId;
				courseStudent.CourseId = course.CourseId;
				courseStudent.Cost = course.Cost;
				courseStudent.Notes = "";
				_context.CoursesStudents.Add(courseStudent);
				await _context.SaveChangesAsync();
				var updated = await _context.Courses.FindAsync(course.CourseId);
				//return ControllerContext.MyDisplayRouteInfo(course.CourseId);
				return RedirectToAction("Home");
            } else
			{
                return View("Detail/{course.CourseId}", course);
                //return ControllerContext.MyDisplayRouteInfo(course.CourseId);
            }
		}

		private async Task<List<Day>> GetDayList(string operation,int id)
		{
			var dayList = await _context.Days
				.OrderBy(e => e.DayId)
				.ToListAsync();

			if (operation == "Edit")
			{
				var days = await GetCourseDayList(id);
				foreach (var item in dayList)
				{
					var c = days.Any(x => x.DaysDayId == item.DayId);
					item.IsChecked = c;

				}
			}

			return dayList;
		}

		private async Task<Course> GetSelectListStudents(Course course)
		{
			var listStudent = await _context.Students
				.Where(x => x.Status == 0)
				.ToListAsync();
			//				.Where( y => courseDaysList.Select(x => x.CoursesCourseId).Contains(y.CourseId))

			if (course.Students.Count != 0)
			{
				var setToRemove = new HashSet<Student>(course.Students);
				listStudent.RemoveAll(x => setToRemove.Contains(x));
			}

			foreach (var item in listStudent)
			{
				course.StudentsSelectList.Add(new SelectListItem
				{
					Text= item.FullName, Value = item.Id.ToString()
				});
			}

			ViewBag.StudentList = listStudent;
			return course;
		}

		private async Task<List<CourseDay>> GetCourseDayList(int id)
		{
			var days = await _context.CourseDays
					.Where(c => c.CoursesCourseId == id && c.status == 0)
					.ToListAsync();

			return days;
		}
	}
}
