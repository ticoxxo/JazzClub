using JazzClub.Models.DataLayer;
using JazzClub.Models.DomainModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace JazzClub.Controllers
{
    public class GuardianController : Controller
    {
        private Repository<Guardian> guardians {  get; set; }
        public GuardianController(JazzClubContext ctx) => guardians = new Repository<Guardian>(ctx);
        public IActionResult Index()
        {
            var options = new QueryOptions<Guardian>
            {
                OrderBy = t => t.Name,
                Where = s => s.status == 0
            };

            return View(guardians.List(options));
        }

        [HttpGet]
        public ViewResult Add()
        {
            this.LoadViewBag("Add");
            return View("Add", new Guardian());
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            this.LoadViewBag("Edit");
            var c = this.GetGuardian(id);
            return View( "Add", c);

		}

        [HttpPost]
        public IActionResult Add(Guardian guardian)
        {
            bool isAdd = guardian.GuardianId == 0;

            if (ModelState.IsValid)
            {
                if (isAdd)
                    guardians.Insert(guardian);
                else
                    guardians.Update(guardian);

                guardians.Save();
                return RedirectToAction("Index");
            }
            else
            {
                string operation = (isAdd) ? "Add" : "Edit";
                this.LoadViewBag(operation);
                return View("Add",guardian);
            }
        }

        private Guardian GetGuardian(int id)
        {
            var guardianOptions = new QueryOptions<Guardian>
            {
                Where = c => c.GuardianId == id
            };
            return guardians.Get(guardianOptions) ?? new Guardian();
        }

		private void LoadViewBag(string operation)
		{
			

			ViewBag.Operation = operation;
		}

		[HttpGet]
        public ViewResult Delete(int id)
        {
            var c = this.GetGuardian(id);
            return View(c);
        }

        [HttpPost]
        public RedirectToActionResult Delete(Guardian guardian)
        {
            guardians.Delete(guardian);
            guardians.Save();
            return RedirectToAction("Index");
        }
       
    }
}
