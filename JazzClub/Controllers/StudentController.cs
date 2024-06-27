using Azure;
using JazzClub.Models.DataLayer;
using JazzClub.Models.DomainModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace JazzClub.Controllers
{
    public class StudentController : Controller
    {
        private Repository<Student> students {  get; set; }
        private Repository<Guardian> guardians { get; set; }

       

        public StudentController(JazzClubContext context)
        {
            students = new Repository<Student>(context);
            guardians = new Repository<Guardian>(context);
        }
        public IActionResult Index()
        {
            var studentOptions = new QueryOptions<Student>
            {
                OrderBy = t => t.Id,
                Where = s => s.Status == 0,
                Includes = "Guardian"
            };

            var stu = students.List(studentOptions);

			return View(students.List(studentOptions));
        }

        [HttpGet]
        public ViewResult Add()
        {
			this.LoadViewBag("Add");
            Student studentsoro = new Student();
            this.GetGuardians(studentsoro);

			return View("Add", studentsoro); 
        }

        [HttpPost]
        public async Task<IActionResult> Add(Student student, IFormFile file, string droplist)
        {
			bool isAdd = student.Id == 0;
          
			if (this.CheckStudentModel(student) && file != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    if(memoryStream.Length < 2097152)
                    {
                        student.Photo = memoryStream.ToArray();
                        
                         
                    }
                    
					students.Insert(student);
					students.Save();
					
					return RedirectToAction("Index");
				}
            }
            else
            {
				string operation = (isAdd) ? "Add" : "Edit";
                this.LoadViewBag(operation);
				this.GetGuardians(student);
				return View( "Add", student);
			}
 
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            var c = this.GetStudent(id);
            //string res = Encoding.UTF8.GetString(c.Photo);
            
			this.GetGuardians(c);
			return View(c);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student student, IFormFile file, string droplist)
        {
			bool isAdd = student.Id == 0;
			if (this.CheckStudentModel(student) )
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

						students.Update(student);
						students.Save();

						return RedirectToAction("Index");
					}
				} 
                else
                {



                    //student.Photo = Encoding.UTF8.GetBytes() ;
                    //string gg = TempData["Photo"].ToString();
					//students.Update(student);
					//students.Save();
					return RedirectToAction("Index");
				}
			}
			else
			{
				string operation = "Edit";
				this.LoadViewBag(operation);
				this.GetGuardians(student);
				return View("Edit", student);
			}
		}

        private void LoadViewBag(string operation)
        {
            


            ViewBag.PhotoContent = new PhotoModel();
			ViewBag.Operation = operation;
		}

        public List<SelectListItem> GetGuardians(Student student)
        {
			var guardianList = guardians.List(new QueryOptions<Guardian>
			{
				OrderBy = d => d.GuardianId,
				Where = d => d.status == 0
			});

            List<SelectListItem> guardianSelectList = new List<SelectListItem>();
            foreach (var item in guardianList)
            {
                student.GuardiansSelectList.Add(new SelectListItem { Text = item.Name, Value = item.GuardianId.ToString() });

			}    
            return guardianSelectList;
		}

        private bool CheckStudentModel(Student stu)
        {
            if (stu.FirstName == null)
                return false;
            if (stu.LastName == null)
                return false;

            return true;
        }

        private Student GetStudent(int id)
        {
            var studentOptions = new QueryOptions<Student>
            {
                Where = c => c.Id == id
            };
            return students.Get(studentOptions) ?? new Student();
        }

	}
}
