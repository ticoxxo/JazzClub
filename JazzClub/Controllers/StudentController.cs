using Azure.Identity;
using JazzClub.Models.DataLayer;
using JazzClub.Models.DomainModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace JazzClub.Controllers
{
	public class StudentController : Controller
	{
	
		private readonly JazzClubContext _context;

		public StudentController(JazzClubContext context) => _context = context;
		public async Task<IActionResult> Index()
		{
			

			var list = await _context.Students
				.Include(d => d.Guardian)
				.Where(x => x.Status == 0)
				.OrderBy(c => c.Id)
				.ToListAsync();
			await this.GetLastPaymentsForStudentsList(list);
			return View(list);
		}

		[HttpGet]
		public async Task<ViewResult> Add()
		{
			this.LoadViewBag("Add");
			Student student = new Student();
			Student studentsoro = await this.GetGuardians(student);
			


			return View("Add", studentsoro);
		}

		[HttpPost]
		public async Task<IActionResult> Add(Student student, IFormFile file, string droplist)
		{
			bool isAdd = student.Id == 0;

			if (this.CheckStudentModel(student) && file != null)
			{

				if (file != null)
				{
					using (var memoryStream = new MemoryStream())
					{
						await file.CopyToAsync(memoryStream);
						if (memoryStream.Length < 2097152)
						{
							student.Photo = memoryStream.ToArray();

						}

						_context.Students.Add(student);
						//students.Insert(student);
						await _context.SaveChangesAsync();

						return RedirectToAction("Index");
					}
				} 
				else
				{
					_context.Students.Add(student);
					await _context.SaveChangesAsync();
					return RedirectToAction("Index");
				}
			}
			else
			{
				string operation = (isAdd) ? "Add" : "Edit";
				ViewBag.PhotoContent = new PhotoModel();
				ViewBag.Operation = operation;
				await this.GetGuardians(student);
				return View("Add", student);
			}

		}

		[HttpGet]
		public async Task<ViewResult> Edit(int id)
		{
			var c = await this.GetStudent(id);
			
			await this.GetGuardians(c);
			return View(c);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Student student, IFormFile file, string droplist)
		{
			bool isAdd = student.Id == 0;
			if (this.CheckStudentModel(student))
			{
				if (file != null)
				{
					using (var memoryStream = new MemoryStream())
					{
						await file.CopyToAsync(memoryStream);
						if (memoryStream.Length < 2097152)
						{
							student.Photo = memoryStream.ToArray();


						}

						_context.Students.Update(student);
						await _context.SaveChangesAsync();

						return RedirectToAction("Index");
					}
				}
				else
				{

					

					
					_context.Students.Update(student);
					await _context.SaveChangesAsync();
					return RedirectToAction("Index");
				}
			}
			else
			{
				
				return View("Edit", student);
			}
		}

		[HttpGet]
		public async Task<ViewResult> AddPayment(int id)
		{
			var c = await this.GetStudent(id);
			var cursoInscrito =  await _context.CoursesStudents
				.Where(a => a.StudentId == id && a.status == 0)
				.ToListAsync();

			var cursos = await _context.Courses
				.Where(a => cursoInscrito.Select(x => x.CourseId).Contains(a.CourseId))
				.OrderBy(x => x.CourseId)
				.ToListAsync();

			var lastPayment = await _context.Payments
				.Where(a => a.StudentId == id)
				.OrderByDescending(x => x.PaymentId)
				.FirstOrDefaultAsync();

			var total = cursos.Sum(a => a.Cost);
			c.LastPayment = lastPayment;
			ViewBag.CoursesInscritos = cursoInscrito;
			ViewBag.Cursos = cursos;
			ViewBag.Total = total;
			return View(c);
		}

		[HttpPost]
		public async Task<IActionResult> AddPayment(IFormCollection form, Student student, string[] cursosPagar)
		{
			/*
			 * 
			 * decimal amount = Convert.ToDecimal(form["newPayment.Amount"]);
			decimal discount = Convert.ToDecimal(form["newPayment.Discount"]);
			DateTime paymentDate = Convert.ToDateTime(form["newPayment.PaymentDate"]);
			*/

			// TODO: Add courseId bien
			Payment payment = new Payment();
			decimal amount = Convert.ToDecimal(form["newPayment.Amount"]);
			decimal discount = Convert.ToDecimal(form["newPayment.Discount"]);
			int courseId = Int32.Parse(cursosPagar.Last());

			payment.Amount = amount;
			payment.Discount = discount;
			payment.Total = amount - discount;
			payment.PaymentDate = Convert.ToDateTime(form["newPayment.PaymentDate"]);
			payment.PaymentExpirationDate = Convert.ToDateTime(form["newPayment.PaymentExpirationDate"]);
			payment.PaymentType = Convert.ToString(form["newPayment.PaymentType"]);
			payment.StudentId = student.Id;
			payment.CourseId = courseId;

			_context.Payments.Add(payment);
			await _context.SaveChangesAsync();

			return RedirectToAction("Index");
		}

		private void LoadViewBag(string operation)
		{



			ViewBag.PhotoContent = new PhotoModel();
			ViewBag.Operation = operation;
		}

		public async Task<Student> GetGuardians(Student student)
		{
			

			var guardianList = await _context.Guardians
				.Where(d => d.status == 0)
				.OrderBy(d => d.GuardianId)
				.ToListAsync();

			
			foreach (var item in guardianList)
			{
				student.GuardiansSelectList.Add(new SelectListItem 
				{
					Text = item.Name, Value = item.GuardianId.ToString() 
				});

			}
			 return student;
		}

		private bool CheckStudentModel(Student stu)
		{
			if (stu.FirstName == null)
				return false;
			if (stu.LastName == null)
				return false;

			return true;
		}

		private async Task<Student> GetStudent(int id)
		{
			

			var student = await _context.Students.FindAsync(id);

			return student ?? new Student();
		}

		private async Task<List<Student>> GetLastPaymentsForStudentsList(List<Student> students)
		{
            foreach (var item in students)
            {
				var last = await _context.Payments
					.Where(a => a.StudentId == item.Id)
					.OrderByDescending(x => x.PaymentId)
					.FirstOrDefaultAsync();
				item.LastPayment = last;
            }

            return students;
		}

	}
}
